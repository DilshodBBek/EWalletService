using EWalletService.Application.UseCases.UserAccount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EWalletService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediatr;
        public UserController(IMediator mediator)=>
            _mediatr = mediator;

        [HttpPost("/register")]
        public async Task<IActionResult> Register(RegisterUserCommand credential)
        {
            return await _mediatr.Send(credential);
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand logincredential)
        {
            return await _mediatr.Send(logincredential);
        }
        
        [Authorize] //Only use after Login or Register which signed in the system
        [HttpPost("/logout")]
        public async Task<IActionResult> Logout()
        {
            return await _mediatr.Send(new LogoutUserCommand());
        }
        
        [Authorize] //Only use after Login or Register which signed in the system
        [HttpDelete("/delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommand _user)
        {
            return await _mediatr.Send(_user);
        }
    }
}
