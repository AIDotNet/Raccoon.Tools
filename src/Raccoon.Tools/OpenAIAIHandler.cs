namespace Watermelon.Service;

public sealed class OpenAiHandler(string url) : HttpClientHandler
{
    private string Endpoint => url.Trim('/');

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.RequestUri =
            new Uri(request.RequestUri.ToString().Replace("https://api.openai.com", Endpoint));

        return await base.SendAsync(request, cancellationToken);
    }
}