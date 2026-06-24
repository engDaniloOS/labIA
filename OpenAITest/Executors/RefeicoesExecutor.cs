using Comum.Models;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;

namespace Workflow.Executors
{
    [SendsMessage(typeof(List<DietaRefeicao>))]
    public class RefeicoesExecutor(AIAgent _agent) : Executor("RefeicoesExecutor")
    {
        public async ValueTask<List<DietaRefeicao>> HandleAsync(DietaMacro dietaMacro, IWorkflowContext context, CancellationToken cancellationToken = default)
        {
            var response = await _agent.RunAsync<List<DietaRefeicao>>(
                new ChatMessage(ChatRole.User, [new TextContent(dietaMacro.ToString())]), 
                cancellationToken: cancellationToken);

            var resultados = response.Result;

            foreach (var resultado in resultados)
                Console.WriteLine(resultado);

            return resultados;
        }

        protected override ProtocolBuilder ConfigureProtocol(ProtocolBuilder protocolBuilder)
        {
            protocolBuilder.ConfigureRoutes(
                builder => builder.AddHandler<DietaMacro, List<DietaRefeicao>>(HandleAsync));

            return protocolBuilder;
        }
    }
}
