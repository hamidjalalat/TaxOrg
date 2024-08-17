using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Users;

namespace Application.Features.Anemic.Users.Queries
{

    public class UserGetByIdQuery : IRequest<Result<UserManageViewModel>>
    {
        private UserGetByIdQuery()
        {

        }
        public UserGetByIdQuery(string id)
        {
            UserId = id;
        }
        public string UserId { get; private set; }

    }

    public class UserGetByIdQueryHandler : BaseRequestHandler<UserGetByIdQuery, Result<UserManageViewModel>>
    {

        public UserGetByIdQueryHandler(

            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {

        }

        protected async override Task<Result<UserManageViewModel>> HandleRequestAsync(UserGetByIdQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<UserManageViewModel>();
            var response = await _unitOfWork.Users.FindByIdAsync(input.UserId, cancellationToken);
            var viewModel = _mapper.Map<UserManageViewModel>(response);
            return result.WithValue(viewModel).ConvertToDtatResult();
        }
    }
}
