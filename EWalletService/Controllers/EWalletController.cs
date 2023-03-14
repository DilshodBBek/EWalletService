using EWalletService.Application.CustomAttributes;
using EWalletService.Application.UseCases.EWalletCQRS;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EWalletService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [CheckX_DigestHeader]
    [CheckX_UserIdHeader]
    //In fact Users must have authorize(login) before getting or setting their EWallets
    //[Authorize]
    public class EWalletController : Controller
    {
        private readonly IMediator _mediatr;
        private readonly IMemoryCache _cache;
        public EWalletController(IMediator mediatr, IMemoryCache cache)
        {
            _mediatr = mediatr;
            _cache = cache;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletCommand wallet)
        {
            return await _mediatr.Send(wallet);
        }

        ////Get All Wallets for X-UserId
        //[HttpGet]
        //public async Task<IActionResult> GetAllWallets([FromBody] GetAllWalletsCommand request)
        //{
        //    return await _mediatr.Send(request);
        //}
        
        [HttpPost]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> Balance([FromBody] BalanceCommand walletID)
        {
            string cacheKey = $"Balance_{walletID.WalletId}";
            if (_cache.TryGetValue(cacheKey, out IActionResult response))
            {
                return response;
            }
            response = await _mediatr.Send(walletID);
            _cache.Set(cacheKey, response, TimeSpan.FromSeconds(30));

            return response;
        }

        [HttpPost]
        public async Task<IActionResult> Replenish([FromBody] ReplenishCommand model)
        {
            return await _mediatr.Send(model);
        }

        [HttpPost]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> Statistics([FromBody] StatisticsCommand statisticsModel)
        {
            string cacheKey = $"Statistics_{statisticsModel.StartDate}_{statisticsModel.EndDate}";
            if (_cache.TryGetValue(cacheKey, out IActionResult response))
            {
                return response;
            }

            // If not, send the command to the mediator and cache the result
            response = await _mediatr.Send(statisticsModel);
            _cache.Set(cacheKey, response, TimeSpan.FromSeconds(30));

            return response;
        }
    }
}
