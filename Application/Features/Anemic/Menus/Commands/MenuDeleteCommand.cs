using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Nazm.Results;
using NazmMapping.Mappings;

namespace Application.Features.Anemic.Menus.Commands
{
    public class MenuDeleteCommand : BaseRequest, IRequest<Result<bool>>, IMapFrom<Menu>
    {
        public MenuDeleteCommand()
        {

        }
        public MenuDeleteCommand(int id)
        {
            MenuId = id;
        }
        public int MenuId { get; set; }
    }

    public class MenuDeleteCommandHandler : BaseRequestHandler<MenuDeleteCommand, Result<bool>>
    {
        private readonly IMenuRepository _MenuRepository;

        public MenuDeleteCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IMenuRepository MenuRepository) : base(unitOfWork, mapper)
        {
            _MenuRepository = MenuRepository;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(MenuDeleteCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<bool>();
            bool result = false;
            try
            {
                await _unitOfWork.BeginTransaction(cancellationToken);
                var model = await _MenuRepository.FindByIdAsync(input.MenuId, cancellationToken);
                if (model != null)
                {
                    _unitOfWork.Menus.Delete(model);
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
                .ConvertToDtatResult();
        }
    }
}
