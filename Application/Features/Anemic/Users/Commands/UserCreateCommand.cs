using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Nazm.Results;
using ViewModels.Users;

namespace Application.Features.Anemic.Users.Commands
{
    public class UserCreateCommand : IRequest<Result<UserManageCreateViewModel>>
    {
        public UserManageCreateViewModel ViewModel { get;  set; }
    }

    public class UserRegisterCommandHandler : BaseRequestHandler<UserCreateCommand, Result<UserManageCreateViewModel>>
    {
        private readonly UserManager<User> _userManager;

        public UserRegisterCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<User> userManager
            ) : base(unitOfWork, mapper)
        {
            _userManager = userManager;
        }

        protected override async Task<Result<UserManageCreateViewModel>> HandleRequestAsync(UserCreateCommand input, CancellationToken cancellationToken)
        {
            var errorList = new List<string>();
            var response = new FluentResults.Result<UserManageCreateViewModel>();
            var user = new User()
            {
                FirstName = input.ViewModel.FirstName,
                LastName = input.ViewModel.LastName,
                NationalCode = input.ViewModel.NationalCode,
                Gender = input.ViewModel.Gender,
                Email = input.ViewModel.Email,
                IsActive = true,

                UserName = input.ViewModel.UserName,
            };
            var isUnique = await _unitOfWork.Users.IsUnique(user);
            if (isUnique.IsSuccess)
            {
                var result = await _userManager.CreateAsync(user, input.ViewModel.Password);

                if (result.Succeeded)
                {
                    return response
                        .WithValue(input.ViewModel)
                        .ConvertToDtatResult();
                }
                else
                {
                    
                    foreach (var error in result.Errors)
                    {
                        errorList.Add(error.Description);
                    }
                    return response
                        .WithErrors(errorList)
                        .ConvertToDtatResult();
                }
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
