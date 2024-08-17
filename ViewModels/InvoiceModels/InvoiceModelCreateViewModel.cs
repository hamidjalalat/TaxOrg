using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;
using System;

namespace ViewModels.InvoiceModels
{
    public class InvoiceModelCreateViewModel :IMapFrom<InvoiceModel>
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public bool IsActive { get; set; }
    }
}
