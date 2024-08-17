using Application.Common.Interfaces.Repository.Anemic.Base;
using Domain.Anemic;
using Domain.Anemic.Entities;
using FluentResults;

namespace Application.Common.Interfaces.Repository.Anemic.EF
{
    public interface IUserRepository<T> : IRepository<T> where T : class, ISoftDeletable
    {
        IQueryable<T> GetAllByRole(string roleName);
        IQueryable<T> GetAllExceptRole(string roleName);
        Task<Result<bool>> IsUnique(T model);
    }
}