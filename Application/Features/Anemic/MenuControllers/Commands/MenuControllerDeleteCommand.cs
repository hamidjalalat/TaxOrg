using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using NazmMapping.Mappings;

namespace Application.Features.Anemic.MenuControllers.Commands
{
    public class MenuControllerDeleteCommand : BaseRequest, IRequest<Result<bool>>, IMapFrom<MenuController>
    {
        public MenuControllerDeleteCommand()
        {

        }
        public MenuControllerDeleteCommand(int id)
        {
            MenuControllerId = id;
        }
        public int MenuControllerId { get; set; }
    }

    public class MenuControllerDeleteCommandHandler : BaseRequestHandler<MenuControllerDeleteCommand, Result<bool>>
    {
        private readonly IMenuControllerRepository _MenuControllerRepository;

        public MenuControllerDeleteCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IMenuControllerRepository MenuControllerRepository) : base(unitOfWork, mapper)
        {
            _MenuControllerRepository = MenuControllerRepository;
        }

        protected override async Task<Result<bool>> HandleRequestAsync(MenuControllerDeleteCommand input, CancellationToken cancellationToken)
        {
            bool result = false;
            var response = new FluentResults.Result<bool>();
            try
            {
                await _unitOfWork.BeginTransaction(cancellationToken);
                var model = await _MenuControllerRepository.FindByIdAsync(input.MenuControllerId, cancellationToken);
                if (model != null)
                {
                    var menuControllerActions = await _unitOfWork.MenuControllerActions
                        .GetAll.Where(s=>s.MenuControllerId==model.MenuControllerId).ToListAsync(cancellationToken);
                    
                    if (menuControllerActions != null)
                    {
                        return response
                            .WithError(Resources.Messages.Errors.DependentTables)
                            .ConvertToDtatResult();
                    }
                    else
                    {
                        _unitOfWork.MenuControllers.Delete(model);
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
