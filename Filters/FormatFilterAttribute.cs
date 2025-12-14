using Microsoft.AspNetCore.Mvc.Filters;

namespace LegislativeEnumsNew.Filters;

/// <summary>
/// Action filter that sets the response format based on query parameter.
/// Supports ?format=json and ?format=xml
/// </summary>
public class ApiFormatFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var format = context.HttpContext.Request.Query["format"].FirstOrDefault()?.ToLowerInvariant();

        if (!string.IsNullOrEmpty(format))
        {
            var contentType = format switch
            {
                "xml" => "application/xml",
                "json" => "application/json",
                _ => null
            };

            if (contentType != null)
            {
                // Override the Accept header to force the response format
                context.HttpContext.Request.Headers.Accept = contentType;
            }
        }

        base.OnActionExecuting(context);
    }
}
