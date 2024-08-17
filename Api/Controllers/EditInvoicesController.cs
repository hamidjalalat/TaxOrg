using Application.Features.Anemic.FileOperations.Queries;
using Application.Features.Anemic.Invoices.Commands;
using Application.Features.Anemic.Invoices.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Invoices;
using ViewModels.Shared;

namespace Api.Controllers
{
    public class EditInvoicesController : ApiControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InvoiceCreateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new EditInvoiceCreateCommand { InvoiceViewModel = viewModel }, cancellationToken);

            if (result.IsSuccess)
                return Ok(result);
            else
                return NotFound(result);
        }

    }
}
