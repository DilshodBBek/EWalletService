using EWalletService.Application.Abstractions;
using EWalletService.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EWalletService.Application.UseCases.EWalletCQRS
{
    public class StatisticsCommand : IRequest<IActionResult>
    {
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }
    }
    public class StatisticsCommandHandler : IRequestHandler<StatisticsCommand, IActionResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public StatisticsCommandHandler(IHttpContextAccessor httpContextAccessor,
            IApplicationDbContext applicationDbContext,
            UserManager<IdentityUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Handle(StatisticsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string? userId = _httpContextAccessor.HttpContext.Request.Headers["X-UserId"];
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return new NotFoundObjectResult($"User with X-UserId={userId} is not found");
                }
                IdentityUser? user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    List<EWallet> result = _applicationDbContext.Wallets.Where(w => w.User.Id.Equals(user.Id)).ToList();

                    return new OkObjectResult(result);
                }
                return new NotFoundObjectResult($"User with id={userId} not found");
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

        }
    }
}
