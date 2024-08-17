using Application.Features.Anemic.FileOperations.Queries;
using Application.Features.Anemic.DimProducts.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.DimProducts;
using ViewModels.Shared;

namespace Api.Controllers
{
    public class DimProductsController : ApiControllerBase
    {

        [HttpPost("FetchActive")]
        public async Task<IActionResult> FetchActive(PublicViewModel publicViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new DimProductGetActiveQuery(publicViewModel), cancellationToken);

            return Ok(result);
        }

    }
}
