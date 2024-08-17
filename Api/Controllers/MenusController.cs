
using Microsoft.AspNetCore.Mvc;
using Application.Features.Anemic.Menus.Queries;
using Application.Features.Anemic.Menus.Commands;
using ViewModels.Menus;
using ViewModels.Shared;
using Application.Features.Anemic.FileOperations.Queries;

namespace Api.Controllers
{
    public class MenusController : ApiControllerBase
    {
        [HttpPost("FetchChilds")]
        public async Task<IActionResult> FetchChilds(PublicViewModel publicViewModel, CancellationToken cancellationToken)
        {
            if (publicViewModel == null || publicViewModel.MenuId <= 0)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new MenuGetChildQuery(publicViewModel), cancellationToken);
            return Ok(result);
        }

        [HttpPost("FetchAll")]
        public async Task<IActionResult> FetchAll(PublicViewModel inputParamsViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new MenuGetAllQuery(inputParamsViewModel), cancellationToken);
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

        [HttpGet("FindById/{id}")]
        public async Task<IActionResult> FindById(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new MenuFindByIdQuery(id), cancellationToken);
            return Ok(result);
        }

        [HttpGet("GetUserMenus")]
        public async Task<IActionResult> GetUserMenus( CancellationToken cancellationToken)
        {
           
            var result = await Mediator.Send(new MenuGetByUserIdQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MenuCreateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new MenuCreateCommand { MenuViewModel = viewModel }, cancellationToken);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MenuUpdateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            if (viewModel.MenuId < 1)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new MenuUpdateCommand { MenuUpdateViewModel = viewModel }, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(int id, CancellationToken cancellationToken)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new MenuDeleteCommand(id),cancellationToken);

            return Ok(result);
        }
    }
}
