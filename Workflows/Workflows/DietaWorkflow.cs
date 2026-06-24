using Comum;
using Comum.Models;
using GeminiDotnet.Extensions.AI;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;
using Workflow.Executors;

namespace Workflow.Workflows
{
    public class DietaWorkflow
    {
        public static async Task Execute()
        {
            var chatClient = GeminiChat.BuildClient();

            AIAgent calculoMacrosAgent = BuildCalculoMacroAgent(chatClient);
            AIAgent montaDietaAgent = BuildDietaAgent(chatClient);

            //Podemos usar os agents na criação do workflow
            //ou podemos criar executors que usam os agents internamente, isso é útil para adicionar regras de negócio, protocolos de comunicação, etc... e ai sim usar os executors no workflow
            //var workflow = new WorkflowBuilder(calculoMacrosAgent)
            //    .AddEdge(calculoMacrosAgent, montaDietaAgent)
            //    .Build();

            var anamneseExecutor = new AnamneseExecutor(calculoMacrosAgent);
            var refeicoesExecutor = new RefeicoesExecutor(montaDietaAgent);

            var workflow = new WorkflowBuilder(anamneseExecutor)
                .AddEdge(anamneseExecutor, refeicoesExecutor)
                .Build();

            var userMessage = new ChatMessage(ChatRole.User, [new TextContent($"Seguem meus dados {Usuario.GetUsuarioTeste()}")]);

            //Processando como Stream Async
            //StreamingRun run = await InProcessExecution.RunStreamingAsync(workflow, userMessage);
            //await run.TrySendMessageAsync(new TurnToken(emitEvents: true));

           await InProcessExecution.RunAsync(workflow, userMessage);
        }

        private static ChatClientAgent BuildDietaAgent(GeminiChatClient chatClient)
        {
            return chatClient.AsAIAgent(
                    name: "montarDieta",
                    instructions: $"Dado os macros do usuário, monte sua dieta com as principais refeições (café a manha, almoço e janta)");
        }

        private static ChatClientAgent BuildCalculoMacroAgent(GeminiChatClient chatClient)
        {
            return chatClient.AsAIAgent(
                    name: "calculoMacros",
                    instructions: $"Dado o usuário, calcule os macronutrientes de sua dieta. E informe se a dieta será de bulking ou cutting");
        }
    }
}
