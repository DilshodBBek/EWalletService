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
    public class GetAllUsersCommand:IRequest<IActionResult>
    {
    }
    public class GetAllUsersCommandHandler : IRequestHandler<GetAllUsersCommand, IActionResult>
    {
        private UserManager<IdentityUser> _userManager { get; set; }

        public GetAllUsersCommandHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
        {
            return new OkObjectResult(_userManager.Users.ToList());
        }
    }
}
