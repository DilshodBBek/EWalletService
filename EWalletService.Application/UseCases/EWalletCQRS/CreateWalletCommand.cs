using EWalletService.Application.Abstractions;
using EWalletService.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EWalletService.Application.UseCases.EWalletCQRS
{
    public class CreateWalletCommand : IRequest<IActionResult>
    {
        public bool IsIdentified { get; set; }
        internal int AmountOfMoney;
        [Range(0, 100)]
        public int Amount
        {
            get { return AmountOfMoney; }
            set
            {
                if (!IsIdentified)
                {
                    if (value > 50)
                    {
                        throw new ArgumentException("Money in e-wallet cannot be greater than 50 if user is identified.");
                    }
                }
                AmountOfMoney = value;

            }
        }

    }
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, IActionResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public CreateWalletCommandHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string userId = _httpContextAccessor.HttpContext.Request.Headers["X-UserId"].ToString();
                IdentityUser? user = _userManager.FindByIdAsync(userId).Result;
                if (user is null)
                {
                    return new NotFoundObjectResult($"User with Id=\"{userId}\" not found");
                }
                EWallet ewallet = new()
                {
                    IsIdentified = request.IsIdentified,
                    AmountOfMoney = request.AmountOfMoney,
                    User = user
                };

                await _applicationDbContext.Wallets.AddAsync(ewallet);
                await _applicationDbContext.SaveChangesAsync();
                return new OkResult();

            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
