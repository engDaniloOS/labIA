using GeminiDotnet;
using GeminiDotnet.Extensions.AI;
using Microsoft.Extensions.AI;

namespace Comum
{
    public class GeminiChat
    {
        public const int EmbeddingDimensions = 768;

        private static GeminiChatClient? client;
        private static IEmbeddingGenerator<string, Embedding<float>>? embeddingGenerator;

        public static GeminiChatClient BuildClient()
        {
            if (client is not null)
                return client;

            var options = new GeminiClientOptions { ApiKey = GetApiKey(), ModelId = "gemini-3.5-flash" };

            client = new GeminiChatClient(options);

            //client.AsBuilder().UseDistributedCache().UseFunctionInvocation().Build();

            return client;
        }

        public static IEmbeddingGenerator<string, Embedding<float>> BuildEmbeddingGenerator()
        {
            if (embeddingGenerator is not null)
                return embeddingGenerator;

            var options = new GeminiClientOptions
            {
                ApiKey = GetApiKey(),
                ModelId = "gemini-embedding-001",
                DefaultEmbeddingDimensions = EmbeddingDimensions
            };

            return (IEmbeddingGenerator<string, Embedding<float>>) new GeminiClient(options).AsEmbeddingGenerator();
        }

        private static string GetApiKey() => Environment.GetEnvironmentVariable("GEMINI_API_KEY") ?? "";

        public static ChatOptions GetDefaultChatOptions(bool jsonResponse = true)
        {
            return new ChatOptions
            {
                MaxOutputTokens = 4096,
                Temperature = 0.5f, //0.5 ~ 0.7 para apps comerciais
                ResponseFormat = jsonResponse ? ChatResponseFormat.Json : null
            };
        }
    }
}
