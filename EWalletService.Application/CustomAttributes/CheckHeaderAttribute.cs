using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text;

namespace EWalletService.Application.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CheckX_DigestHeaderAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (!context.HttpContext.Request.Headers.TryGetValue("X-Digest", out var DigestheaderValues))
                {
                    context.Result = new BadRequestObjectResult($"Missing X-Digest header");
                }
                string bodystring = "";
                var body = context.HttpContext.Request.Body;
                using (StreamReader sr = new(body))
                {
                    if (body.CanSeek)
                        body.Seek(0, SeekOrigin.Begin);
                    if (body.CanRead)
                        bodystring = sr.ReadToEndAsync().Result;
                }
                string Hashbody = GetHash(bodystring);

                if (Hashbody.Equals(DigestheaderValues))
                {
                    Console.WriteLine("X-Digest is valid.");
                }
                else
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Result = new BadRequestObjectResult("Request is not valid.Request Body and X-Digest HMACSHA1 are not the same! ");
                    return;
                }
                string GetHash(string value)
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

                base.OnActionExecuting(context);
            }
            catch (Exception e)
            {
                context.Result = new BadRequestObjectResult(e.Message + "! Request is not valid.");
                return;
            }
        }
    }
}
