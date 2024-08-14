using System.Net;
using GameMatchmakingService.Models;
using GameMatchmakingService.Services.HttpClient;

namespace GameMatchmakingService.Services.Authorization.Impl;

public class AuthorizationService : IAuthorizationService
{
    private readonly IHttpClientService _httpClientService;

    public AuthorizationService(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }
    
    public async Task<bool> AuthorizeAsync(PlayerInfo playerInfo)
    {
        var checkBody = new Dictionary<string, string>()
        {
            { "Login", $"{playerInfo.Login}" },
            { "Token", $"{playerInfo.Token}" }
        };

        var checkAuth = await _httpClientService.SendRequestAsync(
            HttpMethod.Get,
            Utils.Services.AuthenticationServiceUrl,
            checkBody);
        
        return checkAuth.StatusCode == HttpStatusCode.OK;
    }
}