using Microsoft.Extensions.DependencyInjection;

namespace XamWalletWebApi.Helpers
{
    public class CorsHelper
    {
        public static void ConfigureService(IServiceCollection service)
        {
            service.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }
    }
}
