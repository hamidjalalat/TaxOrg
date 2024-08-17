
using Application.Features.Anemic.ActionMethods.Commands;
using Application.Features.Anemic.ActionMethods.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.ActionMethods;
using ViewModels.Shared;

namespace Api.Controllers
{
    public class ActionMethodsController : ApiControllerBase
    {
        [HttpPost("FetchAll")]
        public async Task<IActionResult> FetchAll(PublicViewModel publicViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new ActionMethodGetAllQuery(publicViewModel), cancellationToken);

            return Ok(result);
        }

        [HttpPost("FetchAllJoin")]
        public async Task<IActionResult> FetchAllJoin(PublicViewModel publicViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new ActionMethodGetAllJoinQuery(publicViewModel), cancellationToken);

            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new ActionMethodGetByIdQuery(id), cancellationToken);

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post(ActionMethodViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new ActionMethodCreateCommand { ActionMethodViewModel = viewModel }, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ActionMethodViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            if (viewModel.ActionMethodId < 1)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new ActionMethodUpdateCommand { ActionMethodViewModel = viewModel },cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id, CancellationToken cancellationToken)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new ActionMethodDeleteCommand(id),cancellationToken);

            return Ok(result);
        }
    }
}
