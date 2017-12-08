using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNFe
{
    class Program
    {

        public static ActorSystem NFeIssuanceActors;

        static void Main(string[] args)
        {

            NFeIssuanceActors = ActorSystem.Create("NFeIssuanceActors");

            var init = NFeIssuanceActors.ActorOf(Props.Create(() => new InputDataActor()), ActorPath.InitConsole.Name);

            init.Tell("start");

            NFeIssuanceActors.WhenTerminated.Wait();
        }
    }
}
