using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using ClosedXML.Excel;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;
using System.Data;

namespace Application.Features.Anemic.FileOperations.Queries
{
    public class FileDownloadExcelExportQuery : BaseRequest, IRequest<Result<byte[]>>
    {        
        public FileDownloadExcelExportQuery(dynamic items, IList<ViewModels.ExcelExportColumns>? columns)
        {
            Items = items;
            Columns = columns;
        }

        public dynamic Items { get; set; }
        //public Type ItemType { get; set; }

        public IList<ViewModels.ExcelExportColumns>? Columns { get; set; }
    }

    public class FileDownloadExcelExportQueryHandler : BaseRequestHandler<FileDownloadExcelExportQuery, Result<byte[]>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _environment;

        public FileDownloadExcelExportQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment environment,
            RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
            _environment = environment;
        }

        protected override async Task<Result<byte[]>> HandleRequestAsync(FileDownloadExcelExportQuery input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<byte[]>();
            List<string> errorList = new List<string>();
            var bytes = new byte[0];

            try
            {
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("ExportSheet");

                ListToDataTableConversion lstData = new ListToDataTableConversion();
                DataTable dtData = await lstData.ToDataTableAsync(input.Items);

                int rowIndex = 1;
                int colIndex = 1;

                // Columns
                //foreach (DataColumn col in dtData.Columns)
                //{
                //    System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("Resources.DataDictionary", typeof(Resources.DataDictionary).Assembly);
                //    string resourceValue = resourceManager.GetString(col.ColumnName);

                //    worksheet.Cell(rowIndex, colIndex).Value = resourceValue;
                //    //worksheet.Cell(rowIndex, colIndex).Style.Fill.SetBackgroundColor(XLColor.AliceBlue);
                //    colIndex++;
                //}

                HashSet<string> lstUniqueColumns = new HashSet<string>();
                foreach (var col in input.Columns)
                {
                    System.Resources.ResourceManager resourceManager = new System.Resources.ResourceManager("Resources.DataDictionary", typeof(Resources.DataDictionary).Assembly);
                    string resourceValue = resourceManager.GetString(col.ColumnName);

                    if (lstUniqueColumns.Where(q => q == resourceValue).Count() == 0)
                    {
                        lstUniqueColumns.Add(resourceValue);
                        worksheet.Cell(rowIndex, colIndex).Value = resourceValue;
                    }
                    else
                    {
                        errorList.Add(string.Format(Resources.Messages.Validations.FileExcelColumnDuplicate, $"[{col} : {resourceValue}]"));
                        return response
                            .WithErrors(errorList)
                            .ConvertToDtatResult();
                    }

                    colIndex++;
                }

                int worksheetCount = worksheet.CellsUsed().Count();
                int columnsCount = lstUniqueColumns.Count;
                if (worksheetCount != columnsCount)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.FileExcelColumnsCount));
                    return response
                        .WithErrors(errorList)
                        .ConvertToDtatResult();
                }

                // Rows
                foreach (DataRow row in dtData.Rows)
                {
                    rowIndex++;
                    colIndex = 1;
                    //foreach (DataColumn col in dtData.Columns)
                    //{
                    //    worksheet.Cell(rowIndex, colIndex).Value = row[col.ColumnName].ToString().Trim();
                    //    colIndex++;
                    //}
                    foreach (var col in input.Columns)
                    {
                        string? colValue = row[col.ColumnName].ToString()?.Trim();
                        if (col.IsSeparatedDigits)
                        {
                            colValue = setSeparatedDigits(colValue);
                        }

                        worksheet.Cell(rowIndex, colIndex).Value = colValue;
                        colIndex++;
                    }
                }

                //var table = worksheet.Range("A1:O7").CreateTable();
                var table = worksheet.Range(worksheet.FirstCellUsed(), worksheet.LastCellUsed()).CreateTable();
                table.Theme = XLTableTheme.TableStyleMedium2;

                using (var memoryStream = new MemoryStream())
                {
                    memoryStream.Position = 0;
                    workbook.SaveAs(memoryStream);
                    bytes = memoryStream.ToArray();
                }
            }
            catch (Exception ex)
            {
                errorList.Add(string.Format(Resources.Messages.Errors.Error, $"{Resources.DataDictionary.FileDownload}"));
            }

            if (errorList.Count > 0)
            {
                return response
                        .WithErrors(errorList)
                        .ConvertToDtatResult();
            }
            else
            {
                return response
                        .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.DataDictionary.FileDownload}"))
                        .WithValue(bytes)
                        .ConvertToDtatResult();
            }
        }

        private string? setSeparatedDigits(string? propertyValue)
        {
            string? strResult = propertyValue;

            if (propertyValue != null)
            {
                int intValue;
                long longValue;
                decimal decimalValue;
                if (int.TryParse(propertyValue, out intValue))
                {
                    strResult = string.Format("{0:N0}", intValue);
                }
                else if (long.TryParse(propertyValue, out longValue))
                {
                    strResult = string.Format("{0:N0}", longValue);
                }
                else if (decimal.TryParse(propertyValue, out decimalValue))
                {
                    strResult = string.Format("{0:N0}", decimalValue);
                }
            }

            return strResult;
        }
    }
}
