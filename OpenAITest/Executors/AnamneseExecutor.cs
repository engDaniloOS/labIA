using Comum.Models;
using Microsoft.Agents.AI;
using Microsoft.Agents.AI.Workflows;
using Microsoft.Extensions.AI;

namespace Workflow.Executors
{
    public class AnamneseExecutor(AIAgent _agent) : Executor("AnamneseExecutor")
    {
        [MessageHandler]
        public async ValueTask<DietaMacro> HandleAsync(ChatMessage message, IWorkflowContext context, CancellationToken cancellationToken = default)
        {
            var response = await _agent.RunAsync<DietaMacro>(message, cancellationToken: cancellationToken);

            var resultado = response.Result;
            Console.WriteLine(resultado);

            return resultado;
        }

        protected override ProtocolBuilder ConfigureProtocol(ProtocolBuilder protocolBuilder)
        {
            protocolBuilder.ConfigureRoutes(
                builder => builder.AddHandler<ChatMessage, DietaMacro>(HandleAsync));

            return protocolBuilder;
        }
    }
}
