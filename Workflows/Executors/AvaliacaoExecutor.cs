using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;
using Workflow.Models;

namespace Workflow.Executors
{
    public class AvaliacaoExecutor(AIAgent _agent) : Executor("AvaliacaoExecutor")
    {
        public async ValueTask<string> HandleAsync(Roteamento roteamento, IWorkflowContext context, CancellationToken cancellationToken = default)
        {
            var response = await _agent.RunAsync<string>(
                new ChatMessage(ChatRole.User, [new TextContent(roteamento.ToString())]),
                cancellationToken: cancellationToken);

            var resultado = response.Result;

            Console.WriteLine($"Resultado da avaliação: {resultado}");

            return resultado;
        }

        protected override ProtocolBuilder ConfigureProtocol(ProtocolBuilder protocolBuilder)
        {
            protocolBuilder.ConfigureRoutes(
                builder => builder.AddHandler<Roteamento, string>(HandleAsync));

            return protocolBuilder;
        }
    }
}
