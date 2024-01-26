using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Net;

namespace FullCart.Api.Filters;

public class ApiActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result == null)
            return;

        var objResult = ((ObjectResult)context.Result).Value;
        var objType = objResult.GetType();

        if (objType.FullName.Contains("Common.Shared.Result"))
        {
            var status = (HttpStatusCode)objType.GetProperty("HttpStatusCode").GetValue(objResult, null);

            if (status == HttpStatusCode.OK)
            {
                var data = objType.GetProperty("Data").GetValue(objResult, null);
                context.Result = new ObjectResult(data);

            }
            else if (status == HttpStatusCode.BadRequest)
            {
                var errors = (string[])objType.GetProperty("Errors").GetValue(objResult, null);

                var details = new ValidationProblemDetails(new Dictionary<string, string[]> { { "validation", errors } })
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                };

                context.Result = new BadRequestObjectResult(details);
            }
            else if (status == HttpStatusCode.NotFound)
            {
                var errors = (string[])objType.GetProperty("Errors").GetValue(objResult, null);

                var details = new ValidationProblemDetails(new Dictionary<string, string[]> { { "validation", errors } })
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                };

                context.Result = new NotFoundObjectResult(details);
            }
            else if (status == HttpStatusCode.InternalServerError)
            {
                var errors = (string[])objType.GetProperty("Errors").GetValue(objResult, null);

                var details = new ValidationProblemDetails(new Dictionary<string, string[]> { { "validation", errors } })
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                };

                context.Result = new ObjectResult(details);
            }
            else if (status == HttpStatusCode.Unauthorized)
            {
                context.Result = new UnauthorizedResult();
            }
        }

    }
}