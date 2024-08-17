using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Nazm.Results;
using ViewModels.Nazm_tspagents;
using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;

namespace Application.Features.Anemic.Nazm_tspagents.Commands
{
    public class Nazm_tspagentCreateCommand : BaseRequest, IRequest<Result<Nazm_tspagentViewModel>>
    {
        public Nazm_tspagentCreateViewModel Nazm_tspagentViewModel { get; set; }

    }

    public class Nazm_tspagentCreateCommandHandler : BaseRequestHandler<Nazm_tspagentCreateCommand, Result<Nazm_tspagentViewModel>>
    {
        private readonly INazm_tspagentRepository _Nazm_tspagentRepository;

        public Nazm_tspagentCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,INazm_tspagentRepository Nazm_tspagentRepository) : base(unitOfWork, mapper)
        {
            _Nazm_tspagentRepository = Nazm_tspagentRepository;
        }

        protected override async Task<Result<Nazm_tspagentViewModel>> HandleRequestAsync(Nazm_tspagentCreateCommand input, CancellationToken cancellationToken)
        {
            try
            {
                var response = new FluentResults.Result<Nazm_tspagentViewModel>();

                await _unitOfWork.BeginTransaction(cancellationToken);

                var model = _mapper.Map<Nazm_tspagent>(input.Nazm_tspagentViewModel);

                var isUnique = await _unitOfWork.Nazm_tspagents.IsUnique(model, cancellationToken);

                if (isUnique.IsSuccess)
                {
                    _unitOfWork.Nazm_tspagents.Insert(model);

                    await _unitOfWork.Commit(cancellationToken);
                    await _unitOfWork.CommitTransaction(cancellationToken);

                    var viewModel = _mapper.Map<Nazm_tspagentViewModel>(model);

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
