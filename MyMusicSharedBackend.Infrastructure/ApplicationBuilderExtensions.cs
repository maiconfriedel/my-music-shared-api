using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KylyOrderPicking.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder MyMusicSharedMigrateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using var context = serviceScope.ServiceProvider.GetService<MyMusicSharedBackend.Infrastructure.EntityFramework.MyMusicSharedDbContext>();
                context.Database.Migrate();
            }

            return app;
        }
    }
}