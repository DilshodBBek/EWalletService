using EWalletService.Application.Abstractions;
using EWalletService.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWalletService.Infrastructure.Persistence
{
    public class ApplicationDbContext: IdentityDbContext<UserAccount>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {
            
        }
        public DbSet<EWallet> Wallets { get; set; }
    }
}
