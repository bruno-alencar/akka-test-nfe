using Akka.Actor;
using AkkaNFe.Actors;
using AkkaNFe.Models;
using System;

namespace AkkaNFe
{
    public class InputDataActor : ReceiveActor
    {
        public const string StartCommand = "start";

        public InputDataActor()
        {
            Ready();
        }

        //protected override void
        private void Ready()
        {
            Receive<string>(job => job.Equals(StartCommand), test =>
            {
                InputDataConsole();
            });
        }

        protected override void PreStart()
        {
            //_commander = Context.ActorOf(Props.Create(() => new CommanderActor()), ActorPaths.Read.Name);
            //_generator = Context.ActorOf(Props.Create(() => new GeneratorActor()), ActorPaths.Generator.Name);
            base.PreStart();
        }

        private void InputDataConsole()
        {
            var text = Console.ReadLine();

            var requestInvoice = new RequestInvoice();
            Context.ActorSelection(ActorPaths.Generator.Path).Tell(requestInvoice);
        }
    }
}
