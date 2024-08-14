namespace GameMatchmakingService.Services.HttpClient;

public interface IHttpClientService
{
    Task<HttpResponseMessage> SendRequestAsync(
        HttpMethod method, 
        string url, 
        IEnumerable<KeyValuePair<string, string>> body
        );
}