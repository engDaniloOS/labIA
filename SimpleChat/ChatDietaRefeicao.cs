using Comum;
using Comum.Models;
using Microsoft.Extensions.AI;

namespace SimpleChat
{
    public class ChatDietaRefeicao
    {
        public static async Task<List<DietaRefeicao>> Execute(DietaMacro dietaMacro)
        {
            IEnumerable<ChatMessage> messages =
                [
                    BuildUserMessage(dietaMacro),
                    BuildSystemMessage()
                ];

            var client = GeminiChat.BuildClient();
            var chatOptions = GeminiChat.GetDefaultChatOptions();

            ChatResponse<List<DietaRefeicao>> refeicoesResponse = await client.GetResponseAsync<List<DietaRefeicao>>(messages, chatOptions);
            return refeicoesResponse.Result;
        }

        private static ChatMessage BuildUserMessage(DietaMacro dietaMacro)
        {
            var promptTemplate = 
                $@"Com base nos macros do usuário, monte os pratos porcionados das principais refeições (café da manha, almoço e janta). 
                Respeite os números dos macros, principalmente das calorias. Seguem macros: {dietaMacro}";

            return new ChatMessage { Role = ChatRole.User, Contents = [new TextContent(promptTemplate)] };
        }

        private static ChatMessage BuildSystemMessage()
        {
            var contentSystem =
                    "Você é um nutricionista esportista muito experiênte." +
                    "E tem muita clareza sobre como montar dietas saudáveis, equilibradas, e principalmente que sejam gostosas e fácil de manter." +
                    "E sempre respeita o máximo possível os macronutrientes e calorias necessárias para o paciente, de acordo com os dados fornecidos pelo mesmo";

            return new ChatMessage { Role = ChatRole.System, Contents = [new TextContent(contentSystem)] };
        }
    }
}
