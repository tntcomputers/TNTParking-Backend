using Context.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.DependencyInjectionContext
{
    public static class DependencyInjectionContext
    {
        public static void AddDependency(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("AppSettings");

            var databaseProvider = settings["DatabaseProvider"];// mssql,postgres
            var connectionStringPG = settings["ConnectionString:ConnectionStringTNTEcoIslandsPG"]; //.ConnectionString;
            var connectionStringMS = settings["ConnectionString:ConnectionStringTNTEcoIslandsMS"]; //.ConnectionString;

            //services.AddSingleton(s => new TNTAuthenticatorDBMongoHISTORYContext(settings));
            //services.AddSingleton(s => new TNTAuthenticatorDBMongoLOGContext(settings));
            switch (databaseProvider)
            {
                case "mssql":
                    services.AddDbContext<ITntParkingApplicationDbContext, TntParkingDbMssqlContext>(options => options.UseSqlServer(connectionStringMS), ServiceLifetime.Scoped);
                    break;
                case "postgres":
                    services.AddDbContext<ITntParkingApplicationDbContext, TntparkingContext>(options => options.UseNpgsql(connectionStringPG), ServiceLifetime.Scoped);
                    break;
                default:
                    break;
            }
        }
    }
}
