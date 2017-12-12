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
        #region Messages
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
        #endregion

        public WorkerActor()
        {
            WorkingService();
        }

        public void WorkingService()
        {
            Receive<Guid>(id => {
                Console.WriteLine("passou pelo id " + id);
                Sender.Tell(new QueueSend(
                        new InvoiceControl
                        {
                            InvoiceId = id, StartedOn = DateTimeOffset.UtcNow
                        }
                    ));
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
