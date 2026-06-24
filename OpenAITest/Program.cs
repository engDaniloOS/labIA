using Workflow.Workflows;

internal class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Digite a opção, sendo 1 para o workflow sequencial e 2 para o workflow com camada de decisão ");
        var opcao = Console.ReadLine();

        if (opcao.Equals("sair"))
            return;
        else if (opcao.Equals("1"))
            await DietaWorkflow.Execute();
        else if (opcao.Equals("2"))
        {
            await NutricionistaWorkflow.Execute();
        }
    }
}