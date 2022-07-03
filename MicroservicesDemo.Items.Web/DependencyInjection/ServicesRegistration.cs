using MicroservicesDemo.Queries;
using MicroservicesDemo.Web;
using MicroservicesDemo.WebApi;

namespace MicroservicesDemo.Items.Web.DependencyInjection
{
    public static class ServicesRegistration
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var authSettings = DependencyInjectionExtensions.RegisterSetting<AuthSettings>(services, configuration);
            services.AddTokenAuthentication(authSettings);

            services.AddScoped<JwtService>();
        }
    }
}
