using Domain.Anemic.Entities;
using Microsoft.EntityFrameworkCore;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;

namespace ViewModels.Invoices
{
    public class InvoiceSelectViewModel
    {
        public string Sstid { get; set; }
        public string Sstt { get; set; }
        public decimal Am { get; set; }
        public decimal Fee { get; set; }
        public double Prdis { get; set; }
        public decimal Dis { get; set; }
        public double Adis { get; set; }
        public decimal Vra { get; set; }
        public double Vam { get; set; }
        public double Tsstam { get; set; }
        public DateTime? Indatim { get; set; }
        public int Inty { get; set; }
        public int Ft { get; set; }
        public long Inno { get; set; }
        public int Setm { get; set; }
        public string Tins { get; set; }
        public string Cap { get; set; }
        public string Bid { get; set; }
        public string Tinb { get; set; }
        public string Bpc { get; set; }
        public string Insp { get; set; }
        public int Inp { get; set; }
        public int Ins { get; set; }
        public double Tprdis { get; set; }
        public int Tdis { get; set; }
        public double Tadis { get; set; }
        public double Tvam { get; set; }
        public double Tbill { get; set; }
        public int Tob { get; set; }
        public string TaxId { get; set; }

    }

}
