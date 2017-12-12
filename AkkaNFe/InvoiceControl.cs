using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNFe
{
    public class InvoiceControl
    {
        public Guid InvoiceId { get; set; }
        public int Retry { get; set; }
        public DateTimeOffset StartedOn { get; set; }
        public DateTimeOffset FinishedOn { get; set; }
    }
}
