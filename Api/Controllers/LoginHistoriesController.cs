using Application.Features.Anemic.LoginHistories.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.LoginHistories;
using Application.Features.Anemic.FileOperations.Queries;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LoginHistoriesController : ApiControllerBase
	{
		[HttpPost("FetchAll")]
		public async Task<IActionResult> FetchAll(LoginHistoryInputParamsViewModel inputParamsViewModel, CancellationToken cancellationToken)
		{
			var result = await Mediator.Send(new LoginHistoryGetQuery(inputParamsViewModel),cancellationToken);
            if (result.IsSuccess && inputParamsViewModel.FileExportType != null)
            {
                if (inputParamsViewModel.FileExportType == Domain.Enums.FileExportTypeEnum.Office_Excel)
                {
                    var resultExcelExport = await Mediator.Send(new FileDownloadExcelExportQuery(result.Value.Items, inputParamsViewModel.FileExportColumns), cancellationToken);
                    return Ok(resultExcelExport);
                }
            }

            return Ok(result);
		}

	}
}
