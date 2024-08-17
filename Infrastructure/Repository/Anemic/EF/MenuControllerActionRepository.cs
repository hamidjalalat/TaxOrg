using Application.Common.Interfaces.Repository.Anemic.Base;
using Application.Common.Interfaces.Repository.Anemic.EF;
using Domain.Anemic.Entities;
using Persistence.Configurations.Anemic.Contexts;
using ViewModels.MenuControllerActions;

namespace Infrastructure.Repository.Anemic.EF
{
    public class MenuControllerActionRepository : EFRepository<MenuControllerAction>, IMenuControllerActionRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuControllerActionRepository(EFContext context) : base(context)
        {
        }
        public MenuControllerActionRepository(EFContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<FluentResults.Result<MenuControllerActionViewModel>> IsValid(MenuControllerActionViewModel model,CancellationToken cancellationToken)
        {
            var response = new FluentResults.Result<MenuControllerActionViewModel>();
            List<string> errorlist = new List<string>();
            var menu = await FindByIdAsync(model.MenuControllerId, cancellationToken);
            if (menu == null)
            {
                errorlist.Add("منو انتخابی وجود ندارد لطفا در ورود اطللاعات دقت نمایید");
            }
            var controller = await _unitOfWork.Controllers.FindByIdAsync(model.ActionMethodId, cancellationToken);
            if (controller == null)
            {
                errorlist.Add("متد انتخابی وجود ندارد لطفا در ورود اطللاعات دقت نمایید");
            }
            return response.WithErrors(errorlist);
        }
    }
}
