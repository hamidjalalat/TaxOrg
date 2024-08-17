using MediatR;
using AutoMapper;
using Nazm.Results;
using Domain.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Application.Common.Interfaces.Repository.Anemic.Base;

namespace Application.Features.Anemic.FileOperations.Commands
{
    public class FileDeleteCommand : BaseRequest, IRequest<Result<bool>>
    {
        public FileDeleteCommand(string fileUrl)
        {
            FileUrl = fileUrl;
        }

        public string FileUrl { get; set; }
    }

    public class FileDeleteCommandHandler : BaseRequestHandler<FileDeleteCommand, Result<bool>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _environment;

        public FileDeleteCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment environment,
            RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
            _environment = environment;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(FileDeleteCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<bool>();
            List<string> errorList = new List<string>();
            bool result = false;

            if (string.IsNullOrWhiteSpace(input.FileUrl))
            {
                errorList.Add(Resources.Messages.Validations.FileEmpty);
            }

            if (errorList.Count > 0)
            {
                return response
                        .WithErrors(errorList)
                        .ConvertToDtatResult();
            }
            else
            {
                await Task.Run(() =>
                {
                    var directoryPath = Path.Combine(_environment.ContentRootPath, PublicConstants.wwwrootFolder);
                    var fullPath = Path.Combine(directoryPath, input.FileUrl);

                    bool exists = File.Exists(fullPath);
                    if (exists)
                        File.Delete(fullPath);
                    result = true;
                });

                return response
                        .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.DataDictionary.FileDelete}"))
                        .WithValue(result)
                        .ConvertToDtatResult();
            }
        }
    }
}
