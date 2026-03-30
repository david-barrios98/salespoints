using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using salespoints.Application.DTOs.Auth;

namespace salespoints.Api.Filters
{
    public class SwaggerSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(LoginRequestDTO))
            {
                schema.Example = new Microsoft.OpenApi.Any.OpenApiObject
                {
                    ["username"] = new Microsoft.OpenApi.Any.OpenApiString("Admin"),
                    ["password"] = new Microsoft.OpenApi.Any.OpenApiString("cc1")
                };
            }
        }
    }
}