using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using MediatR;
using Nazm.Results;
using ViewModels.Menus;

namespace Application.Features.Anemic.Menus.Queries
{
    
    public class MenuFindByIdQuery : BaseRequest, IRequest<Result<MenuViewModel>>
    {
        public MenuFindByIdQuery()
        {

        }
        public MenuFindByIdQuery(int id)
        {

            Id = id;
        }
        public int Id { get; set; }

    }

    public class MenuFindByIdQueryHandler : BaseRequestHandler<MenuFindByIdQuery, Result<MenuViewModel>>
    {
        private readonly IMenuRepository _MenuRepository;


        public MenuFindByIdQueryHandler(
            IMenuRepository MenuRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _MenuRepository = MenuRepository;
        }

        protected async override Task<Result<MenuViewModel>> HandleRequestAsync(MenuFindByIdQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<MenuViewModel>();
            var response = await _unitOfWork.Menus.FindByIdAsync(input.Id, cancellationToken);
            var MenuViewModel = _mapper.Map<MenuViewModel>(response);
            return result.WithValue(MenuViewModel).ConvertToDtatResult();
        }
    }
}
