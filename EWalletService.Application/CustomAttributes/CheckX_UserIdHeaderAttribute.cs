using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EWalletService.Application.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CheckX_UserIdHeaderAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (!context.HttpContext.Request.Headers.TryGetValue("X-UserId", out var UserIdheaderValues))
                {
                    context.Result = new BadRequestObjectResult($"Missing X-UserId header");
                    return;
                }
                if (string.IsNullOrWhiteSpace(UserIdheaderValues))
                {
                    context.Result = new BadRequestObjectResult($"X-UserId header is not valid");
                    return;
                }
                base.OnActionExecuting(context);
            }
            catch (Exception ex)
            {
                context.Result = new BadRequestObjectResult(ex.Message + $"! X-UserId header is not valid");
                return;
            }
        }
    }
}
