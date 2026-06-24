using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;
using Workflow.Models;

namespace Workflow.Executors
{
    public class RoteadorExecutor(AIAgent _agent) : Executor("Roteador")
    {
        [SendsMessage(typeof(Roteamento))]
        public async ValueTask<Roteamento> HandleAsync(ChatMessage message, IWorkflowContext context, CancellationToken cancellationToken = default)
        {
            var response = await _agent.RunAsync<Roteamento>(message, cancellationToken: cancellationToken);

            var resultado = response.Result;

            Console.WriteLine($"Resultado do roteador: {resultado}");

            return resultado;
        }

        protected override ProtocolBuilder ConfigureProtocol(ProtocolBuilder protocolBuilder)
        {
            protocolBuilder.ConfigureRoutes(
                builder => builder.AddHandler<ChatMessage, Roteamento>(HandleAsync));

            return protocolBuilder;
        }


    }
}
