using System;
using Domains.Domains;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace RabbitFood.Functions.Functions
{
    public class PedidoCriadoFunction
    {
        private readonly ILogger _logger;

        public PedidoCriadoFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PedidoCriadoFunction>();
        }

        [Function("PedidoCriadoFunction")]
        public void Run([RabbitMQTrigger("pedido.criado", ConnectionStringSetting = "RabbitMQConnectionString")] string myQueueItem)
        {
            var pedido = JsonConvert.DeserializeObject<Pedido>(myQueueItem);

            _logger.LogInformation($"Pedido Será processado, e encaminhado para fila seguinte com sucesso: Cliente = {pedido.Cliente}");
        }
    }
}
