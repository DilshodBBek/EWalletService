using EWalletService.Application.UseCases.EWalletCQRS;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EWalletService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //In fact Users must have authorize(login) before getting or setting their EWallets
    //[Authorize]
    
    public class EWalletController : Controller
    {
        private readonly IMediator _mediatr;

        public EWalletController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<IActionResult> Replenish(ReplenishCommand model)
        {
            return await _mediatr.Send(model);
        }

        [HttpPost]
        public async Task<IActionResult> Statistics(StatisticsCommand statisticsModel)
        {
            return await _mediatr.Send(statisticsModel);
        }
        [HttpPost]
        public async Task<IActionResult> Balance(GetEWalletCommand walletID)
        {
            return await _mediatr.Send(walletID);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIdentifyWallet([FromBody] CreateWalletCommand wallet)
        {
            return await _mediatr.Send(wallet);
        }
    }
}
