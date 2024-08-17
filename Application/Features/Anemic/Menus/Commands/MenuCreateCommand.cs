using Application.Common.Interfaces.Repository.Anemic.Base;
using AutoMapper;
using Domain.Anemic.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nazm.Results;
using ViewModels.Menus;

namespace Application.Features.Anemic.Menus.Commands
{
    // [Authorize]
    public class MenuCreateCommand : BaseRequest, IRequest<Result<MenuViewModel>>
    {
        public MenuCreateViewModel MenuViewModel { get; set; }
    }

    public class MenuCreateCommandHandler : BaseRequestHandler<MenuCreateCommand, Result<MenuViewModel>>
    {
        public MenuCreateCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper

            ) : base(unitOfWork, mapper)
        {

        }

        protected override async Task<Result<MenuViewModel>> HandleRequestAsync(MenuCreateCommand input, CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<MenuViewModel>();
            var model = new Menu();
            try
            {
                await _unitOfWork.BeginTransaction(cancellationToken);

                if (input.MenuViewModel.ParentId == null)
                {
                    input.MenuViewModel.ParentId = 1;
                }
                var menu = await _unitOfWork.Menus.FindByIdAsync(input.MenuViewModel.ParentId, cancellationToken);
                if (menu != null && menu.ParentId.GetLevel()==0)
                {
                    var maxId = await _unitOfWork.Menus.GetAll.Where(s => s.ParentId.GetAncestor(1) == menu.ParentId).MaxAsync(s => s.ParentId, cancellationToken);
                    model.ParentId = menu.ParentId.GetDescendant(maxId, null);
                }
                else
                    model.ParentId = menu.ParentId.GetDescendant(null, null);

                model.IsVisible = input.MenuViewModel.IsVisible;
                model.Title = input.MenuViewModel.Title;
                model.Url = input.MenuViewModel.Url;
                model.PageTitle = input.MenuViewModel.PageTitle;
                model.PageDescription = input.MenuViewModel.PageDescription;
                model.MenuIcon = input.MenuViewModel.MenuIcon;
                model.PageIcon = input.MenuViewModel.PageIcon;
                model.Sort = input.MenuViewModel.Sort;
                var isUnique = await _unitOfWork.Menus.IsUnique(model, cancellationToken);
                if (isUnique.IsSuccess)
                {
                    _unitOfWork.Menus.Insert(model);
                    await _unitOfWork.Commit(cancellationToken);
                    await _unitOfWork.CommitTransaction(cancellationToken);
                    response
                        .WithSuccess(string.Format(Resources.Messages.Successes.Success, $"{Resources.Buttons.Save}"));
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
            var modelViewModel = _mapper.Map<MenuViewModel>(model);
            return response
                    .WithValue(modelViewModel)
                    .ConvertToDtatResult();
        }
    }
}
