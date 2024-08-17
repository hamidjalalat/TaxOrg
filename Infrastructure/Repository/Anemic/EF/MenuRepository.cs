using Application.Common.Interfaces.Repository.Anemic.EF;
using Domain.Anemic.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.Anemic.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Anemic.EF
{
    public class MenuRepository : EFRepository<Menu>, IMenuRepository
    {
        public MenuRepository(EFContext context) : base(context)
        {
        }

        public async Task<IQueryable<Menu>?> GetChildList(int menuId, CancellationToken cancellationToken)
        {
            var menu = await FindByIdAsync(menuId, cancellationToken);
            var result = GetAll
                                .Where(employee => employee.ParentId.IsDescendantOf(menu.ParentId));


            return result;
        }

        public async Task<Result<bool>> IsUnique(Menu model,CancellationToken cancellationToken)
        {
            var result = new FluentResults.Result<bool>();
            List<String> errorList = new List<String>();
            #region Add
            if (model.MenuId == 0)
            {
                
                var isExist = await GetAll.AnyAsync(s => s.ParentId==model.ParentId && s.Title == model.Title, cancellationToken);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.Title));
                }
            }
            #endregion

            #region Edit
            if (model.MenuId > 0)
            {
                
                var isExist = await GetAll.AnyAsync(s => s.MenuId != model.MenuId && s.ParentId == model.ParentId && s.Title == model.Title, cancellationToken);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.Title));
                }
            }
            #endregion

            if (errorList.Count > 0)
            {
                return result.WithErrors(errorList);
            }
            else
            {
                return result.WithValue(true);
            }
        }
    }
}
