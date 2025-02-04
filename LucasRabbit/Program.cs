using EasyNetQ;
using LucasRabbit.Bus;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

var bus = RabbitHutch.CreateBus("host=localhost");

builder.Services.AddSingleton<IBusService, EasyNetQService>(
    o => new EasyNetQService(bus)
    );

builder.Build().Run();

