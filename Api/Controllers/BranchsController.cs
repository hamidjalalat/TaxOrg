using Application.Features.Anemic.FileOperations.Queries;
using Application.Features.Anemic.Branchs.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Branchs;
using ViewModels.Shared;

namespace Api.Controllers
{
    public class BranchsController : ApiControllerBase
    {

        [HttpPost("FetchActive")]
        public async Task<IActionResult> FetchActive(PublicViewModel publicViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new BranchGetActiveQuery(publicViewModel), cancellationToken);

            return Ok(result);
        }

    }
}
