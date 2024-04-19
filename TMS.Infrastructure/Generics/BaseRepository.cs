using AutoMapper;
using Dapper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.Generic;
using TMS.Application.Repository;
using TMS.Infrastructure.DataAcess;
using TMS.Infrastructure.Helpers;

namespace TMS.Infrastructure.Generics
{
    public class BaseRepository<T> : IBaseRepository<T> where T:class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly ITransactionsRepository _transactionsRepository;

        private readonly IMapper _mapper;
        public BaseRepository(ApplicationDbContext dbContext, IMapper mapper, ITransactionsRepository transactionsRepository)
        {
            _context = dbContext;
            _mapper = mapper;
            _transactionsRepository = transactionsRepository;
        }

        public async Task<List<T>> GetAsync()
        {
            var query = DynamicQueryHelper.GetSelectQuery<T>();
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<T>(query);
                return values.ToList();
            }
        }

        public async Task<T?> GetByIdAsync(string idField, int id)
        {
            var query = DynamicQueryHelper.GetSelectByIdQuery<T>(idField);
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<T>(query, new { Id = id });
            }
        }

        public async Task<List<T>?> GetListByPropertyAsync(string propertyName, dynamic propertyValue)
        {
            var query = DynamicQueryHelper.GetSelectByIdQuery<T>(propertyName);
            using (var connection = _context.CreateConnection())
            {
                IDictionary<string, object> propertyObj = GetPropertyKeyValueObj(propertyName, propertyValue);
                var values = await connection.QueryAsync<T>(query, propertyObj);
                return values.ToList();
            }
        }

        public async Task<T?> GetSingleByPropertyAsync(string propertyName, dynamic propertyValue)
        {
            var query = DynamicQueryHelper.GetSelectByIdQuery<T>(propertyName);
            using (var connection = _context.CreateConnection())
            {
                IDictionary<string, object> propertyObj = GetPropertyKeyValueObj(propertyName, propertyValue);
                return await connection.QuerySingleOrDefaultAsync<T>(query, propertyObj);
            }
        }

        public async Task<int> PostAsync(T entity)
        {
            dynamic? queryResult;
            var query = DynamicQueryHelper.GetInsertQuery<T>();
            var parameters = EntityHelper<T>.GetDynamicParameters(entity);

            var dbConnection = _transactionsRepository.GetConnection();
            if (dbConnection == null)
            {
                using (var connection = _context.CreateConnection())
                {
                    queryResult = (await connection.QueryAsync(query, parameters)).SingleOrDefault();
                }
            }
            else
            {
                var transaction = _transactionsRepository.GetTransaction();
                queryResult = (await dbConnection.QueryAsync(query, parameters, transaction)).SingleOrDefault();
            }

            if (queryResult?.Id > 0)
            {
                return (int)queryResult.Id;
            }

            return 0;
        }

        public async Task<bool> UpdateAsync(string idField, T entity)
        {
            var query = DynamicQueryHelper.GetUpdateQuery<T>(idField);
            var parameters = EntityHelper<T>.GetDynamicParameters(entity);

            var dbConnection = _transactionsRepository.GetConnection();
            if (dbConnection == null)
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, parameters);
                }
            }
            else
            {
                var transaction = _transactionsRepository.GetTransaction();
                await dbConnection.ExecuteAsync(query, parameters, transaction);
            }
            return true;
        }

        public async Task<bool> DeleteAsync(string fieldName, int value)
        {
            var query = DynamicQueryHelper.GetDeleteQuery<T>(fieldName);
            var dbConnection = _transactionsRepository.GetConnection();
            if (dbConnection == null)
            {
                using (var connection = _context.CreateConnection())
                {
                    await connection.ExecuteAsync(query, new { Value = value });
                }
            }
            else
            {
                var transaction = _transactionsRepository.GetTransaction();
                await dbConnection.ExecuteAsync(query, new { Id = value }, transaction);
            }
            return true;

        }

        private IDictionary<string, object> GetPropertyKeyValueObj(string propertyName, dynamic propertyValue)
        {
            IDictionary<string, object> propertyObj = new ExpandoObject();
            propertyObj[propertyName] = propertyValue;
            return propertyObj;
        }
    }
}
