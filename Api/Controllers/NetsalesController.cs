using Application.Features.Anemic.FileOperations.Queries;
using Application.Features.Anemic.Netsales.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Netsales;
using ViewModels.Shared;

namespace Api.Controllers
{
    public class NetsalesController : ApiControllerBase
    {

        [HttpPost("FetchActive")]
        public async Task<IActionResult> FetchActive(PublicViewModel publicViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new NetsaleGetActiveQuery(publicViewModel), cancellationToken);

            return Ok(result);
        }

    }
}
