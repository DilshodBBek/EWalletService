using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace EWalletService.Application.Midllewares
{
    public class XUserIdValidationMidlleware
    {
        private readonly RequestDelegate _next;

        public XUserIdValidationMidlleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (context.Request.Path.ToString().StartsWith("/api/EWallet/"))
                {
                    string userId = context.Request.Headers["X-UserId"].ToString();
                    if (int.TryParse(userId, out int user_id))
                    {
                        await _next(context);
                    }

                }
                else
                {
                    await _next(context);
                }

            }
            catch (Exception e)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync(e.Message);
            }
        }
    }
    public static class RequestXUserIdValidationMidlleware
    {
        public static IApplicationBuilder UseXUserIdValidation(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<XUserIdValidationMidlleware>();
        }
    }
}

