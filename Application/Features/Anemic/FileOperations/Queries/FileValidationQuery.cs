using MediatR;
using AutoMapper;
using Nazm.Results;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Application.Common.Interfaces.Repository.Anemic.Base;
using ClosedXML.Excel;

namespace Application.Features.Anemic.FileOperations.Queries
{
    public class FileValidationQuery : BaseRequest, IRequest<Result<bool>>
    {
        public FileValidationQuery(IFormFile file, FileAllowedTypeEnum? fileType)
        {
            File = file;
            FileType = fileType;
        }
        public IFormFile File { get; set; }
        public FileAllowedTypeEnum? FileType { get; set; }
    }

    public class FileValidationQueryHandler : BaseRequestHandler<FileValidationQuery, Result<bool>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _environment;

        public FileValidationQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment environment,
            RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
            _environment = environment;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(FileValidationQuery input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<bool>();
            List<string> errorList = new List<string>();
            bool result = false;
            Stream? streamFile = null;
            string fileName = string.Empty;

            fileName = input.File.FileName;
            var extension = Path.GetExtension(fileName);

            #region Check File

            if (input.FileType == null)
            {
                errorList.Add(Resources.Messages.Validations.FileEmpty);
            }

            try
            {
                switch (input.FileType)
                {
                    case FileAllowedTypeEnum.Office_Excel:
                        {
                            streamFile = input.File.OpenReadStream();
                            var workbook = new XLWorkbook(streamFile);

                            var worksheet = workbook.Worksheet(1);
                            break;
                        }
                    case FileAllowedTypeEnum.Image:
                        {
                            System.Drawing.Image imgCheck;
                            streamFile = input.File.OpenReadStream();

                            imgCheck = System.Drawing.Image.FromStream(streamFile);
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                switch (input.FileType)
                {
                    case FileAllowedTypeEnum.Office_Excel:
                        {
                            errorList.Add($"{Resources.Messages.Validations.FileInvalid}, {string.Format(Resources.Messages.Validations.FileSelected, Resources.DataDictionary.Office_Excel)}");
                            break;
                        }
                    case FileAllowedTypeEnum.Image:
                        {
                            errorList.Add($"{Resources.Messages.Validations.FileInvalid}, {string.Format(Resources.Messages.Validations.FileSelected, Resources.DataDictionary.Image)}");
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            finally
            {
                await Task.Run(() =>
                {
                    streamFile?.Flush();
                    streamFile?.Close();
                    streamFile?.Dispose();
                });
            }

            #endregion

            if (errorList.Count > 0)
            {
                return response
                        .WithErrors(errorList)
                        .ConvertToDtatResult();
            }
            else
            {
                result = true;
                return response
                        .WithValue(result)
                        .ConvertToDtatResult();
            }
        }
    }
}
