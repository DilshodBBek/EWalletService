﻿using EWalletService.Application.Abstractions;
using EWalletService.Domain.Models;
using EWalletService.Infrastructure.Persistence;
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
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"));
            });
            services.AddIdentity<UserAccount, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
            return services;
        }
    }
}
