using Akka.Actor;
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
        public CoordinatorActor()
        {
            Receive<Message>(job =>
            {
                Start();
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
            /* Context.ActorOf(Props.Create(() => new GithubWorkerActor(GithubClientFactory.GetClient))
                 .WithRouter(new RoundRobinPool(10)));*/

        }


        public void Start()
        {

            Console.WriteLine("chego");


        }
    }
}
