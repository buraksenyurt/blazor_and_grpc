using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicLibrary.Data.Entity;

namespace MusicLibrary.Data
{
    /*
     * Çalışma zamanı DI servislerini Entity Framework DbContext türevimizi eklemek için kullanılan sınıf.
     * 
     */
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MusicLibraryDbContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("MusicLibraryDbConnection"))
                );

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<MusicLibraryDbContext>());
            return services;
        }
    }
}