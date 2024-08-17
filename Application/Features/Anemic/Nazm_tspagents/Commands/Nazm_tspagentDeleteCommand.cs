using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Nazm.Results;
using NazmMapping.Mappings;

namespace Application.Features.Anemic.Nazm_tspagents.Commands
{
    public class Nazm_tspagentDeleteCommand : BaseRequest, IRequest<Result<bool>>, IMapFrom<Nazm_tspagent>
    {
        public Nazm_tspagentDeleteCommand()
        {

        }
        public Nazm_tspagentDeleteCommand(int id)
        {
            Nazm_tspagentId = id;
        }
        public int Nazm_tspagentId { get; set; }
    }

    public class Nazm_tspagentDeleteCommandHandler : BaseRequestHandler<Nazm_tspagentDeleteCommand, Result<bool>>
    {
        private readonly INazm_tspagentRepository _Nazm_tspagentRepository;

        public Nazm_tspagentDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, INazm_tspagentRepository Nazm_tspagentRepository) : base(unitOfWork, mapper)
        {
            _Nazm_tspagentRepository = Nazm_tspagentRepository;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(Nazm_tspagentDeleteCommand input, CancellationToken cancellationToken)
        {
            bool result = false;

            var response = new FluentResults.Result<bool>();

            try
            {
                await _unitOfWork.BeginTransaction(cancellationToken);

                var model = await _unitOfWork.Nazm_tspagents.FindByIdAsync(input.Nazm_tspagentId,cancellationToken);

                if (model != null)
                    _unitOfWork.Nazm_tspagents.Delete(model);

                await _unitOfWork.Commit(cancellationToken, isDeleted: true);

                await _unitOfWork.CommitTransaction(cancellationToken);

                result = true;
            }

            catch (Exception)
            {
                result = false;

                await _unitOfWork.RollbackTransaction(cancellationToken);

                throw;
            }

            return response
                .WithValue(result)
                .ConvertToDtatResult();
        }
    }
}
