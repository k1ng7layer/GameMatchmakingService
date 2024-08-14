namespace GameMatchmakingService.Services.HttpClient.Impl;

public class HttpClientService : IHttpClientService
{
    public async Task<HttpResponseMessage> SendRequest(string url, string body)
    {
        using var client = new System.Net.Http.HttpClient();
        var values = new Dictionary<string, string> { { "name", "alice" } };
        var content = new FormUrlEncodedContent(values);
        
        var response = await client.PostAsync(url, content);
        //var responseString = await response.Content.ReadAsStringAsync();

        return response;
    }

    public async Task<HttpResponseMessage> SendRequestAsync(
        HttpMethod method, 
        string url, 
        IEnumerable<KeyValuePair<string, string>> body
    )
    {
        using var client = new System.Net.Http.HttpClient();
        var values = new Dictionary<string, string> { { "name", "alice" } };
        var content = new FormUrlEncodedContent(values);
        var message = new HttpRequestMessage(HttpMethod.Post, url);
        message.Content = content;
        var response = await client.SendAsync(message);

        return response;
    }
}