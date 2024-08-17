using Application.Common.Exceptions;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using ViewModels.RegulationGroups;

namespace Application.Features.Anemic.RegulationGroups.Commands
{
    public class RegulationGroupUpdateCommand : BaseRequest, IRequest<Result<RegulationGroupUpdateViewModel>>
    {
        public RegulationGroupUpdateViewModel RegulationGroupViewModel { get; set; }

    }

    public class UpdateRegulationGroupCommandHandler : BaseRequestHandler<RegulationGroupUpdateCommand, Result<RegulationGroupUpdateViewModel>>
    {
        private readonly IRegulationGroupRepository _RegulationGroupRepository;

        public UpdateRegulationGroupCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IRegulationGroupRepository RegulationGroupRepository) : base(unitOfWork, mapper)
        {
            _RegulationGroupRepository = RegulationGroupRepository;
        }

        protected override async Task<Result<RegulationGroupUpdateViewModel>> HandleRequestAsync(RegulationGroupUpdateCommand input, CancellationToken cancellationToken)
        {
            try
            {
                var response = new FluentResults.Result<RegulationGroupUpdateViewModel>();
                await _unitOfWork.BeginTransaction(cancellationToken);
                var entity = await _unitOfWork.RegulationGroups.GetAll.Where(s => s.Id == input.RegulationGroupViewModel.Id).SingleOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    return response
                         .WithError(Resources.Messages.Errors.RecordEmpty)
                         .ConvertToDtatResult();
                }
                var model = _mapper.Map(input.RegulationGroupViewModel, entity);
                var isUnique = await _unitOfWork.RegulationGroups.IsUnique(model, cancellationToken);
                if (isUnique.IsSuccess)
                {
                    _unitOfWork.RegulationGroups.Update(model);
                    await _unitOfWork.Commit(cancellationToken);
                    await _unitOfWork.CommitTransaction(cancellationToken);
                    var viewModel = _mapper.Map<RegulationGroupUpdateViewModel>(model);
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
