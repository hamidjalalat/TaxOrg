using Application.Features.Anemic.FileOperations.Queries;
using Application.Features.Anemic.InvoiceModels.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.InvoiceModels;
using ViewModels.Shared;

namespace Api.Controllers
{
    public class InvoiceModelsController : ApiControllerBase
    {

        [HttpPost("FetchActive")]
        public async Task<IActionResult> FetchActive(PublicViewModel publicViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new InvoiceModelGetActiveQuery(publicViewModel), cancellationToken);

            return Ok(result);
        }

    }
}
