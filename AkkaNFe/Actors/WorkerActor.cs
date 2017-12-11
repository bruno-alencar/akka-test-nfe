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

        public class QueueSend
        {
            public InvoiceControl InvoiceControl { get; private set; }

            public QueueSend(InvoiceControl invoice)
            {
                InvoiceControl = invoice;
            }
        }

        public class DefineNumber
        {
            public InvoiceControl InvoiceControl { get; private set; }

            public DefineNumber(InvoiceControl invoice)
            {
                InvoiceControl = invoice;
            }
        }

        public class SendSignedBatch
        {
            public InvoiceControl InvoiceControl { get; private set; }

            public SendSignedBatch(InvoiceControl invoice)
            {
                InvoiceControl = invoice;
            }
        }

        public class CheckAuthorization
        {
            public InvoiceControl InvoiceControl { get; private set; }

            public CheckAuthorization(InvoiceControl invoice)
            {
                InvoiceControl = invoice;
            }
        }

        public class MergeSigned
        {
            public InvoiceControl InvoiceControl { get; private set; }

            public MergeSigned(InvoiceControl invoice)
            {
                InvoiceControl = invoice;
            }
        }


        public WorkerActor()
        {
            DoSomething();
        }

        protected override void PreStart()
        {

        }

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
