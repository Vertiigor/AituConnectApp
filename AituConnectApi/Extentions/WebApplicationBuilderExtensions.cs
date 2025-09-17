using AituConnectApi.Data;
using Microsoft.EntityFrameworkCore;

namespace AituConnectApi.Extentions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                dbContext.Database.Migrate();
            }
        }
    }
}
