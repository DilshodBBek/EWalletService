using EWalletService.Application.UseCases.UserAccount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EWalletService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediatr;
        public UserController(IMediator mediator)=>
            _mediatr = mediator;

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommand credential)
        {
            return await _mediatr.Send(credential);
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand logincredential)
        {
            return await _mediatr.Send(logincredential);
        }
        
        [HttpPost]
        [Authorize] //Only use after Login or Register which signed in the system
        public async Task<IActionResult> Logout()
        {
            return await _mediatr.Send(new LogoutUserCommand());
        }
        
        [Authorize] //Only use after Login or Register which signed in the system
        [HttpGet]
        public async Task<IActionResult> GetCurrentUserId()
        {
            return await _mediatr.Send(new GetUserIdCommand());        
        }

        [HttpGet]
        [Authorize] //Only use after Login or Register which signed in the system
        public async Task<IActionResult> GetAllUsers()
        {
            return await _mediatr.Send(new GetAllUsersCommand());
        }

        [Authorize] //Only use after Login or Register which signed in the system
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommand _user)
        {
            return await _mediatr.Send(_user);
        }
    }
}
