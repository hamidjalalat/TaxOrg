using MediatR;
using AutoMapper;
using Nazm.Results;
using Domain.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Application.Common.Interfaces.Repository.Anemic.Base;

namespace Application.Features.Anemic.FileOperations.Commands
{
    public class FileUploadCommand : BaseRequest, IRequest<Result<string>>
    {
        public FileUploadCommand(IFormFile file, string controllerName)
        {
            File = file;
            ControllerName = controllerName;
        }
        public IFormFile File { get; set; }
        public string ControllerName { get; set; }
    }

    public class FileUploadCommandHandler : BaseRequestHandler<FileUploadCommand, Result<string>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _environment;

        public FileUploadCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment environment,
            RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
            _environment = environment;
        }

        protected override async Task<Result<string>> HandleRequestAsync(FileUploadCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<string>();
            List<string> errorList = new List<string>();

            var extension = Path.GetExtension(input.File.FileName);

            #region Check File

            if (input.File.Length == 0)
            {
                errorList.Add(Resources.Messages.Validations.FileZero);
            }

            decimal fileMaxSize = PublicConstants.FileMaxSize;
            string byteType = Resources.DataDictionary.ByteKilo;
            if (PublicConstants.FileMaxSize > PublicConstants.ByteMega)
            {
                fileMaxSize = (PublicConstants.FileMaxSize / (PublicConstants.ByteMega));
                byteType = Resources.DataDictionary.ByteMega;
            }
            if (input.File.Length > PublicConstants.FileMaxSize)
            {
                errorList.Add(string.Format(Resources.Messages.Validations.FileMaxSize, fileMaxSize, byteType));
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
                var newFileName = $"{Guid.NewGuid()}{extension}";

                var directoryPath = Path.Combine(_environment.ContentRootPath, PublicConstants.wwwrootFolder, $"{PublicConstants.UploadsFolder}\\{input.ControllerName}");
                var fullPath = Path.Combine(directoryPath, newFileName);
                var resultPath = Path.Combine($"{PublicConstants.UploadsFolder}\\{input.ControllerName}", newFileName);

                bool exists = Directory.Exists(directoryPath);
                if (exists == false)
                    Directory.CreateDirectory(directoryPath);

                using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    await input.File.CopyToAsync(fileStream, cancellationToken);
                }

                return response
                        .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.DataDictionary.FileUpload} '{input.File.FileName}'"))
                        .WithValue(resultPath)
                        .ConvertToDtatResult();
            }
        }
    }
}
