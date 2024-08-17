using Application.Features.Anemic.FileOperations.Queries;
using Application.Features.Anemic.Invoices.Commands;
using Application.Features.Anemic.Invoices.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Invoices;
using ViewModels.Shared;

namespace Api.Controllers
{
    public class InquiresController : ApiControllerBase
    {


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InvoiceUpdateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new InquiryInvoiceUpdateCommand {  InvoiceViewModel  = viewModel }, cancellationToken);

            if (result.IsSuccess)
                return Ok(result);
            else
                return NotFound(result);
        }

    }
}
