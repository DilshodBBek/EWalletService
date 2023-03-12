using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EWalletService.Application.UseCases.UserAccount
{
    public class DeleteUserCommand:IRequest<IActionResult>
    {
        [JsonProperty("userId")]
        [JsonRequired]
        public required string UserId { get; set; }
    }
    public class DeleteUserCommandHanler : IRequestHandler<DeleteUserCommand, IActionResult>
    {
        private SignInManager<IdentityUser> _signInManager { get; set; }
        private UserManager<IdentityUser> _userManager { get; set; }

        public DeleteUserCommandHanler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Handle(DeleteUserCommand user, CancellationToken cancellationToken)
        {
            //if (string.IsNullOrEmpty(user.UserId))
            //{
                return new BadRequestObjectResult("UserId cannot be null or empty");
            //}
            //IdentityUser _user = _userManager.FindByIdAsync(user.UserId).Result;
            //if (user == null)
            //{
            //    return new NotFoundObjectResult("UserId not found");
            //}

            //if (_identifyWalletService.IsWalletExist(user.UserId) || _unidentifyWalletService.IsWalletExist(_user.UserId))
            //{
            //    return BadRequest("This user have Wallet please remove wallet before delete user!");
            //}
            //var result = await _userManager.DeleteAsync(_user);
            //if (!result.Succeeded)
            //{
            //    return BadRequest(result.Errors);
            //}
            //return Ok();
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