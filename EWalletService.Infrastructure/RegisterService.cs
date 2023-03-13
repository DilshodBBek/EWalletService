using EWalletService.Application.Abstractions;
using EWalletService.Infrastructure.Persistence;
using EWalletService.Infrastructure.Swagger;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EWalletService.Infrastructure
{
    public static class RegisterService
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("DBConnection"));
            });
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<ApiHeaderConfiguration>();
            });
            return services;
        }
    }
}
