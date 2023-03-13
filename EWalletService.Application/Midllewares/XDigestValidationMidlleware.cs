using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;

namespace EWalletService.Application.Midllewares
{
    public class XDigestValidationMidlleware
    {
        private readonly RequestDelegate _next;

        public XDigestValidationMidlleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                
                if(context.Request.Path.ToString().StartsWith("/api/Wallet/"))
                {
                    string digest = context.Request.Headers["X-Digest"].ToString();
                    string bodystring = "";
                    var body = context.Request.Body;
                    using (StreamReader sr = new(body))
                    {
                        if (body.CanSeek)
                            body.Seek(0, SeekOrigin.Begin);
                        if (body.CanRead)
                            bodystring = sr.ReadToEndAsync().Result;
                    }
                    string Hashbody = GetHash(bodystring);

                    if (Hashbody.Equals(digest))
                    {
                        // Call the next delegate/middleware in the pipeline.
                        Console.WriteLine("X-Digest is valid.");
                        await _next(context);
                    }
                    else
                    {
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        await context.Response.WriteAsync("Request is not valid.Body may be changed by hackers");

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
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(e.Message);
            }
        }
        private string GetHash(string value)
        {
            var sha1 = new System.Security.Cryptography.SHA1Managed();
            var plaintextBytes = Encoding.UTF8.GetBytes(value);
            var hashBytes = sha1.ComputeHash(plaintextBytes);

            var sb = new StringBuilder();
            foreach (var hashByte in hashBytes)
            {
                sb.AppendFormat("{0:x2}", hashByte);
            }

            var hashString = sb.ToString();
            return hashString;
        }
    }
    public static class RequestXDigestValidation
    {
        public static IApplicationBuilder UseXDigestValidation(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<XDigestValidationMidlleware>();
        }
    }
}
