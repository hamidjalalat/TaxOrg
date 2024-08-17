using Application.Common.Exceptions;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;
using ViewModels.Nazm_tspagents;

namespace Application.Features.Anemic.Nazm_tspagents.Commands
{
    public class Nazm_tspagentUpdateCommand : BaseRequest, IRequest<Result<Nazm_tspagentUpdateViewModel>>
    {
        public Nazm_tspagentUpdateViewModel Nazm_tspagentViewModel { get; set; }

    }

    public class UpdateNazm_tspagentCommandHandler : BaseRequestHandler<Nazm_tspagentUpdateCommand, Result<Nazm_tspagentUpdateViewModel>>
    {
        private readonly INazm_tspagentRepository _Nazm_tspagentRepository;

        public UpdateNazm_tspagentCommandHandler(IUnitOfWork unitOfWork,IMapper mapper, INazm_tspagentRepository Nazm_tspagentRepository) : base(unitOfWork, mapper)
        {
            _Nazm_tspagentRepository = Nazm_tspagentRepository;
        }

        protected override async Task<Result<Nazm_tspagentUpdateViewModel>> HandleRequestAsync(Nazm_tspagentUpdateCommand input, CancellationToken cancellationToken)
        {
            try
            {
                var response = new FluentResults.Result<Nazm_tspagentUpdateViewModel>();

                await _unitOfWork.BeginTransaction(cancellationToken);

                var entity = await _unitOfWork.Nazm_tspagents.GetAll.Where(s => s.id == input.Nazm_tspagentViewModel.id).SingleOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    return response
                         .WithError(Resources.Messages.Errors.RecordEmpty)
                         .ConvertToDtatResult();
                }

                var model = _mapper.Map(input.Nazm_tspagentViewModel, entity);

                var isUnique = await _unitOfWork.Nazm_tspagents.IsUnique(model, cancellationToken);

                if (isUnique.IsSuccess)
                {
                    _unitOfWork.Nazm_tspagents.Update(model);

                    await _unitOfWork.Commit(cancellationToken);
                    await _unitOfWork.CommitTransaction(cancellationToken);

                    var viewModel = _mapper.Map<Nazm_tspagentUpdateViewModel>(model);

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
