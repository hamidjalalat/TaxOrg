using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;

namespace ViewModels.Invoices
{
    public class InvoiceUpdateViewModel
    {
        public string Token { get; set; }
        public string XOrgId { get; set; }

    }
  
}
