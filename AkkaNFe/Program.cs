using Akka.Actor;
using AkkaNFe.Actors;

namespace AkkaNFe
{
    class Program
    {

        public static ActorSystem NFeIssuanceActors;

        static void Main(string[] args)
        {
            NFeIssuanceActors = ActorSystem.Create("NFeIssuanceActors");

            NFeIssuanceActors.ActorOf(Props.Create(() => new GeneratorActor()), ActorPaths.Generator.Name);
            NFeIssuanceActors.ActorOf(Props.Create(() => new CommanderActor()), ActorPaths.Commander.Name);

            var init = NFeIssuanceActors.ActorOf(Props.Create(() => new InputDataActor()), ActorPaths.InitConsole.Name);
            init.Tell("start");

            NFeIssuanceActors.WhenTerminated.Wait();
        }
    }
}
    