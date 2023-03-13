using EWalletService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EWalletService.Application.Abstractions
{
    public interface IApplicationDbContext
    {
       // DbSet<UserAccount> Users { get; }
        DbSet<EWallet> Wallets { get; }
        DbSet<TransactionHistory> TransactionsHistory { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
