using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNFe
{
    public class CoordinatorActor : ReceiveActor
    {
        private IActorRef _works;

        public CoordinatorActor()
        {
             Receive<Message>(job =>
            {
                Start(job.Text);
            });
        }

        public class Message
        {
            public string Text { get; set; }

            public Message(string message)
            {
                Text = message;
            }
        }

        protected override void PreStart()
        {
            _works = Context.ActorOf(Props.Create(() => new WorkerActor())
                .WithRouter(new RoundRobinPool(5, new DefaultResizer(1 , 10))), ActorPath.Worker.Name);
            base.PreStart();
        }

        public void Start(string message)
        {
            _works.Tell(message);
            Sender.Tell("OK");
        }
    }
}
