using AutoMapper;
using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using ViewModels.Controllers;

namespace ViewModels.Invoices

{
    public class InvoiceViewModel
    {
        public string Status { get; set; }
        public string InvoiceId { get; set; }
        public bool HasError { get; set; }
        public string ReferenceNumber { get; set; }
        public string TaxId { get; set; }
        public long Inno { get; set; }
        public List<Detail> Details { get; set; }

        public class Detail
        {
            public string ResponseDetailDTO { get; set; }
            public string InvoiceId { get; set; }
            public bool IsError { get; set; }
            public string InvoicePart { get; set; }
            public string InvoicePartId { get; set; }
            public string InvoicePartIndex { get; set; }
            public string Field { get; set; }
            public string Message { get; set; }
            public int ErrorCode { get; set; }
        }
    }
}
