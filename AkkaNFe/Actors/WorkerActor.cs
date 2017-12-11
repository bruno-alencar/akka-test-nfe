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

        public class FinishProcess
        {

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
            /*Receive<string>(job => {
                var result = $"AEEEEEEEEEEEEEE {job}";
                Console.WriteLine(result);
                Sender.Tell(result);
            });*/

            Receive<Guid>(id => {
                Console.WriteLine("passou pelo id " + id);
                Sender.Tell(new QueueSend(
                    new InvoiceControl { IdInvoice = id, OnStart = DateTimeOffset.UtcNow }));

            });

            Receive<QueueSend>(queue =>
            {
                Console.WriteLine("QueueSend id " + queue.InvoiceControl.IdInvoice + "data " + queue.InvoiceControl.OnStart);
                Sender.Tell(new DefineNumber(queue.InvoiceControl));
            });

            Receive<DefineNumber>(number =>
            {
                Console.WriteLine("DefineNumber id " + number.InvoiceControl.IdInvoice + "data " + number.InvoiceControl.OnStart);
                Sender.Tell(new SendSignedBatch(number.InvoiceControl));
            });

            Receive<SendSignedBatch>(signed =>
            {
                Console.WriteLine("SendSignedBatch id " + signed.InvoiceControl.IdInvoice + "data " + signed.InvoiceControl.OnStart);
                Sender.Tell(new CheckAuthorization(signed.InvoiceControl));
            });

            Receive<MergeSigned>(merge =>
            {
                Console.WriteLine("MergeSigned id " + merge.InvoiceControl.IdInvoice + "data " + merge.InvoiceControl.OnStart);
                Sender.Tell(new FinishProcess());
            });

        }
    }
}
