using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Filters
{
    public class JwtAuthorizeAttribute: Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Cookies.TryGetValue("JwtToken", out string jwtToken))
            {
                context.Result = new RedirectToActionResult("Login", "Security", null);
            }
        }
    }
}
