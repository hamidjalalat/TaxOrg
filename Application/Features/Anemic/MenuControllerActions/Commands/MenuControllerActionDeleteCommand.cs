using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Nazm.Results;
using NazmMapping.Mappings;

namespace Application.Features.Anemic.MenuControllerActions.Commands
{
    public class MenuControllerActionDeleteCommand : BaseRequest, IRequest<Result<bool>>, IMapFrom<MenuControllerAction>
    {
        public MenuControllerActionDeleteCommand()
        {

        }
        public MenuControllerActionDeleteCommand(int id)
        {
            MenuControllerActionId = id;
        }
        public int MenuControllerActionId { get; set; }
    }

    public class MenuControllerActionDeleteCommandHandler : BaseRequestHandler<MenuControllerActionDeleteCommand, Result<bool>>
    {
        private readonly IMenuControllerActionRepository _MenuControllerActionRepository;

        public MenuControllerActionDeleteCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IMenuControllerActionRepository MenuControllerActionRepository) : base(unitOfWork, mapper)
        {
            _MenuControllerActionRepository = MenuControllerActionRepository;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(MenuControllerActionDeleteCommand input, CancellationToken cancellationToken)
        {
            bool result = false;
            var response = new FluentResults.Result<bool>();
            try
            {
                await _unitOfWork.BeginTransaction(cancellationToken);
                var model = await _MenuControllerActionRepository.FindByIdAsync(input.MenuControllerActionId, cancellationToken);
                if (model != null)
                {
                    _unitOfWork.MenuControllerActions.Delete(model);
                    response
                        .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.Buttons.Delete}"));
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
                .ConvertToDtatResult() ;
        }
    }
}
