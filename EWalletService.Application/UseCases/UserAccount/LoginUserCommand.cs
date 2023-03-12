using EWalletService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EWalletService.Application.UseCases.UserAccount
{
    public class LoginUserCommand:Credentials, IRequest<IActionResult>
    {
    }
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, IActionResult>
    {
        private SignInManager<IdentityUser> _signInManager { get; set; }
        private UserManager<IdentityUser> _userManager { get; set; }

        public LoginUserCommandHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.FindByEmailAsync(request.Username);
            if (result == null)
            {
                return new NotFoundObjectResult("User Not found");
            }
            await _signInManager.SignInAsync(result, false);
            return new OkResult();
        }
    }

}
