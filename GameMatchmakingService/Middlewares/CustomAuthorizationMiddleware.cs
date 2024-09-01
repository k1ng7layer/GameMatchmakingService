using GameMatchmakingService.Models;
using GameMatchmakingService.Services.Authorization;
using Newtonsoft.Json;
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace GameMatchmakingService.Middlewares;

public class CustomAuthorizationMiddleware
{
    private readonly RequestDelegate _requestDelegate;

    public CustomAuthorizationMiddleware(RequestDelegate requestDelegate)
    {
        _requestDelegate = requestDelegate;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        var authorizationService = context.RequestServices.GetService<IAuthorizationService>();

        var header = context.Request.Headers["Authorization"].ToString();
        var info = GetLoginWithToken(header);

        if (info == null)
        {
            context.Response.StatusCode = 401;
            return;
        }
       
        context.Response.StatusCode = 401;
        var result = await authorizationService.AuthorizeAsync(new PlayerInfo(info[0], info[1]));

        if (!result)
        {
            if (info.Length != 2)
            {
                context.Response.StatusCode = 401;
                return;
            }
        }

        await _requestDelegate.Invoke(context);
    }

    private string[]? GetLoginWithToken(string header)
    {
        if (string.IsNullOrEmpty(header))
            return null;
        
        var info = header.Split(';');

        return info.Length != 2 ? null : info;
    } 
}