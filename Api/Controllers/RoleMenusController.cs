using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Features.Anemic.RoleMenus.Commands;
using Application.Features.Anemic.RoleMenus.Queries;
using Application.Features.Anemic.Roles.Commands;
using Application.Features.Anemic.Roles.Queries;
using Application.Features.Anemic.Users.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ViewModels.RoleMenus;
using ViewModels.Shared;

namespace Api.Controllers
{
    public class RoleMenusController : ApiControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleMenusController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet("FetchByRoleId")]
        public async Task<IActionResult> FetchByRoleId(string roleId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new RoleMenuGetQuery(roleId), cancellationToken);

            return Ok(result);
        }

        [HttpPost("AddRoleToMenu")]
        public async Task<IActionResult> AddRoleToMenu(RoleMenuCreateViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null) return NotFound();
            var result = await Mediator.Send(new RoleMenuCreateCommand { RoleViewModel = viewModel }, cancellationToken);

            return Ok(result);
        }

        [HttpPost("DeleteRoleMenu")]
        public async Task<IActionResult> DeleteRoleMenu(RoleMenuDeleteViewModel viewModel, CancellationToken cancellationToken)
        {
            if (viewModel == null) return NotFound();
            var result = await Mediator.Send(new RoleMenuDeleteCommand { RoleViewModel = viewModel },cancellationToken);

            return Ok(result);
        }        
    }
}