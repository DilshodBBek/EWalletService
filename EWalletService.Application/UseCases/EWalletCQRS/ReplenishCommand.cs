using EWalletService.Application.Abstractions;
using EWalletService.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EWalletService.Application.UseCases.EWalletCQRS
{
    public class ReplenishCommand : IRequest<IActionResult>
    {
        [JsonProperty("amount")]
        [JsonRequired]
        public double PaymentAmount { get; set; }

        [JsonProperty("senderWalletId")]
        [JsonRequired]
        public int SenderWalletId { get; set; }

        [JsonProperty("receiverWalletId")]
        [JsonRequired]
        public int ReceiverWalletId { get; set; }
    }
    public class ReplenishCommandHandler : IRequestHandler<ReplenishCommand, IActionResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationDbContext _applicationDbContext;
        public ReplenishCommandHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext applicationDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IActionResult> Handle(ReplenishCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string? userId = _httpContextAccessor.HttpContext.Request.Headers["X-UserId"];

                EWallet? senderEWallet = await _applicationDbContext.Wallets
                                                       .FirstOrDefaultAsync(w => w.Id.Equals(request.SenderWalletId));
                if (senderEWallet is null)
                {
                    return new NotFoundObjectResult($"Sender EWallet with id={request.SenderWalletId} is not found");
                }
                else if (!senderEWallet.User.Id.Equals(userId))
                {
                    return new BadRequestObjectResult("User and Sender EWallet not compatible");
                }
                EWallet? receiverEWallet = _applicationDbContext.Wallets
                                                      .FirstOrDefault(w => w.Id.Equals(request.ReceiverWalletId));
                if (receiverEWallet is null)
                {
                    return new NotFoundObjectResult($"Receiver EWallet with id={request.ReceiverWalletId} is not found");
                }
                else if (senderEWallet.AmountOfMoney < request.PaymentAmount)
                {
                    return new BadRequestObjectResult("Unsufficient amount of money in Sender EWallet");
                }
                senderEWallet.AmountOfMoney -= request.PaymentAmount;
                receiverEWallet.AmountOfMoney += request.PaymentAmount;

                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
