using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;

namespace Application.Features.Anemic.ActionMethods.Commands
{
    public class ActionMethodDeleteCommand : BaseRequest, IRequest<Result<bool>>, IMapFrom<ActionMethod>
    {
        public ActionMethodDeleteCommand()
        {

        }
        public ActionMethodDeleteCommand(int id)
        {
            ActionMethodId = id;
        }
        public int ActionMethodId { get; set; }
    }

    public class ActionMethodDeleteCommandHandler : BaseRequestHandler<ActionMethodDeleteCommand, Result<bool>>
    {
        private readonly IActionMethodRepository _ActionMethodRepository;

        public ActionMethodDeleteCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IActionMethodRepository ActionMethodRepository) : base(unitOfWork, mapper)
        {
            _ActionMethodRepository = ActionMethodRepository;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(ActionMethodDeleteCommand input, CancellationToken cancellationToken)
        {
            bool result = false;
            var response = new FluentResults.Result<bool>();
            try
            {
                await _unitOfWork.BeginTransaction(cancellationToken);
                var model = await _ActionMethodRepository.FindByIdAsync(input.ActionMethodId, cancellationToken);
                if (model != null)
                {
                    var menuControllerAction = await _unitOfWork.MenuControllerActions
                        .GetAll.Where(s=>s.ActionMethodId==model.ActionMethodId).ToListAsync(cancellationToken);
                    if (menuControllerAction != null)
                    {
                        return response
                            .WithError(Resources.Messages.Errors.DependentTables)
                            .ConvertToDtatResult();
                    }
                    else
                    {
                        _unitOfWork.ActionMethods.Delete(model);
                        response
                            .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.Buttons.Delete}"));
                    }
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
