using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using Domain.Anemic.Entities;
using Persistence.Configurations.Anemic.Contexts;
using ViewModels.MenuControllers;

namespace Infrastructure.Repository.Anemic.EF
{
    public class MenuControllerRepository : EFRepository<MenuController>, IMenuControllerRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuControllerRepository(EFContext context) : base(context)
        {

        }
        public MenuControllerRepository(EFContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<FluentResults.Result<MenuControllerViewModel>> IsValid(MenuControllerViewModel model,CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<MenuControllerViewModel>();
            List<string> errorlist = new List<string>();
            var menu = await _unitOfWork.MenuControllers.FindByIdAsync(model.MenuId, cancellationToken);
            if (menu == null)
            {
                errorlist.Add("منو انتخابی وجود ندارد لطفا در ورود اطللاعات دقت نمایید");
            }
            var controller = await _unitOfWork.Controllers.FindByIdAsync(model.ControllerId, cancellationToken);
            if (controller == null)
            {
                errorlist.Add("فرم انتخابی وجود ندارد لطفا در ورود اطللاعات دقت نمایید");
            }
            return response.WithErrors(errorlist);
        }
    }
}
