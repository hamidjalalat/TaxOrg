using Application.Common;
using Application.Common.Interfaces.Repository.Anemic.EF;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Anemic.Entities;
using Domain.Enums;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.Anemic.Contexts;
using Persistence.Configurations.Rich;
using System;
using System.Linq;
using System.Threading;
using ViewModels.Invoices;
using ViewModels.Nazm_tspagents;


namespace Infrastructure.Repository.Anemic.EF
{
    public class Nazm_tspagentRepository : EFRepository<Nazm_tspagent>, INazm_tspagentRepository
    {

        public Nazm_tspagentRepository(EFContext context) : base(context)
        {
            _eFContext = context;
            _reduceNow = System.DateTime.Now.AddDays(-21).Date;
        }

        private readonly EFContext _eFContext;

        private System.DateTime _reduceNow;

        public async Task<InquiryFindByInnoViewModel> FindByInnoAsync(long inno)
        {
            return await _eFContext.Nazm_tspagents.Where(C => C.inno == inno && C.TaxId == null && C.indatim > _reduceNow)
                        .OrderBy(C => C.id)
                        .Select(s => new InquiryFindByInnoViewModel
                        {
                            id = s.id,
                            inno = s.inno,
                            TaxId = s.TaxId,
                            Reference_Id = s.Reference_Id,
                        })
                        .FirstAsync();
        }
        public async Task<InquiryFindByInnoViewModel> FindByInnoForCancelInvoiceAsync(long inno)
        {
            return await _eFContext.Nazm_tspagents.Where(C => C.inno == inno && C.TaxId != null)
                        .OrderBy(C => C.id)
                        .Select(s => new InquiryFindByInnoViewModel
                        {
                            id = s.id,
                            inno = s.inno,
                            TaxId = s.TaxId,
                            Reference_Id = s.Reference_Id,
                        })
                        .FirstAsync();
        }
        public async Task<InquiryFindByInnoViewModel> FindByInnoForEditInvoiceAsync(long inno)
        {
            return await _eFContext.Nazm_tspagents.Where(C => C.inno == inno && C.TaxId != null)
                        .OrderBy(C => C.id)
                        .Select(s => new InquiryFindByInnoViewModel
                        {
                            id = s.id,
                            inno = s.inno,
                            TaxId = s.TaxId,
                            Reference_Id = s.Reference_Id,
                        })
                        .FirstAsync();
        }
        public async Task<InquiryFindByRefrenceViewModel> FindByReferenceIdAsync(string referenceId)
        {
            return await _eFContext.Nazm_tspagents.Where(C => C.Reference_Id == referenceId)
                        .OrderBy(C => C.id)
                        .Select(S => new InquiryFindByRefrenceViewModel
                        {
                            id = S.id,
                            Status = S.Status,
                            Error_Description = S.Error_Description,
                            InqueryDate = S.InqueryDate
                        })
                        .FirstAsync();
        }
        public async Task<List<string>> GetAllByFilterForInQuery(int take)
        {
            return await _eFContext.Nazm_tspagents
                         .Where(R => R.Reference_Id != null)
                         .Where(T => T.TaxId != null)
                         .Where(S => S.Status == null || S.Status == "PENDING")
                         .Where(D => D.InqueryDate.Value.AddHours(3) < Nazm.DateTime.Now || D.InqueryDate == null)
                         .OrderBy(C => C.InqueryDate)
                         .Take(take)
                         .Select(Re => Re.Reference_Id)
                         .ToListAsync();
        }
        public async Task<List<InvoiceSelectViewModel>> GetAllByFilterForInvoice(int take)
        {
            return await _eFContext.Nazm_tspagents
                    .Where(D => D.indatim > _reduceNow)
                    .Where(T => T.TaxId == null)
                    .Take(take)
                    .OrderBy(C => C.id)
                    .Select(SE => new InvoiceSelectViewModel
                    {
                        Indatim = SE.indatim,
                        Inno = SE.inno,
                        Tins = SE.tins,
                        Bid = SE.bid,
                        Tob = SE.tob,
                        Setm = SE.setm,
                        Tinb = SE.tinb,
                        Bpc = SE.bpc,
                        Fee = SE.fee,
                        Inty = SE.inty,
                        Ft = SE.ft,
                        Ins = SE.ins,
                        Inp = SE.inp,
                        Sstid = SE.sstid,
                        Sstt = SE.sstt,
                        Am = SE.am,
                        Dis = SE.dis,
                        Vra = SE.vra,
                    })
                    .ToListAsync();
        }
        public async Task<long> GetCountInvoice(Nazm_tspagentInputDateInvoiceViewModel date)
        {
            return await _eFContext.Nazm_tspagents.Where(D => D.indatim.Value >= date.FromDate && D.indatim.Value <= date.ToDate).CountAsync();
        }
        public async Task<long> GetCountInvoiceSuccess(Nazm_tspagentInputDateInvoiceViewModel date)
        {
            return await _eFContext.Nazm_tspagents.Where(C => C.Status == "SUCCESS").Where(D => D.indatim.Value >= date.FromDate && D.indatim.Value <= date.ToDate).CountAsync();
        }
        public async Task<long> GetCountInvoicePending(Nazm_tspagentInputDateInvoiceViewModel date)
        {
            return await _eFContext.Nazm_tspagents.Where(C => C.Status == "PENDING").Where(D => D.indatim.Value >= date.FromDate && D.indatim.Value <= date.ToDate).CountAsync();
        }
        public async Task<long> GetCountInvoiceFailed(Nazm_tspagentInputDateInvoiceViewModel date)
        {
            return await _eFContext.Nazm_tspagents.Where(C => C.Status == "FAILED").Where(D => D.indatim.Value >= date.FromDate && D.indatim.Value <= date.ToDate).CountAsync();
        }
        public async Task<long> GetCountInvoiceCancel(Nazm_tspagentInputDateInvoiceViewModel date)
        {
            return await _eFContext.Nazm_tspagents.Where(C => C.ins == 3).Where(D => D.indatim.Value >= date.FromDate && D.indatim.Value <= date.ToDate).CountAsync();
        }
        public async Task<long> GetCountInvoiceSending(Nazm_tspagentInputDateInvoiceViewModel date)
        {
            return await _eFContext.Nazm_tspagents.Where(C => C.TaxId == null).Where(D => D.indatim.Value >= date.FromDate && D.indatim.Value <= date.ToDate).CountAsync();
        }
        public async Task<List<InvoiceSelectViewModel>> GetAllByFilterForCancelInvoice(int take)
        {
            return await _eFContext.Nazm_tspagents
                    .Where(T => T.TaxId != null)
                    .Where(I => I.ins == 3)
                    .Where(S => S.Status == "SUCCESS")
                    .Take(take)
                    .OrderBy(C => C.id)
                    .Select(SE => new InvoiceSelectViewModel
                    {
                        Indatim = SE.indatim,
                        Inno = SE.inno,
                        Tins = SE.tins,
                        Bid = SE.bid,
                        Tob = SE.tob,
                        Setm = SE.setm,
                        Tinb = SE.tinb,
                        Bpc = SE.bpc,
                        Fee = SE.fee,
                        Inty = SE.inty,
                        Ft = SE.ft,
                        Ins = SE.ins,
                        Inp = SE.inp,
                        Sstid = SE.sstid,
                        Sstt = SE.sstt,
                        Am = SE.am,
                        Dis = SE.dis,
                        Vra = SE.vra,
                        TaxId = SE.TaxId
                    })
                    .ToListAsync();
        }
        public async Task<List<InvoiceSelectViewModel>> GetAllByFilterForEditInvoice(int take)
        {
            return await _eFContext.Nazm_tspagents
                    .Where(T => T.TaxId != null)
                    .Where(I => I.ins == 2)
                    .Where(S => S.Status == "SUCCESS")
                    .Take(take)
                    .OrderBy(C => C.id)
                    .Select(SE => new InvoiceSelectViewModel
                    {
                        Indatim = SE.indatim,
                        Inno = SE.inno,
                        Tins = SE.tins,
                        Bid = SE.bid,
                        Tob = SE.tob,
                        Setm = SE.setm,
                        Tinb = SE.tinb,
                        Bpc = SE.bpc,
                        Fee = SE.fee,
                        Inty = SE.inty,
                        Ft = SE.ft,
                        Ins = SE.ins,
                        Inp = SE.inp,
                        Sstid = SE.sstid,
                        Sstt = SE.sstt,
                        Am = SE.am,
                        Dis = SE.dis,
                        Vra = SE.vra,
                        TaxId = SE.TaxId
                    })
                    .ToListAsync();
        }
        public async Task<Result<bool>> IsUnique(Nazm_tspagent model, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<bool>();
            List<String> errorList = new List<String>();

            #region Add
            if (model.id == 0)
            {
                var isExist = await GetAll.AnyAsync(s => s.inno == model.inno, cancellationToken);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.Code));
                }

            }
            #endregion

            #region Edit
            if (model.id > 0)
            {
                var isExist = await GetAll.AnyAsync(s => s.id != model.id && s.inno == model.inno, cancellationToken);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.Code));
                }

            }
            #endregion

            if (errorList.Count > 0)
            {
                return result.WithErrors(errorList);
            }
            else
            {
                return result.WithValue(true);
            }
        }
        public IQueryable<Nazm_tspagent> GetAllByFilter(Nazm_tspagentInputParamsViewModel InputParams)
        {
            var Nazm_tspagent = _eFContext.Nazm_tspagents.AsQueryable();

            if (InputParams.BranchId != 0)
            {
                Nazm_tspagent = Nazm_tspagent
                    .Where(B => B.Branch_Id == InputParams.BranchId);
            }

            if (InputParams.Bid != null)
            {
                Nazm_tspagent = Nazm_tspagent
                    .Where(B => B.bid == InputParams.Bid);
            }

            if (InputParams.Inno != 0)
            {
                Nazm_tspagent = Nazm_tspagent
                    .Where(B => B.inno == InputParams.Inno);
            }

            if (InputParams.Bpc != null)
            {
                Nazm_tspagent = Nazm_tspagent
                    .Where(B => B.bpc == InputParams.Bpc);
            }

            if (InputParams.PolicyNo != null)
            {
                Nazm_tspagent = Nazm_tspagent
                    .Where(B => B.Policy_No == InputParams.PolicyNo);
            }

            if (InputParams.DimProductId != 0)
            {
                Nazm_tspagent = Nazm_tspagent
                    .Where(B => B.DimProduct_Id == InputParams.DimProductId);
            }

            if (InputParams.NetsaleId != 0)
            {
                Nazm_tspagent = Nazm_tspagent
                    .Where(B => B.Netsale_Id == InputParams.NetsaleId);
            }

            if (InputParams.InvoiceModelId != 0)
            {
                Nazm_tspagent = Nazm_tspagent
                    .Where(B => B.InvoiceModel_Id == InputParams.InvoiceModelId);
            }

            if (InputParams.Status == StatusEnum.SUCCESS)
            {
                Nazm_tspagent = Nazm_tspagent
                    .Where(B => B.Status == StatusEnum.SUCCESS.ToString());
            }

            if (InputParams.Status == StatusEnum.FAILED)
            {
                Nazm_tspagent = Nazm_tspagent
                    .Where(B => B.Status == StatusEnum.FAILED.ToString());
            }

            if (InputParams.Status == StatusEnum.SENDING)
            {
                Nazm_tspagent = Nazm_tspagent
                   .Where(C => C.TaxId == null);
            }

            if (InputParams.Status == StatusEnum.PENDING)
            {
                Nazm_tspagent = Nazm_tspagent
                  .Where(C => C.Status == StatusEnum.PENDING.ToString());
            }

            if (InputParams.Status == StatusEnum.CANCEL)
            {
                Nazm_tspagent = Nazm_tspagent
                  .Where(C => C.ins == 3);
            }

            Nazm_tspagent = Nazm_tspagent.Where(C => C.indatim.Value >= InputParams.FromDate && C.indatim.Value <= InputParams.ToDate);

            Nazm_tspagent = Nazm_tspagent.OrderBy(C => C.indatim).Take(InputParams.PageSize);

            var result = Nazm_tspagent.OrderByDescending(I => I.indatim).AsNoTracking();

            return result;

        }

    }
}
