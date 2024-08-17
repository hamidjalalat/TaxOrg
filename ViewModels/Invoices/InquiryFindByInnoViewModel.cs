using NazmMapping.Mappings;
using ViewModels.Nazm_tspagents;

namespace ViewModels.Invoices
{
    public class InquiryFindByInnoViewModel 
    {
        public int id { get; set; }
        public long inno { get; set; }
        public string TaxId { get; set; }
        public string Reference_Id { get; set; }

    }
}
