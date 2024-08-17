using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;

namespace Application.Features.Anemic.RegulationGroups.Commands
{
    public class RegulationGroupDeleteCommand : BaseRequest, IRequest<Result<bool>>, IMapFrom<RegulationGroup>
    {
        public RegulationGroupDeleteCommand()
        {

        }
        public RegulationGroupDeleteCommand(int id)
        {
            RegulationGroupId = id;
        }
        public int RegulationGroupId { get; set; }
    }

    public class RegulationGroupDeleteCommandHandler : BaseRequestHandler<RegulationGroupDeleteCommand, Result<bool>>
    {
        private readonly IRegulationGroupRepository _RegulationGroupRepository;

        public RegulationGroupDeleteCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IRegulationGroupRepository RegulationGroupRepository) : base(unitOfWork, mapper)
        {
            _RegulationGroupRepository = RegulationGroupRepository;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(RegulationGroupDeleteCommand input, CancellationToken cancellationToken)
        {
            bool result = false;
            var response = new FluentResults.Result<bool>();
            try
            {
                await _unitOfWork.BeginTransaction(cancellationToken);
                var model = await _RegulationGroupRepository.FindByIdAsync(input.RegulationGroupId, cancellationToken);

                if (model != null)
                {
                    /*
                    var regulations = await _unitOfWork.Regulations.GetAll.Where(s => s.RegulationGroupId == model.Id).ToListAsync(cancellationToken);
                    if (regulations.Count > 0)
                    {
                        return response
                            .WithError(Resources.Messages.Errors.DependentTables)
                            .ConvertToDtatResult();
                    }
                    else
                    {
                        _unitOfWork.RegulationGroups.Delete(model);
                        response
                            .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.Buttons.Delete}"));
                    }
                    */
                }

                await _unitOfWork.Commit(cancellationToken,isDeleted: true);
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
