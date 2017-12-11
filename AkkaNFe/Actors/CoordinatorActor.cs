﻿using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using static AkkaNFe.WorkerActor;

namespace AkkaNFe
{
    public class CoordinatorActor : ReceiveActor
    {
        private IActorRef _works;
        private Guid _idInvoice;

        public CoordinatorActor()
        {
            /*Receive<Message>(job =>
            {
                Become(Pass);
            });*/

            Pass();

        }

        public class Message
        {
            public string Text { get; set; }

            public Message(string message)
            {
                Text = message;
            }
        }


        public class JobInvoice
        {
            public InvoiceControl InvoiceControl { get; private set; }

            public JobInvoice(InvoiceControl invoice)
            {
                InvoiceControl = invoice;
            }
        }



        protected override void PreStart()
        {
            _works = Context.ActorOf(Props.Create(() => new WorkerActor())
                .WithRouter(new RoundRobinPool(5, new DefaultResizer(1, 10))), ActorPath.Worker.Name);
            base.PreStart();
        }

        public void Pass()
        {
            Receive<JobInvoice>(x =>
            {

            PreparingWork(x.InvoiceControl);

            });
        }

        public void PreparingWork(InvoiceControl invoice)
        {
            _idInvoice = invoice.IdInvoice;
            Become(InvoiceWorking);
        }


        public void InvoiceWorking()
        {
            _works.Tell(_idInvoice);

            Receive<QueueSend>(queue =>
            {
                _works.Tell(queue);
            });

            Receive<DefineNumber>(number =>
            {
                _works.Tell(number);
            });

            Receive<SendSignedBatch>(signed =>
            {
                _works.Tell(signed);
            });

            Receive<SendSignedBatch>(signed =>
            {
                _works.Tell(signed);
            });


            Sender.Tell("OK");
        }



    }
}