using Domain.Anemic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Invoices;
using System.Text.Json;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Globalization;

namespace Application.Common
{
    public class ToObjectConversion
    {
        public List<InvoiceBodeyViewModel> GetInvoiceSetToObject(List<InvoiceSelectViewModel> listNazm_tspagent)
        {
            List<InvoiceBodeyViewModel> InvoiceBodeyViewModels = new List<InvoiceBodeyViewModel>();

            foreach (var item in listNazm_tspagent)
            {
                InvoiceBodeyViewModel InvoiceBodeyViewModel = new InvoiceBodeyViewModel();

                // ----------------------------------------------------------------------
                ViewModels.Invoices.Header header = new ViewModels.Invoices.Header();
                
                header.Indatim =item.Indatim.Value.ToString("yyyy-MM-dd");
                header.Inno = item.Inno;
                header.Tins = item.Tins;
                header.Bid = item.Bid;
                header.Tob = item.Tob;
                header.Setm = item.Setm;
                header.Tins = item.Tins;
                header.Cap = null;
                header.Tinb = item.Tinb;
                header.Bpc = item.Bpc;
                header.Tprdis = (double)item.Fee; 
                header.Tdis = 0;
                header.Tadis = (double)item.Fee; 
                header.Inty = item.Inty;
                header.Ft = item.Ft;
                header.Ins = item.Ins;
                header.Inp = item.Inp;

                // ----------------------------------------------------------------------
                ViewModels.Invoices.Body body = new ViewModels.Invoices.Body();
                body.Sstid = item.Sstid;
                body.Sstt = item.Sstt;
                body.Am = (double)item.Am;   //تعداد/مقدار
                body.Fee = (double)item.Fee;  //مبلغ واحد
                body.Prdis = body.Am * body.Fee;  //مبلغ قبل از تخفیف
                body.Dis = (double)item.Dis;   // مبلغ تخفیف
                body.Adis = body.Prdis - body.Dis;  //مبلغ بعد از تخفیف
                body.Vra = (double)item.Vra;  //نرخ مالیات بر ارزش افزوده
                body.Vam = Math.Floor((body.Vra * body.Adis) / 100);   // KS مبلغ مالیات بر ارزش افزوده
                body.Tsstam = Math.Floor(((body.Fee * body.Vra) / 100) + body.Fee);  // مبلغ کل کاال/خدمت OS

                header.Tvam = Math.Floor((body.Fee * body.Vra) / 100);// مجموع مالیات بر ارزش افزوده 
                header.Tbill = (double)body.Tsstam; // مجموع صورتحساب XS

                InvoiceBodeyViewModel.Header = header;
                InvoiceBodeyViewModel.Body = new List<ViewModels.Invoices.Body> { body };
                InvoiceBodeyViewModel.Payments = new List<Payment> { };

                InvoiceBodeyViewModels.Add(InvoiceBodeyViewModel);
            }

            return InvoiceBodeyViewModels;

        }
        public List<InvoiceBodeyViewModel> GetCancelInvoiceSetToObject(List<InvoiceSelectViewModel> listNazm_tspagent)
        {
            List<InvoiceBodeyViewModel> InvoiceBodeyViewModels = new List<InvoiceBodeyViewModel>();

            foreach (var item in listNazm_tspagent)
            {
                InvoiceBodeyViewModel InvoiceBodeyViewModel = new InvoiceBodeyViewModel();

                // ----------------------------------------------------------------------
                ViewModels.Invoices.Header header = new ViewModels.Invoices.Header();

                header.Indatim = item.Indatim.Value.ToString("yyyy-MM-dd");
                header.Inno = item.Inno;
                header.Tins = item.Tins;
                header.Bid = item.Bid;
                header.Tob = item.Tob;
                header.Setm = item.Setm;
                header.Tins = item.Tins;
                header.Cap = null;
                header.Tinb = item.Tinb;
                header.Bpc = item.Bpc;
                header.Tprdis = (double)item.Fee;
                header.Tdis = 0;
                header.Tadis = (double)item.Fee;
                header.Inty = item.Inty;
                header.Ft = item.Ft;
                header.Ins = item.Ins;
                header.Inp = item.Inp;
                header.TaxId = item.TaxId;
                header.irtaxid = item.TaxId;

                // ----------------------------------------------------------------------
                ViewModels.Invoices.Body body = new ViewModels.Invoices.Body();
               
                body.Sstid = item.Sstid;
                body.Sstt = item.Sstt;
                body.Am = (double)item.Am;   //تعداد/مقدار
                body.Fee = (double)item.Fee;  //مبلغ واحد
                body.Prdis = body.Am * body.Fee;  //مبلغ قبل از تخفیف
                body.Dis = (double)item.Dis;   // مبلغ تخفیف
                body.Adis = body.Prdis - body.Dis;  //مبلغ بعد از تخفیف
                body.Vra = (double)item.Vra;  //نرخ مالیات بر ارزش افزوده
                body.Vam = Math.Floor((body.Vra * body.Adis) / 100);   // KS مبلغ مالیات بر ارزش افزوده
                body.Tsstam = Math.Floor(((body.Fee * body.Vra) / 100) + body.Fee);  // مبلغ کل کاال/خدمت OS

                header.Tvam = Math.Floor((body.Fee * body.Vra) / 100);// مجموع مالیات بر ارزش افزوده 
                header.Tbill = (double)body.Tsstam; // مجموع صورتحساب XS

                InvoiceBodeyViewModel.Header = header;
                InvoiceBodeyViewModel.Body = new List<ViewModels.Invoices.Body> { body };
                InvoiceBodeyViewModel.Payments = new List<Payment> { };

                InvoiceBodeyViewModels.Add(InvoiceBodeyViewModel);
            }

            return InvoiceBodeyViewModels;

        }
    }
}
