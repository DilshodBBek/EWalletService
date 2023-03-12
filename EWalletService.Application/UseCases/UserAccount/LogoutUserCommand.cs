using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWalletService.Application.UseCases.UserAccount
{
    public class LogoutUserCommand:IRequest<IActionResult>
    {
    }
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, IActionResult>
    {
        private SignInManager<IdentityUser> _signInManager { get; set; }

        public LogoutUserCommandHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            return new OkResult();
        }
    }
}
