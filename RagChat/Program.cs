using RagChat;

var pastaConhecimento = Path.Combine(AppContext.BaseDirectory, "KnowledgeBase");
var collection = await RagIndex.BuildFromFolderAsync(pastaConhecimento);

Console.WriteLine("Base de conhecimento indexada. Faça perguntas sobre nutrição (digite 'sair' para encerrar).");

while (true)
{
    Console.Write("\n> ");
    var pergunta = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(pergunta) || pergunta.Equals("sair", StringComparison.OrdinalIgnoreCase))
        break;

    var resposta = await RagQuery.Execute(collection, pergunta);
    Console.WriteLine(resposta);
}
