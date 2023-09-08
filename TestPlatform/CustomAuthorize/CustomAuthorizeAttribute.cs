using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestPlatform.Infrastructure.Repositories.Tokens;

namespace TestPlatform.API.CustomAuthorize;

public class CustomAuthorizeAttribute : TypeFilterAttribute
{
    public CustomAuthorizeAttribute()
        : base(typeof(AuthorizeActionFilter))
    {
    }
}

public class AuthorizeActionFilter : IAsyncActionFilter
{
    private readonly ITokenRepository _tokenRepository;

    public AuthorizeActionFilter(ITokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var accestoken = context.HttpContext.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrEmpty(accestoken))
        {
            context.HttpContext.Response.StatusCode = 401;
            return;
        }

        accestoken = accestoken.Substring(7);

        var entityToken = await _tokenRepository.SelectByIdAsync(accestoken);

        if (entityToken == null)
        {
            context.HttpContext.Response.StatusCode = 401;
            return;
        }

        entityToken.LastActivity = DateTime.UtcNow;
        await _tokenRepository.UpdateAsync(entityToken);
        
        await next();
    }
}