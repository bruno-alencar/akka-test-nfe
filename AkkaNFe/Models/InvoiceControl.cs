using AkkaNFe.Models;
using System;

namespace AkkaNFe
{
    public class ControlInvoice
    {
        public Guid InvoiceId { get; set; }
        public int Retry { get; set; }
        public DateTimeOffset StartedOn { get; set; }
        public DateTimeOffset FinishedOn { get; set; }

        //deprecated
        public RequestInvoice RequestInvoice { get; set; }
    }
}
