using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using ViewModels.Controllers;

namespace Application.Features.Anemic.Controllers.Commands
{
    public class ControllerUpdateCommand : BaseRequest, IRequest<Result<ControllerViewModel>>, IMapFrom<Controller>
    {
        public ControllerViewModel ControllerViewModel { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ControllerViewModel, Controller>()
                .ForMember(d => d.ControllerId, opt => opt.MapFrom(s => s.ControllerId))
                .ReverseMap();
        }
    }

    public class ControllerUpdateCommandHandler : BaseRequestHandler<ControllerUpdateCommand, Result<ControllerViewModel>>
    {
        private readonly IControllerRepository _ControllerRepository;

        public ControllerUpdateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IControllerRepository ControllerRepository) : base(unitOfWork, mapper)
        {
            _ControllerRepository = ControllerRepository;
        }

        protected override async Task<Result<ControllerViewModel>> HandleRequestAsync(ControllerUpdateCommand input, CancellationToken cancellationToken)
        {
            try
            {
                var response = new FluentResults.Result<ControllerViewModel>();
                await _unitOfWork.BeginTransaction(cancellationToken);
                var entity = await _unitOfWork.Controllers.GetAll.Where(s => s.ControllerId == input.ControllerViewModel.ControllerId).SingleOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    return response
                         .WithError(Resources.Messages.Errors.RecordEmpty)
                         .ConvertToDtatResult();
                }
                var model = _mapper.Map(input.ControllerViewModel, entity);

                _unitOfWork.Controllers.Update(model);
                await _unitOfWork.Commit(cancellationToken);
                await _unitOfWork.CommitTransaction(cancellationToken);
                return response
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
