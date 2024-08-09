using System.Net.Http.Headers;
using Raccoon.Tools.Services;

namespace Raccoon.Tools;

public sealed class OpenAiHandler(string url) : HttpClientHandler
{
    private string Endpoint => url.Trim('/');

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = TokenService.GetToken();

        // 修改token
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        request.RequestUri =
            new Uri(request.RequestUri.ToString().Replace("https://api.openai.com", Endpoint));

        return await base.SendAsync(request, cancellationToken);
    }
}