using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;

namespace Application.Features.Anemic.Users.Commands
{
    public class UserDeleteCommand : IRequest<Result<bool>>
    {
        public string Id { get; private set; }

        public UserDeleteCommand(string userId)
        {
            Id = userId;
        }
    }

    public class UserDeleteCommandHandler : BaseRequestHandler<UserDeleteCommand, Result<bool>>
    {
        private readonly UserManager<User> _userManager;

        public UserDeleteCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<User> userManager
            ) : base(unitOfWork, mapper)
        {
            _userManager = userManager;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(UserDeleteCommand input, CancellationToken cancellationToken)
        {
            bool result = false;
            var response = new FluentResults.Result<bool>();
            try
            {
                await _unitOfWork.BeginTransaction(cancellationToken);
                var user = await _userManager.FindByIdAsync(input.Id);

                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null && roles.Count > 0)
                    {
                        return response
                            .WithError(Resources.Messages.Errors.DependentTables)
                            .ConvertToDtatResult();
                    }
                    /*
                    var discoveryMarks = await _unitOfWork.DiscoveryMarks
                        .GetAll.Where(s => s.UserId == user.Id).ToListAsync(cancellationToken);

                    if (discoveryMarks != null && discoveryMarks.Count > 0)
                    {
                        return response
                            .WithError(Resources.Messages.Errors.DependentTables)
                            .ConvertToDtatResult();
                    }

                    var discoveryOpinion = await _unitOfWork.DiscoveryOpinions
                        .GetAll.Where(s => s.FromUserId == user.Id || s.ToUserId == user.Id).ToListAsync(cancellationToken);

                    if (discoveryOpinion != null && discoveryOpinion.Count > 0)
                    {
                        return response
                            .WithError(Resources.Messages.Errors.DependentTables)
                            .ConvertToDtatResult();
                    }

                    var discoveryUserActionType = await _unitOfWork.DiscoveryUserActionTypes
                        .GetAll.Where(s => s.UserId == user.Id).ToListAsync(cancellationToken);

                    if (discoveryUserActionType != null && discoveryUserActionType.Count > 0)
                    {
                        return response
                            .WithError(Resources.Messages.Errors.DependentTables)
                            .ConvertToDtatResult();
                    }

                    var regulationActionTypePermission = await _unitOfWork.RegulationActionTypePermissions
                        .GetAll.Where(s => s.UserId == user.Id).ToListAsync(cancellationToken);

                    if (regulationActionTypePermission != null && regulationActionTypePermission.Count > 0)
                    {
                        return response
                            .WithError(Resources.Messages.Errors.DependentTables)
                            .ConvertToDtatResult();
                    }
                    */

                    await _userManager.DeleteAsync(user);
                    response
                        .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.Buttons.Delete}"));
                }
                await _unitOfWork.Commit(cancellationToken,isDeleted: true);
                await _unitOfWork.CommitTransaction(cancellationToken);
                result = true;
            }
            catch (Exception)
            {
                result = false;
                await _unitOfWork.RollbackTransaction(cancellationToken);
                throw;
            }

            return response
                .WithValue(result)
                .ConvertToDtatResult();
        }
    }
}
