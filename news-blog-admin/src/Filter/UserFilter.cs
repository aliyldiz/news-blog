using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NewsBlogAdmin.Filter;

public class UserFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("id");
        
        if (!userId.HasValue)
        {
            context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    {"controller", "Home"},
                    {"action", "Index"}
                });
        }
    }
}