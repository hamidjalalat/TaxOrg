using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using ViewModels.RegulationGroups;

namespace Application.Features.Anemic.RegulationGroups.Queries
{

    public class RegulationGroupFindByIdQuery : BaseRequest, IRequest<Result<RegulationGroupViewModel>>
    {
        public RegulationGroupFindByIdQuery()
        {

        }
        public RegulationGroupFindByIdQuery(int id)
        {
          
            Id = id;
        }
        public int Id { get; set; }
      

    }

    public class RegulationGroupGetByIdQueryHandler : BaseRequestHandler<RegulationGroupFindByIdQuery, Result<RegulationGroupViewModel>>
    {
        private readonly IRegulationGroupRepository _RegulationGroupRepository;


        public RegulationGroupGetByIdQueryHandler(
            IRegulationGroupRepository RegulationGroupRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _RegulationGroupRepository = RegulationGroupRepository;
        }

        protected async override Task<Result<RegulationGroupViewModel>> HandleRequestAsync(RegulationGroupFindByIdQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<RegulationGroupViewModel>();
            var response = await _unitOfWork.RegulationGroups.FindByIdAsync(input.Id, cancellationToken);
            var RegulationGroupViewModel = _mapper.Map<RegulationGroupViewModel>(response);

            return result.WithValue(RegulationGroupViewModel).ConvertToDtatResult();
        }
    }
}
