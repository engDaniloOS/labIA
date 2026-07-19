using Comum;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;

namespace RagChat
{
    public static class RagQuery
    {
        public static async Task<string> Execute(VectorStoreCollection<string, DocumentChunk> collection, string pergunta, int topK = 3)
        {
            var trechosRelevantes = await Recuperar(collection, pergunta, topK);

            var messages = new List<ChatMessage>
            {
                BuildSystemMessage(),
                BuildUserMessage(pergunta, trechosRelevantes)
            };

            var client = GeminiChat.BuildClient();
            var chatOptions = GeminiChat.GetDefaultChatOptions(jsonResponse: false);

            var response = await client.GetResponseAsync(messages, chatOptions);
            return response.Text;
        }

        private static async Task<List<DocumentChunk>> Recuperar(VectorStoreCollection<string, DocumentChunk> collection, string pergunta, int topK)
        {
            var resultados = new List<DocumentChunk>();

            await foreach (var resultado in collection.SearchAsync(pergunta, topK))
            {
                Console.WriteLine($"  [contexto usado - score {resultado.Score:F3}] ({resultado.Record.Fonte}) {resultado.Record.Conteudo}");
                resultados.Add(resultado.Record);
            }

            return resultados;
        }

        private static ChatMessage BuildSystemMessage()
        {
            var contentSystem =
                "Você é um nutricionista esportivo. Responda SOMENTE com base no contexto fornecido pelo usuário. " +
                "Se o contexto não tiver informação suficiente para responder, diga claramente que não sabe, não invente informações.";

            return new ChatMessage { Role = ChatRole.System, Contents = [new TextContent(contentSystem)] };
        }

        private static ChatMessage BuildUserMessage(string pergunta, List<DocumentChunk> trechosRelevantes)
        {
            var contexto = string.Join("\n\n", trechosRelevantes.Select(t => $"[{t.Fonte}] {t.Conteudo}"));

            var promptTemplate =
                $@"Contexto: {contexto}
                Pergunta: {pergunta}";

            return new ChatMessage { 
                Role = ChatRole.User, 
                Contents = [new TextContent(promptTemplate)] 
            };
        }
    }
}
