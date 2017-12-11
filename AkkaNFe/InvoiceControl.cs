using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaNFe
{
    public class InvoiceControl
    {
        public Guid IdInvoice { get; set; }
        public int Retry { get; set; }
        public DateTimeOffset OnStart { get; set; }
        public DateTimeOffset OnFinish { get; set; }
    }
}
