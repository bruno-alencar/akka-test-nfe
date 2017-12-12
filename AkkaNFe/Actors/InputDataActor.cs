using Akka.Actor;
using System;

namespace AkkaNFe
{
    public class InputDataActor : ReceiveActor
    {
        private IActorRef _commander;
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
            _commander = Context.ActorOf(Props.Create(() => new CommanderActor()), ActorPaths.Read.Name);
            base.PreStart();
        }

        private void InputDataConsole()
        {
            var text = Console.ReadLine();
            _commander.Tell(new CommanderActor.CanAcceptJob(new ControlInvoice { InvoiceId = new Guid("403b3e6a-fb18-428a-acbd-cf6ea9b11fba") }));
        }
    }
}
