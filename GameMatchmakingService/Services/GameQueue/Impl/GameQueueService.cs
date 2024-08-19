using System.Collections.Concurrent;
using GameMatchmakingService.Services.GameInstancePool;
using Newtonsoft.Json;

namespace GameMatchmakingService.Services.GameQueue.Impl;

public class GameQueueService : IGameQueueService
{
    private const byte PlayerCount = 6;
    
    private readonly IGameInstancePool _gameInstancePool;
    private readonly ConcurrentQueue<string> _players = new();

    public GameQueueService(IGameInstancePool gameInstancePool)
    {
        _gameInstancePool = gameInstancePool;
    }
    
    public void Enqueue(string playerLogin)
    {
        _players.Enqueue(playerLogin);

        if (_players.Count >= PlayerCount)
            CreateGame();
    }

    private async Task CreateGame()
    {
        var logins = new List<string>();
        
        for (int i = 0; i < PlayerCount; i++)
        {
            if (_players.TryDequeue(out var login))
                logins.Add(login);
        }
        
        var gameServer = await _gameInstancePool.StartGameInstance(logins);

        foreach (var login in logins)
        {
            
        }
    }
}