using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _commander = Context.ActorOf(Props.Create(() => new CommanderActor()), ActorPath.Read.Name);
            base.PreStart();
        }

        private void InputDataConsole()
        {
            var text = Console.ReadLine();
            _commander.Tell(new CommanderActor.InputMessage(text));
        }

    }
}
