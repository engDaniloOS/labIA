using GeminiDotnet;
using GeminiDotnet.Extensions.AI;
using Microsoft.Extensions.AI;

namespace Comum
{
    public class GeminiChat
    {
        private static GeminiChatClient? client;

        public static GeminiChatClient BuildClient()
        {
            if (client is not null)
                return client;

            var key = Environment.GetEnvironmentVariable("OPEN_AI_KEY");
            var options = new GeminiClientOptions { ApiKey = key ?? "", ModelId = "gemini-3.5-flash" };

            client = new GeminiChatClient(options);

            //client.AsBuilder().UseDistributedCache().UseFunctionInvocation().Build();

            return client;
        }

        public static ChatOptions GetDefaultChatOptions()
        {
            return new ChatOptions
            {
                MaxOutputTokens = 4096,
                Temperature = 0.5f, //0.5 ~ 0.7 para apps comerciais
                ResponseFormat = ChatResponseFormat.Json
            };
        }
    }
}
