using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using ViewModels.ActionMethods;

namespace Application.Features.Anemic.ActionMethods.Queries
{

    public class ActionMethodGetByIdQuery : BaseRequest, IRequest<Result<ActionMethodViewModel>>
    {
        public ActionMethodGetByIdQuery()
        {

        }
        public ActionMethodGetByIdQuery(int id)
        {
            Id = id;
          
        }
        public int Id { get; set; }
     

    }

    public class ActionMethodGetByIdQueryHandler : BaseRequestHandler<ActionMethodGetByIdQuery, Result<ActionMethodViewModel>>
    {
        private readonly IActionMethodRepository _ActionMethodRepository;


        public ActionMethodGetByIdQueryHandler(
            IActionMethodRepository ActionMethodRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _ActionMethodRepository = ActionMethodRepository;
        }

        protected async override Task<Result<ActionMethodViewModel>> HandleRequestAsync(ActionMethodGetByIdQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<ActionMethodViewModel>();
            var response = await _unitOfWork.ActionMethods.FindByIdAsync(input.Id, cancellationToken);
            var ActionMethodViewModel = _mapper.Map<ActionMethodViewModel>(response);

            return result.WithValue(ActionMethodViewModel).ConvertToDtatResult();
        }
    }
}
