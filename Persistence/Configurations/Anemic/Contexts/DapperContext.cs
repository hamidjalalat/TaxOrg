using Domain.Enums;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Persistence.Configurations.Anemic.Contexts
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _oracleConnectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _oracleConnectionString = _configuration.GetConnectionString("orclConnection");
        }
        //public IDbConnection CreateConnection()
        //    => new SqlConnection(_connectionString);

        public IDbConnection CreateConnection(DatabaseTypeEnum dbType)
        {
            switch (dbType)
            {
                case DatabaseTypeEnum.SQL:
                    return new SqlConnection(_connectionString);
                case DatabaseTypeEnum.Oracle:
                    return new OracleConnection(_oracleConnectionString);
                default:
                    return new SqlConnection(_connectionString);
            }
        }
    }
}
