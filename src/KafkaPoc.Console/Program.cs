using KafkaPoc.Console.Application.Events;
using KafkaPoc.Console.Application.Events.EventHandlers;
using KafkaPoc.Console.Application.Events.MessageHandler;
using KafkaPoc.Console.Config.Kafka;
using KafkaPoc.Console.Services;
using KafkaPoc.Console.Services.DataContracts;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var host = CreateHostBuilder(args).Build();

// Get configuration and add topic
var messageHandlers = host.Services.GetServices<IKafkaMessageHandler>();
foreach (var handler in messageHandlers)
{
    Task.Run(() => KafkaConsumerHostExtensions.StartConsumer(host.Services, handler.Topic));
}

await host.RunAsync();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, configuration) =>
        {
            configuration
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");
        })
        .ConfigureServices((hostContext, services) =>
        {
            services.Configure<KafkaConfig>(hostContext.Configuration.GetSection("Kafka"));
            services.AddSingleton(cfg => cfg.GetRequiredService<IOptions<KafkaConfig>>().Value);
            services.AddMediatR(typeof(Program).Assembly);
            services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
            services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();
            services.AddTransient<IKafkaMessageHandler, ProductCreatedEventHandler>();

        });