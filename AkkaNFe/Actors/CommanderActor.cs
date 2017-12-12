using Akka.Actor;
using Akka.Routing;
using System;
using System.Linq;

namespace AkkaNFe
{
    public class CommanderActor : ReceiveActor, IWithUnboundedStash
    {
        #region Message classes

        public class InputMessage
        {
            public string Text { get; set; }

            public InputMessage(string mes)
            {
                Text = mes;
            }
        }

        public class CanAcceptJob
        {
            public CanAcceptJob(ControlInvoice requestInvoice)
            {
                this.ControlInvoice = requestInvoice;
            }

            public ControlInvoice ControlInvoice { get; private set; }
        }

        public class AbleToAcceptJob
        {
            public AbleToAcceptJob(ControlInvoice controlInvoice)
            {
                this.controlInvoice = controlInvoice;
            }

            public ControlInvoice controlInvoice { get; private set; }
        }

        public class UnableToAcceptJob
        {
            public UnableToAcceptJob(ControlInvoice controlInvoice)
            {
                this.ControlInvoice = controlInvoice;
            }

            public ControlInvoice ControlInvoice { get; private set; }
        }

        #endregion

        public IStash Stash { get; set; }
        private IActorRef _coordinator;
        private IActorRef _canAcceptJobSender;
        private int pendingJobReplies;
        private ControlInvoice _controlInvoice;

        public CommanderActor()
        {
            Ready();
        }

        protected override void PreStart()
        {
            _coordinator = Context.ActorOf(Props.Create(() => new CoordinatorActor())
                                  .WithRouter(new BroadcastPool(1)),
                            ActorPaths.Coordinator.Name);

            base.PreStart();
        }

        private void Ready()
        {
            Receive<CanAcceptJob>(job =>
            {
                _coordinator.Tell(job);
                _controlInvoice = job.ControlInvoice;
                BecomeAsking();
            });
        }

        private void BecomeAsking()
        {
            _canAcceptJobSender = Sender;

            //block, but ask the router for the number of routees. Avoids magic numbers.
            pendingJobReplies = _coordinator.Ask<Routees>(new GetRoutees()).Result.Members.Count();
            Become(Asking);

            //send ourselves a ReceiveTimeout message if no message within 3 seonds
            Context.SetReceiveTimeout(TimeSpan.FromSeconds(60));
        }

        private void Asking()
        {
            //stash any subsequent requests
            Receive<CanAcceptJob>(job => Stash.Stash());

            //means at least one actor failed to respond
            Receive<ReceiveTimeout>(timeout =>
            {
                _canAcceptJobSender.Tell(new UnableToAcceptJob(_controlInvoice));
                BecomeReady();
            });

            Receive<UnableToAcceptJob>(job =>
            {
                pendingJobReplies--;
                if (pendingJobReplies == 0)
                {
                    _canAcceptJobSender.Tell(job);
                    BecomeReady();
                }
            });

            Receive((AbleToAcceptJob job) =>
            {
                _canAcceptJobSender.Tell(job);

                //start processing messages
                _coordinator.Tell(new CoordinatorActor.BeginJob(job.controlInvoice));

                // get pattern
                BecomeReady();
            });
        }

        private void BecomeReady()
        {
            Become(Ready);
            Stash.UnstashAll();

            Context.SetReceiveTimeout(null);
        }
    }
}
