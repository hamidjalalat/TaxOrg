using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Nazm.Results;
using NazmMapping.Mappings;
using ViewModels.Controllers;

namespace Application.Features.Anemic.Controllers.Commands
{

    public class CreateControllerCommand : BaseRequest, IRequest<Result<ControllerViewModel>>, IMapFrom<Controller>
    {
        public ControllerViewModel ControllerViewModel { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ControllerViewModel, Controller>()
                .ForMember(d => d.ControllerId, opt => opt.MapFrom(s => s.ControllerId))
                .ReverseMap();
        }
    }

    public class ControllerCreateCommandHandler : BaseRequestHandler<CreateControllerCommand, Result<ControllerViewModel>>
    {
        private readonly IControllerRepository _ControllerRepository;

        public ControllerCreateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IControllerRepository ControllerRepository) : base(unitOfWork, mapper)
        {
            _ControllerRepository = ControllerRepository;
        }

        protected override async Task<Result<ControllerViewModel>> HandleRequestAsync(CreateControllerCommand input, CancellationToken cancellationToken)
        {
            try
            {
                var result = new FluentResults.Result<ControllerViewModel>();
                await _unitOfWork.BeginTransaction(cancellationToken);
                var model = _mapper.Map<Controller>(input.ControllerViewModel);

                _unitOfWork.Controllers.Insert(model);
                await _unitOfWork.Commit(cancellationToken);
                await _unitOfWork.CommitTransaction(cancellationToken);
                return result
                    .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.Buttons.Save}"))
                    .WithValue(input.ControllerViewModel)
                    .ConvertToDtatResult();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction(cancellationToken);
                throw;
            }

        }
    }
}
