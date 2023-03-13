using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EWalletService.Application.UseCases.UserAccount
{
    public class GetUserIdCommand : IRequest<IActionResult>
    {
    }
    public class GetUserIdCommandHandler : IRequestHandler<GetUserIdCommand, IActionResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private UserManager<IdentityUser> _userManager { get; set; }

        public GetUserIdCommandHandler(UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Handle(GetUserIdCommand request, CancellationToken cancellationToken)
        {
            IdentityUser? user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            return new OkObjectResult("Current UserId=" + user?.Id);
        }
    }
}
