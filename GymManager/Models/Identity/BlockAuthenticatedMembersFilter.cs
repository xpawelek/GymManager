using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GymManager.Controllers;

public class BlockAuthenticatedMembersFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (user.Identity?.IsAuthenticated == true && user.IsInRole("Member"))
        {
            context.Result = new ForbidResult("Members cannot create new accounts");
        }
    }
}