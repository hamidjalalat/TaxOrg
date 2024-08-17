using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Invoices
{
    public class InquiryViewModel
    {

        public string ReferenceNumber { get; set; }
        public string Uid { get; set; }
        public string FiscalId { get; set; }
        public string Status { get; set; }
        public DataModel Data { get; set; }
        public string PacketType { get; set; }
        public string TaxId { get; set; }

        public class DataModel
        {
            public string ConfirmationReferenceId { get; set; }
            public List<Error> Error { get; set; }
            public List<string> Warning { get; set; }
            public bool Success { get; set; }
        }

        public class Error
        {
            public string Code { get; set; }
            public string Message { get; set; }
            public string Detail { get; set; }
        }

    }
}
