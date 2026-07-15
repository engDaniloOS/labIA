using Comum.Models;
using MemoryChat;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var usuario = Usuario.GetUsuarioTeste();

        var dietaMacro =
            await ChatWithMemory.ChatWithHistoryAsync<DietaMacro>(
                "123",
                "Olá, gostaria de uma dieta macro para um usuário com as seguintes características: " + usuario,
                "Você é um nutricionista especialista em dietas e refeições.");

        Console.WriteLine(dietaMacro);

        var dietaRefeicoes = await ChatWithMemory.ChatWithHistoryAsync<List<DietaRefeicao>>(
            "123",
            $"Com base na dieta macro, gostaria de uma lista de refeições para compor a dieta (café da manha, almoço e janta). Macros: {dietaMacro}");

        if (dietaRefeicoes is not null)
        {
            foreach (var refeicao in dietaRefeicoes)
                Console.WriteLine(refeicao);
        }

        Console.WriteLine("Alguma dúvida?");
        var duvida = Console.ReadLine();

        var respostaDuvida = await ChatWithMemory.ChatWithHistoryAsync<string>(
            "123",
            $"Tenho a seguinte dúvida: {duvida}",
            jsonResponse: false);

        Console.WriteLine(respostaDuvida);
    }
}