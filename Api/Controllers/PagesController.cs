using Api.Common;
using Application.Features.Anemic.Controllers.Commands;
using Application.Features.Anemic.Controllers.Queries;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ViewModels.Controllers;
using ViewModels.Shared;

namespace Api.Controllers
{
    [Display(ShortName ="sasas")]
    public class PagesController : ApiControllerBase
    {
        [HttpPost("Fetch")]
        public async Task<IActionResult> Fetch(PublicViewModel publicViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new ControllerGetAllQuery(publicViewModel), cancellationToken);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post(ControllerViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new CreateControllerCommand { ControllerViewModel = viewModel }, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ControllerViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            if (viewModel.ControllerId < 1)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new ControllerUpdateCommand { ControllerViewModel = viewModel }, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new DeleteControllerCommand(id), cancellationToken);

            return Ok(result);
        }

        [HttpPost("SyncAll")]
        public async Task<IActionResult> SyncAll( CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new ControllerSyncAllCommand(Settings.GetAllControllersAndTheirActions()),cancellationToken);
            return Ok();
        }

    }

}
