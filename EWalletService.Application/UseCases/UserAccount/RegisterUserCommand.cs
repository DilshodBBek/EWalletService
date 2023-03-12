using EWalletService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EWalletService.Application.UseCases.UserAccount
{
    public class RegisterUserCommand : Credentials, IRequest<IActionResult>
    {
    }
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IActionResult>
    {
        private SignInManager<IdentityUser> _signInManager { get; set; }
        private UserManager<IdentityUser> _userManager { get; set; }

        public RegisterUserCommandHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        async Task<IActionResult> IRequestHandler<RegisterUserCommand, IActionResult>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                IdentityUser user = new()
                {
                    UserName = request.Username,
                    Email = request.Username
                };
                var result = await _userManager.CreateAsync(user, request.Password);//?? throw new Exception("Cannot generate response");

                if (!result.Succeeded)
                    return new BadRequestObjectResult(result.Errors);
                await _signInManager?.SignInAsync(user, false);
                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
