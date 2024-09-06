using System.Collections.Concurrent;
using Microsoft.KernelMemory;
using Microsoft.KernelMemory.Configuration;
using Microsoft.SemanticKernel;

#pragma warning disable SKEXP0010

namespace Raccoon.Tools;

public class KernelFactory
{
    private static readonly ConcurrentDictionary<string, Kernel> Kernels = new();

    private static readonly ConcurrentDictionary<string, MemoryServerless> MemoryServerless = new();

    public static MemoryServerless CreateMemoryServerless(string name = "default")
    {
        return MemoryServerless.GetOrAdd(name, key =>
        {
            var client = new HttpClient(new OpenAiHandler("https://api.token-ai.cn/"));

            return new KernelMemoryBuilder()
                .WithCustomTextPartitioningOptions(new TextPartitioningOptions
                {
                    MaxTokensPerLine = 100,
                    MaxTokensPerParagraph = 100,
                    OverlappingTokens = 20
                })
                .WithOpenAITextGeneration(new OpenAIConfig
                {
                    APIKey = "sk-",
                    TextModel = "gpt-4o-min"
                }, null, client)
                .WithOpenAITextEmbeddingGeneration(new OpenAIConfig
                {
                    // 如果 EmbeddingToken 为空，则使用 ChatToken
                    APIKey = "sk-",
                    EmbeddingModel = "text-embedding-ada-002"
                }, null, false, client)
                .Build<MemoryServerless>();
        });
    }

    public static Kernel Create(string model, string embeddingModel = "text-embedding-ada-002", string name = "default")
    {
        return Kernels.GetOrAdd(name + model + embeddingModel, key =>
        {
            var client = new HttpClient(new OpenAiHandler("https://api.token-ai.cn/"));

            var kernel = Kernel.CreateBuilder()
                .AddOpenAIChatCompletion(model, "sk-", httpClient: client)
                .AddOpenAITextToImage("sk-", modelId: "dall-e-3",
                    httpClient: client)
                .AddOpenAITextEmbeddingGeneration(embeddingModel, "sk-",
                    httpClient: client)
                .Build();

            var pluginsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "plugins", "Tools");

            kernel.ImportPluginFromPromptDirectory(pluginsDirectory, "Tools");

            return kernel;
        });
    }
}