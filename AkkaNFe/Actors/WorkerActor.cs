using Akka.Actor;
using System;

namespace AkkaNFe
{
    public class WorkerActor : ReceiveActor
    {
        #region Messages
        public class QueueSend
        {
            public ControlInvoice InvoiceControl { get; private set; }

            public QueueSend(ControlInvoice invoice)
            {
                InvoiceControl = invoice;
            }
        }

        public class DefineNumber
        {
            public ControlInvoice InvoiceControl { get; private set; }

            public DefineNumber(ControlInvoice invoice)
            {
                InvoiceControl = invoice;
            }
        }

        public class SendSignedBatch
        {
            public ControlInvoice InvoiceControl { get; private set; }

            public SendSignedBatch(ControlInvoice invoice)
            {
                InvoiceControl = invoice;
            }
        }

        public class CheckAuthorization
        {
            public ControlInvoice InvoiceControl { get; private set; }

            public CheckAuthorization(ControlInvoice invoice)
            {
                InvoiceControl = invoice;
            }
        }

        public class MergeSigned
        {
            public ControlInvoice InvoiceControl { get; private set; }

            public MergeSigned(ControlInvoice invoice)
            {
                InvoiceControl = invoice;
            }
        }

        public class StartJob {
            public ControlInvoice InvoiceControl { get; private set; }

            public StartJob(ControlInvoice invoice)
            {
                InvoiceControl = invoice;
            }
        }

        public class FinishProcess { }

        #endregion

        public WorkerActor()
        {
            WorkingService();
        }

        public void WorkingService()
        {
            Receive<StartJob>(job => {
                Console.WriteLine("passou pelo id " + job.InvoiceControl.InvoiceId);
                Sender.Tell(new QueueSend(job.InvoiceControl));
            });

            Receive<QueueSend>(queue =>
            {
                Console.WriteLine("QueueSend id " + queue.InvoiceControl.InvoiceId + "data " + queue.InvoiceControl.StartedOn);
                Sender.Tell(new DefineNumber(queue.InvoiceControl));
            });

            Receive<DefineNumber>(number =>
            {
                Console.WriteLine("DefineNumber id " + number.InvoiceControl.InvoiceId + "data " + number.InvoiceControl.StartedOn);
                Sender.Tell(new SendSignedBatch(number.InvoiceControl));
            });

            Receive<SendSignedBatch>(signed =>
            {
                Console.WriteLine("SendSignedBatch id " + signed.InvoiceControl.InvoiceId + "data " + signed.InvoiceControl.StartedOn);
                Sender.Tell(new CheckAuthorization(signed.InvoiceControl));
            });

            Receive<CheckAuthorization>(check =>
            {
                Console.WriteLine("CheckAuthorization id " + check.InvoiceControl.InvoiceId + "data " + check.InvoiceControl.StartedOn);
                Sender.Tell(new MergeSigned(check.InvoiceControl));
            });

            Receive<MergeSigned>(merge =>
            {
                Console.WriteLine("MergeSigned id " + merge.InvoiceControl.InvoiceId + "data " + merge.InvoiceControl.StartedOn);
                Sender.Tell(new FinishProcess());
            });
        }
    }
}
