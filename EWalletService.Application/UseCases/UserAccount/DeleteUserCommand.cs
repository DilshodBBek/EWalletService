using EWalletService.Application.Abstractions;
using EWalletService.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EWalletService.Application.UseCases.UserAccount
{
    public class DeleteUserCommand : IRequest<IActionResult>
    {
        [JsonProperty("userId")]
        [JsonRequired]
        public required string UserId { get; set; }
    }
    public class DeleteUserCommandHanler : IRequestHandler<DeleteUserCommand, IActionResult>
    {
        private SignInManager<IdentityUser> _signInManager { get; set; }
        private UserManager<IdentityUser> _userManager { get; set; }
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteUserCommandHanler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IApplicationDbContext applicationDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.UserId))
                {
                    return new BadRequestObjectResult("UserId cannot be null or empty");
                }
                IdentityUser? user = await _userManager.FindByIdAsync(request.UserId);

                if (user == null)
                {
                    return new NotFoundObjectResult("UserId not found");
                }
                IEnumerable<EWallet> userWallets = _applicationDbContext.Wallets.Where(w => w.User.Id.Equals(user.Id));

                _applicationDbContext.Wallets.RemoveRange(userWallets);
                await _userManager.DeleteAsync(user);

                return new OkObjectResult($"{userWallets.Count()} wallets was deleted");
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

    }
}

//if (string.IsNullOrEmpty(_user.UserId))
//{
//    return BadRequest("UserId cannot be null or empty");
//}
//IdentityUser user = _userManager.FindByIdAsync(_user.UserId).Result;
//if (user == null)
//{
//    return NotFound(_user.UserId + " -> UserId not found");
//}

//if (_identifyWalletService.IsWalletExist(_user.UserId) || _unidentifyWalletService.IsWalletExist(_user.UserId))
//{
//    return BadRequest("This user have Wallet please remove wallet before delete user!");
//}
//var result = await _userManager.DeleteAsync(user);
//if (!result.Succeeded)
//{
//    return BadRequest(result.Errors);
//}
//return Ok();