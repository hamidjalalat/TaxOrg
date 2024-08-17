using Application.Features.Anemic.FileOperations.Queries;
using Application.Features.Anemic.Nazm_tspagents.Commands;
using Application.Features.Anemic.Nazm_tspagents.Queries;
using Application.Features.Anemic.RegulationGroups.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Nazm_tspagents;
using ViewModels.Shared;

namespace Api.Controllers
{
    public class Nazm_tspagentsController : ApiControllerBase
    {

        [HttpPost("FetchAll")]
        public async Task<IActionResult> FetchAll(PublicViewModel inputParamsViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new Nazm_tspagentGetAllQuery(inputParamsViewModel), cancellationToken);

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

        [HttpPost("FetchAllByFilterNazm_tspagent")]
        public async Task<IActionResult> FetchAllByFilterNazm_tspagent(Nazm_tspagentInputParamsViewModel inputParamsViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new Nazm_tspagentGetAllByFilterQuery(inputParamsViewModel), cancellationToken);

            return Ok(result);
        }

        [HttpPost("GetCountInvoice")]
        public async Task<IActionResult> GetCountInvoice([FromBody] Nazm_tspagentInputDateInvoiceViewModel inputParamsViewModel, CancellationToken cancellationToken)
        {
            var CountInvoice = await Mediator.Send(new Nazm_tspagentGetCountInvoiceQuery(inputParamsViewModel), cancellationToken);

         
            return Ok(CountInvoice);
        }

        [HttpPost("GetCountInvoiceCancel")]
        public async Task<IActionResult> GetCountInvoiceCancel([FromBody] Nazm_tspagentInputDateInvoiceViewModel inputParamsViewModel, CancellationToken cancellationToken)
        {
            var CountInvoiceCancel = await Mediator.Send(new Nazm_tspagentGetCountInvoiceCancelQuery(inputParamsViewModel), cancellationToken);

            return Ok(CountInvoiceCancel);
        }

        [HttpPost("GetCountInvoicePending")]
        public async Task<IActionResult> GetCountInvoicePending([FromBody] Nazm_tspagentInputDateInvoiceViewModel inputParamsViewModel, CancellationToken cancellationToken)
        {
            var CountInvoicePending = await Mediator.Send(new Nazm_tspagentGetCountInvoicePendingQuery(inputParamsViewModel), cancellationToken);

            return Ok(CountInvoicePending);
        }


        [HttpPost("GetCountInvoiceSending")]
        public async Task<IActionResult> GetGetCountInvoiceSending([FromBody] Nazm_tspagentInputDateInvoiceViewModel inputParamsViewModel, CancellationToken cancellationToken)
        {
            var CountInvoiceSending = await Mediator.Send(new Nazm_tspagentGetCountInvoiceSendingQuery(inputParamsViewModel), cancellationToken);

            return Ok(CountInvoiceSending);
        }

        [HttpPost("GetCountInvoiceSuccess")]
        public async Task<IActionResult> GetCountInvoiceSuccess([FromBody] Nazm_tspagentInputDateInvoiceViewModel inputParamsViewModel, CancellationToken cancellationToken)
        {
            var CountInvoiceSuccess = await Mediator.Send(new Nazm_tspagentGetCountInvoiceSuccessQuery(inputParamsViewModel), cancellationToken);

            return Ok(CountInvoiceSuccess);
        }

        [HttpPost("GetCountInvoiceFailed")]
        public async Task<IActionResult> GetCountInvoiceFailed([FromBody] Nazm_tspagentInputDateInvoiceViewModel inputParamsViewModel, CancellationToken cancellationToken)
        {
            var CountInvoiceFailed = await Mediator.Send(new Nazm_tspagentGetCountInvoiceFailedQuery(inputParamsViewModel), cancellationToken);

            return Ok(CountInvoiceFailed);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Nazm_tspagentCreateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new Nazm_tspagentCreateCommand { Nazm_tspagentViewModel = viewModel }, cancellationToken);

            return Ok(result);
        }


        [HttpPost("EditForSend")]
        public async Task<IActionResult> EditForSend(Nazm_tspagentEditForSendViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new Nazm_tspagentEditForSendCommand { Nazm_tspagentEditForSendViewModel = viewModel }, cancellationToken);

            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Nazm_tspagentUpdateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            if (viewModel.id < 1)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new Nazm_tspagentUpdateCommand { Nazm_tspagentViewModel = viewModel }, cancellationToken);

            return Ok(result);
        }


        [HttpPost("CancelById")]
        public async Task<IActionResult> CancelById(int id, CancellationToken cancellationToken)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new Nazm_tspagentCancelCommand(id), cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id, CancellationToken cancellationToken)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new Nazm_tspagentDeleteCommand(id), cancellationToken);

            return Ok(result);
        }


        [HttpGet("FindById/{id}")]
        public async Task<IActionResult> FindById(int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new Nazm_tspagentFindByIdQuery(id), cancellationToken);

            return Ok(result);
        }

    }
}
