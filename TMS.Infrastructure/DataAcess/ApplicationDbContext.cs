using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using TMS.Common.Extensions;

namespace TMS.Infrastructure.DataAcess
{
    public class ApplicationDbContext
    {

        private readonly string _connectionString;
        public ApplicationDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString(AppConstants.SQL_CONNECTION);
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sqlQuery, DynamicParameters param)
        {
            using (var connection = this.CreateConnection())
            {
                connection.Open();
                return await connection.QueryAsync<T>(sqlQuery, param);
            }
        }

    }
}
