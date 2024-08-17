using Application.Features.Anemic.FileOperations.Queries;
using Application.Features.Anemic.Users.Commands;
using Application.Features.Anemic.Users.Queries;
using AutoMapper;
using Domain.Anemic.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Shared;
using ViewModels.Users;

namespace Api.Controllers
{
    public class UserManagesController : ApiControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagesController(UserManager<User> userManager,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;

        }

        [HttpPost("FetchAll")]
        public async Task<IActionResult> FetchAll(PublicViewModel inputParamsViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new UserGetAllQuery(inputParamsViewModel), cancellationToken);
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
        public async Task<IActionResult> FindById(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var result = await Mediator.Send(new UserGetByIdQuery(id), cancellationToken);

            return Ok(result);
        }

        [HttpGet("FetchUserRoles")]
        public async Task<IActionResult> FetchUserRoles(string userId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new UserGetRolesQuery(userId), cancellationToken);

            return Ok(result);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] UserManageCreateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new UserCreateCommand { ViewModel = viewModel }, cancellationToken);

            return Ok(result);
        }

        [HttpPut("EditUser")]
        public async Task<IActionResult> EditUser([FromBody] UserManageUpdateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null)
            {
                return BadRequest();
            }
            if (string.IsNullOrEmpty(viewModel.Id))
            {
                return BadRequest();
            }

            var result = await Mediator.Send(new UserUpdateCommand { ViewModel = viewModel }, cancellationToken);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var result = await Mediator.Send(new UserDeleteCommand(id), cancellationToken);

            return Ok(result);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(UserManageChangePasswordViewModel changePasswordViewModel, CancellationToken cancellationToken)
        {
            if (changePasswordViewModel == null)
                return BadRequest();

            var result = await Mediator.Send(new UserChangePasswordCommand(changePasswordViewModel),cancellationToken);
            return Ok(result);
        }

        [HttpPost("ChangePasswordByAdmin")]
        public async Task<IActionResult> ChangePasswordByAdmin(UserManageChangePasswordByAdminViewModel changePasswordViewModel, CancellationToken cancellationToken)
        {
            if (changePasswordViewModel == null)
                return BadRequest();

            var result = await Mediator.Send(new UserAdminChangePasswordCommand(changePasswordViewModel),cancellationToken);
            return Ok(result);
        }

        [HttpPost("ActiveByAdmin")]
        public async Task<IActionResult> ActiveByAdmin(UserManageActiveByAdminViewModel activeViewModel, CancellationToken cancellationToken)
        {
            if (activeViewModel == null)
                return BadRequest();

            var result = await Mediator.Send(new UserActiveByAdminCommand(activeViewModel),cancellationToken);
            return Ok(result);
        }

        [HttpPost("FetchStatisticReport")]
        public async Task<IActionResult> FetchStatisticReport(UserManageStatisticReportInputParamsViewModel inputParamsViewModel, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new UserStatisticReportQuery(inputParamsViewModel),cancellationToken);
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


    }
}