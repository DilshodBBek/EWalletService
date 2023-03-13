using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace EWalletService.Infrastructure.Swagger
{
    public class ApiHeaderConfiguration : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            foreach (var tag in operation.Tags)
            {
                if (tag.Name?.CompareTo("User") != 0)
                {
                    operation.Parameters.Add(new OpenApiParameter()
                    {
                        Name = "X-UserId",
                        Description = "UserId",
                        In = ParameterLocation.Header,
                        Schema = new OpenApiSchema() { Type = "string" },
                        Required = true,
                        Example = new OpenApiString("Any UserId")
                    });

                    operation.Parameters.Add(new OpenApiParameter()
                    {
                        Name = "X-Digest",
                        Description = "X-Digest is hmac-sha1, the hash sum of the request body",
                        In = ParameterLocation.Header,
                        Required = true,
                        Schema = new OpenApiSchema() { Type = "string" },
                        Example = new OpenApiString("hmac-sha1 hash string")
                    });
                }
            }
        }
    }
}
