using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Application.Generic;
using TMS.Application.Repository;
using TMS.Infrastructure.DataAcess;
using TMS.Infrastructure.Generics;

namespace TMS.Infrastructure.Extensions
{
    public static class RepositoryRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<ApplicationDbContext>();
            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddSingleton<ITransactionsRepository, TransactionsRepository>();
        }
    }
}
