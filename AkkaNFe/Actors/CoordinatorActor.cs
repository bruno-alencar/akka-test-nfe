using Akka.Actor;
using Akka.Routing;
using static AkkaNFe.WorkerActor;

namespace AkkaNFe
{
    public class CoordinatorActor : ReceiveActor
    {
        private IActorRef _works;

        #region Messages
        public class Message
        {
            public string Text { get; set; }

            public Message(string message)
            {
                Text = message;
            }
        }

        public class BeginJob
        {
            public ControlInvoice InvoiceControl { get; private set; }

            public BeginJob(ControlInvoice invoice)
            {
                InvoiceControl = invoice;
            }
        }
        #endregion

        public CoordinatorActor()
        {
            Waiting();
        }

        protected override void PreStart()
        {
            _works = Context.ActorOf(Props.Create(() => new WorkerActor())
                .WithRouter(new RoundRobinPool(5, new DefaultResizer(1, 10))), ActorPaths.Worker.Name);
            base.PreStart();
        }

        public void Waiting()
        {
            Receive<CommanderActor.CanAcceptJob>(job => Sender.Tell(new CommanderActor.AbleToAcceptJob(job.ControlInvoice)));
            Receive<BeginJob>(job =>
            {
                Become(Working);

                // message to start process
                _works.Tell(new StartJob( invoice: job.InvoiceControl));
            });
        }

        public void Working()
        {
            Receive<QueueSend>(queue =>
            {
                _works.Tell(queue);
            });

            Receive<DefineNumber>(number =>
            {
                _works.Tell(number);
            });

            Receive<SendSignedBatch>(signed =>
            {
                _works.Tell(signed);
            });

            Receive<CheckAuthorization>(check =>
            {
                _works.Tell(check);
            });

            Receive<MergeSigned>(merge =>
            {
                _works.Tell(merge);
            });

            Receive<FinishProcess>(finish =>
            {

            });

            Sender.Tell("OK");
        }
    }
}
