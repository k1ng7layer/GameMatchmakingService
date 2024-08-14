using System.Net;
using GameMatchmakingService.Models;
using GameMatchmakingService.Services.HttpClient;
using Newtonsoft.Json;

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
        var json = JsonConvert.SerializeObject(playerInfo);
        // var checkBody = new Dictionary<string, string>()
        // {
        //     { "PlayerInfo", $"{json}" },
        // };

        var checkAuth = await _httpClientService.SendRequestAsync(
            HttpMethod.Post,
            Utils.Services.AuthenticationServiceUrl + "/Account/Validate",
            json);
        
        return checkAuth.StatusCode == HttpStatusCode.OK;
    }
}