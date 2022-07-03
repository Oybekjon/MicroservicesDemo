using MicroservicesDemo.Queries;
using MicroservicesDemo.Users.Queries.Settings;
using MicroservicesDemo.Web;
using MicroservicesDemo.WebApi;

namespace MicroservicesDemo.Users.Web.DependencyInjection
{
    public static class ServicesRegistration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            DependencyInjectionExtensions.RegisterSetting<HashConfig>(services, configuration);
            var authSettings = DependencyInjectionExtensions.RegisterSetting<AuthSettings>(services, configuration);
            services.AddTokenAuthentication(authSettings);

            services.AddScoped<JwtService>();
        }
    }
}
