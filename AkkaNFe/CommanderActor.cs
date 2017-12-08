using Akka.Actor;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNFe
{
    public class CommanderActor : ReceiveActor
    {

        private IActorRef _coordinator;
        private IActorRef _canAcceptJobSender;
        private int pendingJobReplies;

        public CommanderActor()
        {
            PoolCoordinator();
        }

        public class InputMessage
        {
            public string Text { get; set; }

            public InputMessage(string mes)
            {
                Text = mes;
            }
        }

        protected override void PreStart()
        {
            _coordinator = Context.ActorOf(Props.Create(() => new CoordinatorActor()), ActorPath.Coordinator.Name);
            base.PreStart();
        }

        public void PoolCoordinator()
        {
            Receive<InputMessage>(x => {
                BecomeAsking();
            });
        }

        private void BecomeAsking()
        {
            _canAcceptJobSender = Sender;

            //block, but ask the router for the number of routees. Avoids magic numbers.
            pendingJobReplies = 1;// _coordinator.Ask<Routees>(new GetRoutees()).Result.Members.Count();
            Asking();

            //send ourselves a ReceiveTimeout message if no message within 3 seonds
            //Context.SetReceiveTimeout(TimeSpan.FromSeconds(3));
        }

        private void Asking()
        {
            _coordinator.Tell(new CoordinatorActor.Message("sssss"));
            /*Receive<string>(job =>
            {
                //_canAcceptJobSender.Tell(job);

                //start processing messages
                //_coordinator.Tell(new CoordinatorActor.Message(job));

            });*/
        }


    }
}
