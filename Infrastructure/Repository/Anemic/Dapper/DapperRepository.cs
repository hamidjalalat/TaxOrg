using Application.Common.Interfaces.Repository.Anemic.Dapper;
using Dapper;
using Domain.Enums;
using Oracle.ManagedDataAccess.Client;
using Persistence.Configurations.Anemic.Contexts;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repository.Anemic.Dapper
{

    public class DapperRepository<T> : IDapperRepository<T> where T : class
    {
        #region Fields

        private readonly DapperContext _context;

        #endregion

        #region Constructor

        public DapperRepository(DapperContext context) => _context = context;

        #endregion

        #region Implementation of IRepository
        public async Task<T?> GetFirstAsync(object param, string query, DatabaseTypeEnum databaseType)
        {
            using (var connection = _context.CreateConnection(databaseType))
            {
                return await connection.QueryFirstOrDefaultAsync<T>(query, param, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<DTO?> GetFirstAsync<DTO>(object param, string query, DatabaseTypeEnum databaseType) where DTO : class
        {
            using (var connection = _context.CreateConnection(databaseType))
            {
                return await connection.QueryFirstOrDefaultAsync<DTO>(query, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T?> GetSingleAsync(object param, string query, DatabaseTypeEnum databaseType)
        {
            using (var connection = _context.CreateConnection(databaseType))
            {
                return await connection.QuerySingleOrDefaultAsync<T>(query, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<DTO?> GetSingleAsync<DTO>(object param, string query, DatabaseTypeEnum databaseType) where DTO : class
        {
            using (var connection = _context.CreateConnection(databaseType))
            {
                return await connection.QuerySingleOrDefaultAsync<DTO>(query, param, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<T>?> GetListAsync(object param, string query, DatabaseTypeEnum databaseType)
        {
            using (var connection = _context.CreateConnection(databaseType))
            {
                var result = await connection.QueryAsync<T>(query, param, commandTimeout: connection.ConnectionTimeout, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<List<DTO>?> GetListAsync<DTO>(object param, string query,DatabaseTypeEnum databaseType) where DTO : class
        {
            using (var connection = _context.CreateConnection(databaseType))
            {
                var result = await connection.QueryAsync<DTO>(query, param, commandTimeout: connection.ConnectionTimeout, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task Exeute(object param, string query, DatabaseTypeEnum databaseType)
        {
            using (var connection = _context.CreateConnection(databaseType))
            {
                await connection.ExecuteAsync(query, param, commandTimeout: connection.ConnectionTimeout, commandType: CommandType.StoredProcedure);

            }
        }

        public async Task<int?> Insert(object param, string query, DatabaseTypeEnum databaseType)
        {
            using (var connection = _context.CreateConnection(databaseType))
            {
                return await connection.QuerySingleOrDefaultAsync<int>(query, param, commandType: CommandType.StoredProcedure);
            }
        }
        #endregion

    }

}
