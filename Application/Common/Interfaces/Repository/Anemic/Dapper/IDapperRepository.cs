using Domain.Enums;
using System.Data;

namespace Application.Common.Interfaces.Repository.Anemic.Dapper
{
    public interface IDapperRepository<T>
    {
        Task<T?> GetFirstAsync(object param, string query, DatabaseTypeEnum databaseType);
        Task<DTO?> GetFirstAsync<DTO>(object param, string query, DatabaseTypeEnum databaseType) where DTO : class;
        Task<T?> GetSingleAsync(object param, string query, DatabaseTypeEnum databaseType);
        Task<DTO?> GetSingleAsync<DTO>(object param, string query, DatabaseTypeEnum databaseType) where DTO : class;
        Task<List<T>?> GetListAsync(object param, string query, DatabaseTypeEnum databaseType);
        Task<List<DTO>?> GetListAsync<DTO>(object param, string query, DatabaseTypeEnum databaseType) where DTO : class;
        Task Exeute(object param, string query, DatabaseTypeEnum databaseType);
        Task<int?> Insert(object param, string query, DatabaseTypeEnum databaseType);
    }
}
