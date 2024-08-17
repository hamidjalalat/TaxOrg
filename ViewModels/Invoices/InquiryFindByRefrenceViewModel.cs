using NazmMapping.Mappings;
using System;
using ViewModels.Nazm_tspagents;

namespace ViewModels.Invoices
{
    public class InquiryFindByRefrenceViewModel 
    {
        public int id { get; set; }
        public string Status { get; set; }
        public string Error_Description { get; set; }
        public DateTime? InqueryDate { get; set; }

    }
}
