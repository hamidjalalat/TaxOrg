
using Application.Common.Interfaces.Repository;
using Application.Common.Interfaces.Repository.Anemic.EF;
using Domain.Anemic.Entities;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Configurations.Anemic.Contexts;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Infrastructure.Repository.Anemic.EF
{

    public class UserRepository : EFRepository<User>, IUserRepository<User>
    {
        private readonly EFContext _databaseContext;

        public UserRepository(EFContext _databaseContext) : base(_databaseContext)
        {
            this._databaseContext = _databaseContext;
        }

        public IQueryable<User> GetAllByRole(string roleName)
        {
            return
                _databaseContext.UserRoles
                .Join(_databaseContext.Users.Where(u => !u.IsDeleted),
                    ur => ur.UserId,
                    u => u.Id, (ur, u) => new
                    {
                        ur.RoleId,
                        ApplicationUser = u
                    })
                .Join(_databaseContext.Roles,
                    ur => ur.RoleId,
                    r => r.Id, (urm, r) => new
                    {
                        urm.ApplicationUser,
                        RoleName = r.Name
                    })
                .Where(r => r.RoleName == roleName)
                .Select(u => u.ApplicationUser);
        }

        public IQueryable<User> GetAllExceptRole(string roleName)
        {
            return
                _databaseContext.UserRoles
                .Join(_databaseContext.Users.Where(u => !u.IsDeleted),
                    ur => ur.UserId,
                    u => u.Id, (ur, u) => new
                    {
                        ur.RoleId,
                        ApplicationUser = u
                    })
                .Join(_databaseContext.Roles,
                    ur => ur.RoleId,
                    r => r.Id, (urm, r) => new
                    {
                        urm.ApplicationUser,
                        RoleName = r.Name
                    })
                .Where(r => r.RoleName != roleName)
                .Select(u => u.ApplicationUser);
        }

        public async Task<Result<bool>> IsUnique(User model)
        {
            var result = new FluentResults.Result<bool>();
            List<String> errorList = new List<String>();
            #region Add
            if (string.IsNullOrEmpty(model.Id))
            {
                var isExist = await GetAll.AnyAsync(s => s.FirstName == model.FirstName && s.LastName == model.LastName);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.User));
                }
                isExist = await GetAll.AnyAsync(s => s.NationalCode == model.NationalCode);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.NationalCode));
                }
                isExist = await GetAll.AnyAsync(s => s.UserName == model.UserName);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.UserName));
                }
            }
            #endregion

            #region Edit
            if (!string.IsNullOrEmpty(model.Id))
            {
                var isExist = await GetAll.AnyAsync(s => s.Id != model.Id && s.FirstName == model.FirstName && s.LastName == model.LastName );
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.User));
                }
                isExist = await GetAll.AnyAsync(s => s.Id != model.Id &&  s.NationalCode == model.NationalCode);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.NationalCode));
                }
                isExist = await GetAll.AnyAsync(s => s.Id != model.Id && s.UserName == model.UserName);
                if (isExist)
                {
                    errorList.Add(string.Format(Resources.Messages.Validations.Repetitive, Resources.DataDictionary.UserName));
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
