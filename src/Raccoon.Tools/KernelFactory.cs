using System.Collections.Concurrent;
using Microsoft.SemanticKernel;
using Watermelon.Service;

#pragma warning disable SKEXP0010

namespace Raccoon.Tools;

public class KernelFactory
{
    private static readonly ConcurrentDictionary<string, Kernel> Kernels = new();


    public static Kernel Create(string name = "default")
    {
        return Kernels.GetOrAdd(name, key =>
        {
            var client = new HttpClient(new OpenAiHandler("https://api.token-ai.cn/"));

            var kernel = Kernel.CreateBuilder()
                .AddOpenAIChatCompletion("gpt-4o-mini", "", httpClient: client)
                .AddOpenAITextToImage("", modelId: "dall-e-3",
                    httpClient: client)
                .AddOpenAITextEmbeddingGeneration("text-embedding-ada-002", "",
                    httpClient: client)
                .Build();

            var pluginsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "plugins","Tools");

            kernel.ImportPluginFromPromptDirectory(pluginsDirectory, "Tools");

            return kernel;
        });
    }
}