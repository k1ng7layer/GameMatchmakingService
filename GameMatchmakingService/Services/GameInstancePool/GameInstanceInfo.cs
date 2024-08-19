namespace GameMatchmakingService.Services.GameInstancePool;

public readonly struct GameInstanceInfo
{
    public readonly string ServerIp;

    public GameInstanceInfo(string serverIp)
    {
        ServerIp = serverIp;
    }
}