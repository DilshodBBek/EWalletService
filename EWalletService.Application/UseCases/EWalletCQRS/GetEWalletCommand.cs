using EWalletService.Application.Abstractions;
using EWalletService.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EWalletService.Application.UseCases.EWalletCQRS
{
    public class GetEWalletCommand : IRequest<ContentResult>
    {
        [JsonProperty("walletId")]
        [JsonRequired]
        public required int WalletId { get; set; }
    }
    public class GetEWalletCommandHandler : IRequestHandler<GetEWalletCommand, IActionResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationDbContext _applicationDbContext;
        public GetEWalletCommandHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IActionResult> Handle(GetEWalletCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string? userId = _httpContextAccessor.HttpContext.Request.Headers["X-UserId"];

                EWallet? eWallet = await _applicationDbContext.Wallets
                                                       .FirstOrDefaultAsync(w => w.Id.Equals(request.WalletId));
                if (eWallet == null)
                {
                    return new NotFoundObjectResult($"EWallet with id={request.WalletId} is not found");
                }
                if (!eWallet.User.Id.Equals(userId))
                {
                    return new BadRequestObjectResult("User and EWallet not compatible");
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
