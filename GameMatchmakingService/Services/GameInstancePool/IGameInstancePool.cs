namespace GameMatchmakingService.Services.GameInstancePool;

public interface IGameInstancePool
{
    Task<GameInstanceInfo> StartGameInstance(IEnumerable<string> players);
}