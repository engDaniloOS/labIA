using Comum;
using Microsoft.Extensions.AI;

namespace MemoryChat
{
    public class ChatWithMemory
    {
        private static readonly Dictionary<string, List<ChatMessage>> _sessionHistories = [];

        public static async Task<T?> ChatWithHistoryAsync<T>(string sessionId, string userChat, string? systemRole = null)
        {
            List<ChatMessage> history = GetOrBuildSessionHistory(sessionId, systemRole);

            history.Add(new ChatMessage
            {
                Role = ChatRole.User,
                Contents = [new TextContent(userChat)]
            });

            var chatClient = GeminiChat.BuildClient();
            var response = await chatClient.GetResponseAsync<T>(history, GeminiChat.GetDefaultChatOptions());

            history.Add(new ChatMessage
            {
                Role = ChatRole.Assistant,
                Contents = [new TextContent(response?.Result?.ToString())]
            });

            return response is null? default : response.Result;
        }

        private static List<ChatMessage> GetOrBuildSessionHistory(string sessionId, string? systemRole)
        {
            if (!_sessionHistories.ContainsKey(sessionId))
                _sessionHistories.Add(sessionId, []);

            var history = _sessionHistories[sessionId];

            if (history.Count == 0 && systemRole is not null)
            {
                history.Add(new ChatMessage
                {
                    Role = ChatRole.System,
                    Contents = [new TextContent(systemRole)]
                });
            }

            return history;
        }
    }
}
