using Domains.Domains;
using LucasRabbit.Bus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LucasRabbit.Functions
{
    public class VendaFunction
    {
        private readonly ILogger<VendaFunction> _logger;
        private IBusService _rabbitService;

        public VendaFunction(ILogger<VendaFunction> logger, IBusService busService)
        {
            _logger = logger;
            _rabbitService = busService;
        }

        [Function("food")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function,"post")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var pedido = JsonConvert.DeserializeObject<Pedido>(requestBody);

            _rabbitService.Publish("pedido.criado", pedido);
            _logger.LogInformation("Comunicação enviada para fila.");

            return new OkObjectResult(JsonConvert.SerializeObject(new
            {
                success = true,
                message = "Seu pedido foi recebido e está sendo processado!",
                pedido = new
                {
                    pedidoId = pedido.PedidoId,
                    cliente = pedido.Cliente,
                    restaurante = pedido.Restaurante,
                    itens = pedido.Itens.Select(i => new { nome = i.Nome, quantidade = i.Quantidade })
                }
            }));
        }
    }
}
