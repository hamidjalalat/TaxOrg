using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;
using Domain.Anemic.Entities;

namespace Application.Features.Anemic.Roles.Commands
{
    public class RoleManageDeleteCommand : BaseRequest, IRequest<Result<bool>>
    {
        public RoleManageDeleteCommand()
        {
            
        }

        public RoleManageDeleteCommand(string id)
        {
            RoleId = id;
        }
        public string RoleId { get; set; }

    }

    public class RoleDeleteCommandHandler : BaseRequestHandler<RoleManageDeleteCommand, Result<bool>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleDeleteCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager) : base(unitOfWork, mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(RoleManageDeleteCommand input, CancellationToken cancellationToken)
        {
            bool result = false;
            var response = new FluentResults.Result<bool>();
            try
            {
                var role = await _roleManager.FindByIdAsync(input.RoleId);

                if (role != null)
                {
                    var users = await _userManager.GetUsersInRoleAsync(role.Name);

                    var claims = await _roleManager.GetClaimsAsync(role);

                    if (users.Count > 0 || claims.Count > 0)
                    {
                        return response
                            .WithError(Resources.Messages.Errors.DependentTables)
                            .ConvertToDtatResult();
                    }
                    else
                    {
                        await _roleManager.DeleteAsync(role);
                        response
                            .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.Buttons.Delete}"));
                    }
                }
                result = true;
            }
            catch (Exception)
            {
                result = false;
                throw;
            }
            return response
                .WithValue(result)
                .ConvertToDtatResult();

        }
    }
}
