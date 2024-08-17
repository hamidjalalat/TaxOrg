using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using ViewModels.Menus;

namespace Application.Features.Anemic.Menus.Commands
{
    public class MenuUpdateCommand : BaseRequest, IRequest<Result<MenuViewModel>>
    {
        public MenuUpdateViewModel MenuUpdateViewModel { get; set; }       
    }

    public class MenuUpdateCommandHandler : BaseRequestHandler<MenuUpdateCommand, Result<MenuViewModel>>
    {
        private readonly IMenuRepository _MenuRepository;

        public MenuUpdateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IMenuRepository MenuRepository) : base(unitOfWork, mapper)
        {
            _MenuRepository = MenuRepository;
        }

        protected override async Task<Result<MenuViewModel>> HandleRequestAsync(MenuUpdateCommand input, CancellationToken cancellationToken)
        {
            try
            {
                var response = new FluentResults.Result<MenuViewModel>();
                await _unitOfWork.BeginTransaction(cancellationToken);
                var entity = await _unitOfWork.Menus.GetAll.Where(s => s.MenuId == input.MenuUpdateViewModel.MenuId).SingleOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    return response
                         .WithError(Resources.Messages.Errors.RecordEmpty)
                         .ConvertToDtatResult();
                }
                var model = _mapper.Map(input.MenuUpdateViewModel, entity);
                var isUnique = await _unitOfWork.Menus.IsUnique(model, cancellationToken);
                if (isUnique.IsSuccess)
                {
                    _unitOfWork.Menus.Update(model);
                    await _unitOfWork.Commit(cancellationToken);
                    await _unitOfWork.CommitTransaction(cancellationToken);
                    var modelViewModel = _mapper.Map<MenuViewModel>(model);
                    return response
                        .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.Buttons.Save}"))
                        .WithValue(modelViewModel)
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
