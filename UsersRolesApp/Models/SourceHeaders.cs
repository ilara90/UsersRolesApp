using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UsersRolesApp.Models
{
    /// <summary>
    /// Обработка запросов
    /// </summary>
    public class SourceHeaders : IOperationFilter
    {
        /// <summary>
        /// Заголовок пользователя
        /// </summary>
        public const string UserId = "userId";

        public OpenApiSchema GuidScheme = new OpenApiSchema
        {
            Type = "string",
            Format = "uuid"
        };

        /// <summary>
        /// Добавление параметра в заголовок запроса
        /// </summary>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            if (!context.ApiDescription.ActionDescriptor.RouteValues.TryGetValue("controller",
                    out string controllerName))
            {
                return;
            }

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = UserId,
                In = ParameterLocation.Header,
                Schema = GuidScheme,
                Description = "Пользователь",
                Required = true
            });
        }
    }
}
