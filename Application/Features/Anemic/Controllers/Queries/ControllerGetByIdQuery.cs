using AutoMapper;
using MediatR;
using ViewModels.Controllers;
using Nazm.Results;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;

namespace Application.Features.Anemic.Controllers.Queries
{

    public class ControllerGetByIdQuery : BaseRequest, IRequest<Result<ControllerViewModel>>
    {
        public ControllerGetByIdQuery()
        {

        }
        public ControllerGetByIdQuery(int id)
        {
            Id = id;
          
        }
        public int Id { get; set; }
       

    }

    public class ControllerGetByIdQueryHandler : BaseRequestHandler<ControllerGetByIdQuery, Result<ControllerViewModel>>
    {
        private readonly IControllerRepository _ControllerRepository;


        public ControllerGetByIdQueryHandler(
            IControllerRepository ControllerRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(unitOfWork, mapper)
        {
            _ControllerRepository = ControllerRepository;
        }

        protected async override Task<Result<ControllerViewModel>> HandleRequestAsync(ControllerGetByIdQuery input, CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<ControllerViewModel>();
            var response = await _unitOfWork.Controllers.FindByIdAsync(input.Id, cancellationToken);
            var ControllerViewModel = _mapper.Map<ControllerViewModel>(response);
            return result.WithValue(ControllerViewModel).ConvertToDtatResult();

        }
    }
}
