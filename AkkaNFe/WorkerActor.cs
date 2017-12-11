using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNFe
{
    public class WorkerActor : ReceiveActor
    {
        public WorkerActor()
        {
            DoSomething();
        }

        protected override void PreStart()
        {
        }
        IActorRef target = null;

        public void DoSomething()
        {
            Receive<string>(job => {
                var result = $"AEEEEEEEEEEEEEE {job}";
                Console.WriteLine(result);
                Sender.Tell(result);
            });
        }
    }
}
