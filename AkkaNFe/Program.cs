using Akka.Actor;

namespace AkkaNFe
{
    class Program
    {

        public static ActorSystem NFeIssuanceActors;

        static void Main(string[] args)
        {
            NFeIssuanceActors = ActorSystem.Create("NFeIssuanceActors");

            var init = NFeIssuanceActors.ActorOf(Props.Create(() => new InputDataActor()), ActorPaths.InitConsole.Name);
            init.Tell("start");

            NFeIssuanceActors.WhenTerminated.Wait();
        }
    }
}
