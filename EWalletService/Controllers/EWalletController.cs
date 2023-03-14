using EWalletService.Application.CustomAttributes;
using EWalletService.Application.UseCases.EWalletCQRS;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        public EWalletController(IMediator mediatr)
        {
            _mediatr = mediatr;
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
        public async Task<IActionResult> Balance([FromBody] BalanceCommand walletID)
        {
            return await _mediatr.Send(walletID);
        }

        

        [HttpPost]
        public async Task<IActionResult> Replenish([FromBody] ReplenishCommand model)
        {
            return await _mediatr.Send(model);
        }

        [HttpPost]
        public async Task<IActionResult> Statistics([FromBody] StatisticsCommand statisticsModel)
        {
            return await _mediatr.Send(statisticsModel);
        }
        

      

        
    }
}
