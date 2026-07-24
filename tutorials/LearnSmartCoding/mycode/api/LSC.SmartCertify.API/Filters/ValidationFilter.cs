using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LSC.SmartCertify.API.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var arg in context.ActionArguments.Values)
        {
            if (arg is null) continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(arg.GetType());
            var validator = context.HttpContext.RequestServices.GetService(validatorType) as IValidator;

            if (validator is not null)
            {
                var validationResult = await validator.ValidateAsync(new ValidationContext<object>(arg));
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => new {e.PropertyName, e.ErrorMessage});
                    context.Result = new BadRequestObjectResult(errors);
                    return;
                }
            }
        }

        await next();
    }
}