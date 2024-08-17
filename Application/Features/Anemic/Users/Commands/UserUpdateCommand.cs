using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;
using ViewModels.Users;

namespace Application.Features.Anemic.Users.Commands
{

    public class UserUpdateCommand : IRequest<Result<UserManageUpdateViewModel>>
    {
        public UserManageUpdateViewModel ViewModel { get; set; }
       
    }

    public class UserUpdateCommandHandler : BaseRequestHandler<UserUpdateCommand, Result<UserManageUpdateViewModel>>
    {
        private readonly UserManager<User> _userManager;

        public UserUpdateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<User> userManager
            ) : base(unitOfWork, mapper)
        {
            _userManager = userManager;
        }

        protected override async Task<Result<UserManageUpdateViewModel>> HandleRequestAsync(UserUpdateCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<UserManageUpdateViewModel>();

            var user = await _userManager.FindByIdAsync(input.ViewModel.Id);
            var entity = _mapper.Map(input.ViewModel, user);
            var isUnique = await _unitOfWork.Users.IsUnique(user);
            if (isUnique.IsSuccess)
            {
                var result = await _userManager.UpdateAsync(entity);

                if (result.Succeeded)
                {
                    return response
                        .WithSuccess(Resources.Messages.Successes.SuccessUpdate)
                        .ConvertToDtatResult();
                }
                List<string> errorList = new List<string>();
                foreach (var error in result.Errors)
                {
                    errorList.Add(error.Description);
                }

                return response
                    .WithErrors(errorList)
                    .ConvertToDtatResult();
            }
            else
            {
                return response
                    .WithErrors(isUnique.Errors)
                    .ConvertToDtatResult();
            }
           
        }
    }
}
