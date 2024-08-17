using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.MenuControllerActions;

namespace Application.Features.Anemic.MenuControllerActions.Queries
{

    public class MenuGetItemQuery : BaseRequest, IRequest<Result<List<MenuControllerActionViewModel>>>
    {
        public MenuGetItemQuery()
        {

        }
        public MenuGetItemQuery(int menuId)
        {
           MenuId=menuId;
        }
        public int MenuId { get; set; }
       

    }

    public class MenuGetItemQueryHandler : BaseRequestHandler<MenuGetItemQuery, Result<List<MenuControllerActionViewModel>>>
    {
        private readonly IMenuControllerActionRepository _MenuControllerActionRepository;


        public MenuGetItemQueryHandler(
            IMenuControllerActionRepository MenuControllerActionRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _MenuControllerActionRepository = MenuControllerActionRepository;
        }

        protected async override Task<Result<List<MenuControllerActionViewModel>>> HandleRequestAsync(MenuGetItemQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<List<MenuControllerActionViewModel>>();
            var response = _unitOfWork.MenuControllerActions.GetAll
                .Where(s => s.MenuController.MenuId == input.MenuId)
                .Include(s => s.ActionMethod)
                .Include(s => s.MenuController).ThenInclude(s => s.Controller)
                .AsNoTracking().ToListAsync(cancellationToken);

            var MenuControllerActionViewModel = _mapper.Map<List<MenuControllerActionViewModel>>(response);
            return result.WithValue(MenuControllerActionViewModel).ConvertToDtatResult();
        }
    }
}
