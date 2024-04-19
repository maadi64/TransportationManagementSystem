using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.Repository;
using TMS.Infrastructure.DataAcess;

namespace TMS.Infrastructure.Extensions
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private IDbTransaction dbTransaction;
        private IDbConnection? dbConnection;
        private readonly ApplicationDbContext _dbContext;

        public TransactionsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            this.dbConnection = null;
        }

        public IDbConnection? GetConnection()
        {
            return this.dbConnection;
        }

        public IDbTransaction GetTransaction()
        {
            return this.dbTransaction;
        }

        public void BeginTransaction()
        {
            try
            {
                this.dbConnection = _dbContext.CreateConnection();
                this.dbConnection.Open();
                this.dbTransaction = dbConnection.BeginTransaction();
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                Dispose();
            }
        }

        public void CommitTransaction()
        {
            try
            {
                dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
            }
        }

        public void RollBackTransaction()
        {
            dbTransaction.Rollback();
        }

        public void Dispose()
        {
            //Close the SQL Connection and dispose the objects
            dbTransaction.Dispose();
            dbConnection?.Close();
            dbConnection?.Dispose();
            dbConnection = null;
        }
    }
}
