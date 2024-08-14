using GameMatchmakingService.Models;

namespace GameMatchmakingService.Services.Authorization;

public interface IAuthorizationService
{
    Task<bool> AuthorizeAsync(PlayerInfo playerInfo);
}