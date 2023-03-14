using EWalletService.Application.Abstractions;
using EWalletService.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWalletService.Application.UseCases.EWalletCQRS
{
    public class GetAllWalletsCommand : IRequest<IActionResult>
    {
        public bool GetAllUsers  {get; set;}
    }
    public class GetAllWalletsCommandHandler : IRequestHandler<GetAllWalletsCommand, IActionResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IApplicationDbContext _applicationDbContext;
        public GetAllWalletsCommandHandler(IHttpContextAccessor httpContextAccessor, IApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _applicationDbContext = applicationDbContext;
        }
        public async Task<IActionResult> Handle(GetAllWalletsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                string? userId = _httpContextAccessor.HttpContext.Request.Headers["X-UserId"];

                List<EWallet> result = _applicationDbContext.Wallets.Where(w => w.User.Id.Equals(userId)).ToList();
                
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}
