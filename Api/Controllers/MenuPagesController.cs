
using Application.Features.Anemic.MenuControllers.Commands;
using Application.Features.Anemic.MenuControllers.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.MenuControllers;
using ViewModels.Shared;

namespace Api.Controllers
{

    public class MenuPagesController : ApiControllerBase
    {

        [HttpPost("FetchAll")]
        public async Task<IActionResult> FetchAll(PublicViewModel publicViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new MenuControllerGetAllQuery(publicViewModel), cancellationToken);

            return Ok(result);
        }

        [HttpPost("FetchAllJoin")]
        public async Task<IActionResult> FetchAllJoin(PublicViewModel publicViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new MenuControllerGetAllJoinQuery(publicViewModel), cancellationToken);

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post(MenuControllerViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new MenuControllerCreateCommand { MenuControllerViewModel = viewModel }, cancellationToken );
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MenuControllerViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            if (viewModel.MenuControllerId < 1)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new MenuControllerUpdateCommand { MenuControllerViewModel = viewModel }, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id, CancellationToken cancellationToken)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new MenuControllerDeleteCommand(id),cancellationToken);

            return Ok(result);
        }
    }
}
