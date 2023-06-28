using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyProtocolsAPI_Roy.Attributes
{

    [AttributeUsage(validOn: AttributeTargets.All)]

    public sealed class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
       private readonly string _apiKey = "Pro6RoyApiKey";

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        if (!context.HttpContext.Request.Headers.TryGetValue(_apiKey, out var ApiSalida))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "Llamada no contiene informacion de seguridad..."

            };
            return;
        }

        var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

        var ApikeyValue = appSettings.GetValue<string>(_apiKey);

        if (!ApikeyValue.Equals(ApiSalida))
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content = "ApiKey Invalidad."
            };

            return;
        }

        await next();

    }
  }
}
