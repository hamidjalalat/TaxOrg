using Application.Features.Anemic.Users.Commands;
using Application.Features.Anemic.UserRoles.Queries;
using Microsoft.AspNetCore.Mvc;
using ViewModels.UserRoles;
using Application.Features.Anemic.FileOperations.Queries;

namespace Api.Controllers
{
    public class UserRolesController : ApiControllerBase
    {
        [HttpPost("FetchByRoleId")]
        public async Task<IActionResult> FetchByRoleId(UserRoleInputParamsViewModel inputParamsViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new UserRoleGetUsersQuery(inputParamsViewModel), cancellationToken);
            if (result.IsSuccess && inputParamsViewModel.FileExportType != null)
            {
                if (inputParamsViewModel.FileExportType == Domain.Enums.FileExportTypeEnum.Office_Excel)
                {
                    var resultExcelExport = await Mediator.Send(new FileDownloadExcelExportQuery(result.Value, inputParamsViewModel.FileExportColumns), cancellationToken);
                    return Ok(resultExcelExport);
                }
            }

            return Ok(result);
        }


        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(UserRoleCreateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
                return NotFound();

            var result = await Mediator.Send(new UserAddToRoleCommand { ViewModel = viewModel }, cancellationToken);

            return Ok(result);
        }

        [HttpPost("DeleteUserFromRole")]
        public async Task<IActionResult> DeleteUserFromRole(UserRoleDeleteViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
                return NotFound();

            var result = await Mediator.Send(new UserDeleteFromRoleCommand { ViewModel = viewModel },cancellationToken);

            return Ok(result);
        }
    }
}
