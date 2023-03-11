using EWalletService.Application.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
            return services;
        }
    }
}
