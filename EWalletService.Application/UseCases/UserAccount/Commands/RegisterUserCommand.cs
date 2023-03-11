using EWalletService.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EWalletService.Application.UseCases.UserAccount.Commands
{
    public class RegisterUserCommand : Credentials, IRequest<RegisterUserCommandResponse>
    {
    }
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>
    {
        private SignInManager<Domain.Models.UserAccount> _signInManager { get; set; }
        private UserManager<Domain.Models.UserAccount> _userManager { get; set; }

        public RegisterUserCommandHandler(UserManager<Domain.Models.UserAccount> userManager, SignInManager<Domain.Models.UserAccount> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        async Task<RegisterUserCommandResponse> IRequestHandler<RegisterUserCommand, RegisterUserCommandResponse>.Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Domain.Models.UserAccount user = new()
                {
                    UserName = request.Username,
                    Email = request.Username,
                    IsIdentified = request.IsIdentified
                };
                var result = await _userManager.CreateAsync(user, request.Password);//?? throw new Exception("Cannot generate response");

                if (!result.Succeeded)
                    return new();
               await _signInManager?.SignInAsync(user, false);
                return new();
            }
            catch (Exception e)
            {
                return new() { IsSuccess = false, ErrorMessage = e.Message };
            }
        }
    }
    public class RegisterUserCommandResponse
    {
        public bool IsSuccess { get; set; } = true;
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
