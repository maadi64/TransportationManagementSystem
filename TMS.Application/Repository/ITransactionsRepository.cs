using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Application.Repository
{
    public interface ITransactionsRepository
    {
        IDbConnection? GetConnection();
        IDbTransaction GetTransaction();
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        void Dispose();
    }
}
