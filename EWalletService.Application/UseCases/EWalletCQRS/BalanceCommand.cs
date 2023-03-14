using EWalletService.Application.Abstractions;
using EWalletService.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EWalletService.Application.UseCases.EWalletCQRS
{
    public class BalanceCommand : IRequest<IActionResult>
    {
        [JsonProperty("walletId")]
        [JsonRequired]
        public required int WalletId { get; set; }
    }
    public class BalanceCommandHandler : IRequestHandler<BalanceCommand, IActionResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationDbContext _applicationDbContext;
        public BalanceCommandHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IActionResult> Handle(BalanceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string? userId = _httpContextAccessor.HttpContext.Request.Headers["X-UserId"];

                EWallet? eWallet = await _applicationDbContext.Wallets
                                         .FirstOrDefaultAsync(w => w.Id.Equals(request.WalletId) &&
                                                                            w.User.Id.Equals(userId));
                if (eWallet == null)
                {
                    return new NotFoundObjectResult($"EWallet with id={request.WalletId} is not found");
                }
                return new OkObjectResult(eWallet);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }

}
