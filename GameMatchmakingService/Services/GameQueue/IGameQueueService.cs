namespace GameMatchmakingService.Services.GameQueue;

public interface IGameQueueService
{
    void Enqueue(string playerLogin);
}