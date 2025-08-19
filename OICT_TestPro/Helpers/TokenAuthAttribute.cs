namespace OICT_Test.Helpers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class TokenAuthAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("x-token", out var token))
            {
                context.Result = new UnauthorizedObjectResult("Chybí token");
                return;
            }

            if (!TokenStore.Tokens.TryGetValue(token!, out var expires) || expires < DateTime.UtcNow)
            {
                context.Result = new UnauthorizedObjectResult("Neplatný nebo vypršelý token");
                return;
            }
        }
    }

}
