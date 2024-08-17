using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Nazm_tspagents
{
    public class QueryResponseCountInvoiceViewModel
    {
        public long CountInvoice { get; set; }
        public long CountInvoiceCancel { get; set; }
        public long CountInvoicePending { get; set; }
        public long GetCountInvoiceSending { get; set; }
        public long GetCountInvoiceSuccess { get; set; }
        public long CountInvoiceFailed { get; set; }
    }
}
