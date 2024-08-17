using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using ViewModels.Nazm_tspagents;

namespace Application.Features.Anemic.Nazm_tspagents.Queries
{

    public class Nazm_tspagentFindByIdQuery : BaseRequest, IRequest<Result<Nazm_tspagentViewModel>>
    {
        public Nazm_tspagentFindByIdQuery()
        {

        }
        public Nazm_tspagentFindByIdQuery(int id)
        {
          
            Id = id;
        }
        public int Id { get; set; }

    }

    public class Nazm_tspagentGetByIdQueryHandler : BaseRequestHandler<Nazm_tspagentFindByIdQuery, Result<Nazm_tspagentViewModel>>
    {
        private readonly INazm_tspagentRepository _Nazm_tspagentRepository;


        public Nazm_tspagentGetByIdQueryHandler( INazm_tspagentRepository Nazm_tspagentRepository,IMapper mapper,IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _Nazm_tspagentRepository = Nazm_tspagentRepository;
        }

        protected async override Task<Result<Nazm_tspagentViewModel>> HandleRequestAsync(Nazm_tspagentFindByIdQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<Nazm_tspagentViewModel>();

            var response = await _unitOfWork.Nazm_tspagents.FindByIdAsync(input.Id, cancellationToken);
            var Nazm_tspagentViewModel = _mapper.Map<Nazm_tspagentViewModel>(response);

            return result.WithValue(Nazm_tspagentViewModel).ConvertToDtatResult();
        }
    }
}
