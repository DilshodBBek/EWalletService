using EWalletService.Application.UseCases.UserAccount.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EWalletService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediatr;
        public UserController(IMediator mediator)
        {
            _mediatr = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommand credential)
        {
            var result = await _mediatr.Send(credential);
            return !result.IsSuccess ? BadRequest(result.ErrorMessage) : Ok();
        }

    }
}
