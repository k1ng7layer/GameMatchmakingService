namespace GameMatchmakingService.Models;

public readonly struct PlayerInfo
{
    public readonly string Login;
    public readonly string Token;

    public PlayerInfo(string login, string token)
    {
        Login = login;
        Token = token;
    }
}