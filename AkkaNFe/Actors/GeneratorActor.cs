using Akka.Actor;
using AkkaNFe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNFe.Actors
{
    public class GeneratorActor : ReceiveActor
    {
        #region Message classes 
        #endregion

        public GeneratorActor()
        {
            ReadyToGenerate();
        }

        private void ReadyToGenerate()
        {

            // TODO: need remove request invoice from control invoice class
            // save on database and generate new Id

            Receive((RequestInvoice requestInvoice) => {

                var id = Guid.NewGuid();
                requestInvoice.Id = id;

                Context.ActorSelection(ActorPaths.Commander.Path)
                    .Tell(new CommanderActor.CanAcceptJob(
                        new ControlInvoice {
                            InvoiceId = id,
                            RequestInvoice = requestInvoice
                    }));
            });

            ////yes
            //Receive<GithubCommanderActor.UnableToAcceptJob>(job => Context.ActorSelection(ActorPaths.MainFormActor.Path).Tell(job));

            ////no
            //Receive<GithubCommanderActor.AbleToAcceptJob>(job => Context.ActorSelection(ActorPaths.MainFormActor.Path).Tell(job));
        }
    }
}
