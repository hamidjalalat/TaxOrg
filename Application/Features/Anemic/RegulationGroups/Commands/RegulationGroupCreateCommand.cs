using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Nazm.Results;
using ViewModels.RegulationGroups;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;

namespace Application.Features.Anemic.RegulationGroups.Commands
{
    public class RegulationGroupCreateCommand : BaseRequest, IRequest<Result<RegulationGroupViewModel>>
    {
        public RegulationGroupCreateViewModel RegulationGroupViewModel { get; set; }

    }

    public class RegulationGroupCreateCommandHandler : BaseRequestHandler<RegulationGroupCreateCommand, Result<RegulationGroupViewModel>>
    {
        private readonly IRegulationGroupRepository _RegulationGroupRepository;

        public RegulationGroupCreateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IRegulationGroupRepository RegulationGroupRepository) : base(unitOfWork, mapper)
        {
            _RegulationGroupRepository = RegulationGroupRepository;
        }

        protected override async Task<Result<RegulationGroupViewModel>> HandleRequestAsync(RegulationGroupCreateCommand input, CancellationToken cancellationToken)
        {
            try
            {
                var response = new FluentResults.Result<RegulationGroupViewModel>();
                await _unitOfWork.BeginTransaction(cancellationToken);
                var model = _mapper.Map<RegulationGroup>(input.RegulationGroupViewModel);
                var isUnique = await _unitOfWork.RegulationGroups.IsUnique(model, cancellationToken);
                if (isUnique.IsSuccess)
                {
                    _unitOfWork.RegulationGroups.Insert(model);
                    await _unitOfWork.Commit(cancellationToken);
                    await _unitOfWork.CommitTransaction(cancellationToken);
                    var viewModel = _mapper.Map<RegulationGroupViewModel>(model);
                    return response
                        .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.Buttons.Save}"))
                        .WithValue(viewModel)
                        .ConvertToDtatResult();
                }
                else
                {
                    return response
                        .WithErrors(isUnique.Errors)
                        .ConvertToDtatResult();
                }
                
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransaction(cancellationToken);
                throw;
            }

        }
    }
}
