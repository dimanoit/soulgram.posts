using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Soulgram.Posts.Api.Filters;

public class ValidationFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid)
        {
            return;
        }

        var errorList = context.ModelState
            .Select(model => new
            {
                model.Key,
                Value = model.Value.Errors.Select(err => err.ErrorMessage)
            });

        var jsonError = JsonConvert.SerializeObject(errorList);
        throw new ValidationException($"Parameter validation failed {jsonError}");
    }
}