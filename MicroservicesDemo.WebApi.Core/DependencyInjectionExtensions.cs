using MicroservicesDemo.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.WebApi
{
    public static class DependencyInjectionExtensions
    {
        public static void RegisterDatabase<T>(this IServiceCollection services, string connectionString)
            where T : DbContext
        {
            services.AddDbContext<T>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DbContext, T>();
        }

        public static T RegisterSetting<T>(IServiceCollection services, IConfiguration configuration)
            where T : class, ILockable, new()
        {
            var settings = new T();
            configuration.Bind(typeof(T).Name, settings);
            settings.LockValues();
            services.AddSingleton(settings);
            return settings;
        }
    }
}
