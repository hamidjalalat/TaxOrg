using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;

namespace Application.Features.Anemic.FileOperations.Queries
{
    public class FileDownloadQuery : BaseRequest, IRequest<Result<byte[]>>
    {
        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        public string? FileContentType { get; set; }
    }

    public class FileDownloadQueryHandler : BaseRequestHandler<FileDownloadQuery, Result<byte[]>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _environment;

        public FileDownloadQueryHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IWebHostEnvironment environment,
            RoleManager<IdentityRole> roleManager) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
            _environment = environment;
        }

        protected override async Task<Result<byte[]>> HandleRequestAsync(FileDownloadQuery input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<byte[]>();
            List<string> errorList = new List<string>();

            if (errorList.Count > 0)
            {
                return response
                        .WithErrors(errorList)
                        .ConvertToDtatResult();
            }
            else
            {
                var directoryPath = Path.Combine(_environment.ContentRootPath, Domain.Constants.PublicConstants.wwwrootFolder);
                var fullPath = Path.Combine(directoryPath, input.FilePath);

                var bytes = new byte[0];
                var memoryStream = new MemoryStream();
                using (var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                //using (var fileStream = new FileStream(fullPath, FileMode.Open))
                {
                    await fileStream.CopyToAsync(memoryStream);
                    bytes = memoryStream.ToArray();
                }
                memoryStream.Position = 0;
                return response
                        .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.DataDictionary.FileDownload} '{input.FileName}'"))
                        .WithValue(bytes)
                        .ConvertToDtatResult();
            }
        }
    }
}
