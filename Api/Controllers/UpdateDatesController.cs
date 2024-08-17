using Application.Features.Anemic.FileOperations.Queries;
using Application.Features.Anemic.Invoices.Commands;
using Application.Features.Anemic.Invoices.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Invoices;
using ViewModels.Nazm_tspagents;
using ViewModels.Shared;

namespace Api.Controllers
{
    public class UpdateDatesController : ApiControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UpdateDateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            UpdateDateViewModel updateDateViewModel = viewModel as UpdateDateViewModel;
            viewModel.ReduceDay = 21;

            var result = await Mediator.Send(new UpdateDateCommand { InvoiceViewModel = viewModel }, cancellationToken);


            return Ok(result);
        }

    }
}
