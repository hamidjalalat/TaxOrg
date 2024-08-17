
using Application.Features.Anemic.MenuControllerActions.Commands;
using Application.Features.Anemic.MenuControllerActions.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.MenuControllerActions;
using ViewModels.Shared;

namespace Api.Controllers
{
    public class MenuPageActionsController : ApiControllerBase
    {

       
        [HttpPost("FetchAllJoin")]
        public async Task<IActionResult> FetchAllJoin(PublicViewModel publicViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new MenuControllerActionGetAllJoinQuery(publicViewModel), cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MenuControllerActionViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new MenuControllerActionCreateCommand { MenuControllerActionViewModel = viewModel }, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MenuControllerActionViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            if (viewModel.MenuControllerActionId < 1)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new MenuControllerActionUpdateCommand { MenuControllerActionViewModel = viewModel }, cancellationToken);

            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id, CancellationToken cancellationToken)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new MenuControllerActionDeleteCommand(id), cancellationToken);

            return Ok(result);
        }
    }
}
