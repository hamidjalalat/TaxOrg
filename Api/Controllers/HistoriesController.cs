using Application.Features.Anemic.Histories.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Histories;
using Application.Features.Anemic.FileOperations.Queries;

namespace Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HistoriesController : ApiControllerBase
	{
		[HttpPost("FetchAll")]
		public async Task<IActionResult> FetchAll(HistoryInputParamsViewModel inputParamsViewModel, CancellationToken cancellationToken)
		{
			var result = await Mediator.Send(new HistoryGetQuery(inputParamsViewModel), cancellationToken);
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

		[HttpGet("GetChanged")]
		public async Task<IActionResult> GetChanged(string model, int id, CancellationToken cancellationToken)
		{
			var result = await Mediator.Send(new HistoryGetChangedQuery(model, id), cancellationToken);

			return Ok(result);
		}
	}
}
