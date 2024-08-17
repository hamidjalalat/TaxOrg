using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using ViewModels.Shared;
using ViewModels.Users;

namespace Application.Features.Anemic.Users.Queries
{

    public class UserGetAllQuery : IRequest<Result<PaginatedList<UserManageViewModel>>>
    {
        public UserGetAllQuery()
        {

        }
        public UserGetAllQuery(PublicViewModel viewModel)
        {
			InputViewModel = viewModel;
		}
		public PublicViewModel InputViewModel { get; private set; }

	}

    public class UserGetAllQueryHandler : BaseRequestHandler<UserGetAllQuery, Result<PaginatedList<UserManageViewModel>>>
    {

        public UserGetAllQueryHandler(

            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {

        }

        protected async override Task<Result<PaginatedList<UserManageViewModel>>> HandleRequestAsync(UserGetAllQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<PaginatedList<UserManageViewModel>>();
            var response = _unitOfWork.Users.GetAll
                .AsNoTracking();

            var viewModel = await response
                .ProjectTo<UserManageViewModel>(_mapper.ConfigurationProvider, cancellationToken)
                     .PaginatedListAsync(input.InputViewModel.PageNumber, input.InputViewModel.PageSize, cancellationToken);

            return result.WithValue(viewModel).ConvertToDtatResult();
        }
    }
}
