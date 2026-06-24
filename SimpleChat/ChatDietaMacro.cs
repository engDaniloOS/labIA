using Comum;
using Comum.Models;
using Microsoft.Extensions.AI;

namespace SimpleChat
{
    public class ChatDietaMacro
    {
        public static async Task<DietaMacro> Execute(Usuario usuario)
        {
            IEnumerable<ChatMessage> messages =
                [
                    BuildUserMessage(usuario),
                    BuildSystemMessage()
                ];

            var client = GeminiChat.BuildClient();
            var chatOptions = GeminiChat.GetDefaultChatOptions();

            ChatResponse<DietaMacro> dietaMacroResponse = await client.GetResponseAsync<DietaMacro>(messages, chatOptions);
            return dietaMacroResponse.Result;
        }

        private static ChatMessage BuildUserMessage(Usuario usuario)
        {
            var promptTemplate = usuario.ToString();

            return new ChatMessage { Role = ChatRole.User, Contents = [new TextContent(promptTemplate)] };
        }

        private static ChatMessage BuildSystemMessage()
        {
            var contentSystem =
                    "Você é um nutricionista esportista muito experiênte." +
                    "Ao receber dados do cliente, responderá sempre com os macros necessários para que ele alcance o seu objetivo.";

            return new ChatMessage { Role = ChatRole.System, Contents = [new TextContent(contentSystem)] };
        }
    }
}
