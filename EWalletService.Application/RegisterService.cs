using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EWalletService.Application
{
    public static class RegisterService
    {
        public static  IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR((config) =>
            {
                config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });
            services.AddHttpContextAccessor();
            return services;
        }

       
    }
}
