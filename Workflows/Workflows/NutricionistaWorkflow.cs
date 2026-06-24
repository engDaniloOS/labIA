using Comum;
using Comum.Models;
using GeminiDotnet.Extensions.AI;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;
using Workflow.Executors;
using Workflow.Models;

namespace Workflow.Workflows
{
    public class NutricionistaWorkflow
    {
        public static async Task Execute()
        {
            var pedidoUsuario = "Quero melhorar minha alimentação, o que você me recomenda?";
            //var pedidoUsuario = "Gostaria que avaliasse meus números. O que acha?";

            var chatClient = GeminiChat.BuildClient();

            AIAgent roteadorAgent = BuildRoteadorAgent(chatClient);
            AIAgent calculoMacrosAgent = BuildCalculoMacroAgent(chatClient);
            AIAgent avaliacaoAgent = BuildAvaliacaoAgent(chatClient);

            //nós
            var roteadorExecutor = new RoteadorExecutor(roteadorAgent);
            var calculoMacrosExecutor = new AnamneseExecutor(calculoMacrosAgent);
            var avaliacaoExecutor = new AvaliacaoExecutor(avaliacaoAgent);

            var workflow = new WorkflowBuilder(roteadorExecutor)
                .AddEdge(
                    roteadorExecutor, 
                    avaliacaoExecutor, 
                    condition: (Roteamento roteamento) => roteamento == Roteamento.Avaliacao)
                .AddEdge(
                    roteadorExecutor, 
                    calculoMacrosExecutor, 
                    condition: (Roteamento roteamento) => roteamento == Roteamento.Macro)
                .Build();

            var userMessage = new ChatMessage(
                ChatRole.User,
                [new TextContent($"Segue pedido do usuário: {pedidoUsuario}. Seguem dados usuário: {Usuario.GetUsuarioTeste()}")]);

            //Processando como Stream Async
            //StreamingRun run = await InProcessExecution.RunStreamingAsync(workflow, userMessage);
            //await run.TrySendMessageAsync(new TurnToken(emitEvents: true));

            await InProcessExecution.RunAsync(workflow, userMessage);
        }

        private static AIAgent BuildRoteadorAgent(GeminiChatClient chatClient)
        {
            return chatClient.AsAIAgent(
                    name: "roteador",
                    instructions: $"Dado a entrada do usuário, direcione para o agente correto, se for para calcular os macros, retorne {Roteamento.Macro}, se for para avaliar o usuário, retorne {Roteamento.Avaliacao}.");
        }

        private static AIAgent BuildAvaliacaoAgent(GeminiChatClient chatClient)
        {
            return chatClient.AsAIAgent(
                    name: "avaliacao",
                    instructions: $"Dado o usuario, faça uma analise nutricional do seu perfil, e indique os próximos passos para que possa melhorar o seu corpo");
        }

        private static AIAgent BuildCalculoMacroAgent(GeminiChatClient chatClient)
        {
            return chatClient.AsAIAgent(
                    name: "calculoMacros",
                    instructions: $"Dado o usuário, calcule os macronutrientes de sua dieta. E informe se a dieta será de bulking ou cutting");
        }
    }
}
