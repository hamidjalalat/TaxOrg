using Application.Features.Anemic.FileOperations.Queries;
using Application.Features.Anemic.Roles.Commands;
using Application.Features.Anemic.Roles.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModels.RoleManages;
using ViewModels.Shared;

namespace Api.Controllers
{
    public class RoleManagesController : ApiControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("FetchAll")]
        public async Task<IActionResult> FetchAll(PublicViewModel inputParamsViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new RoleGetAllQuery(inputParamsViewModel), cancellationToken);
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

        [HttpGet("FetchByRoleId/{id}")]
        public async Task<IActionResult> FetchByRoleId(string roleId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new RoleFindByIdQuery { Id = roleId }, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RoleManageCreateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new RoleManageCreateCommand { RoleViewModel = viewModel }, cancellationToken);
            return Ok(result);
        } 


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new RoleManageDeleteCommand(id), cancellationToken);

            return Ok(result);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RoleManageUpdateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new RoleManageUpdateCommand { RoleViewModel = viewModel }, cancellationToken);

            return Ok(result);
        }       
    }
}