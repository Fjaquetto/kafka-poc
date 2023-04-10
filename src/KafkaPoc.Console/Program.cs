using KafkaPoc.Console.Application.Events;
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
var kafkaConfig = host.Services.GetRequiredService<KafkaConfig>();
kafkaConfig
    .AddTopic(nameof(ProductCreatedEvent));

// Start Kafka consumer
if (kafkaConfig.Topics != null)
    foreach (var topic in kafkaConfig?.Topics) //TODO: Here we can get all names of SubscribeEvents..
    {
        Task.Run(() => KafkaConsumerHostExtensions.StartConsumer(host.Services, topic));
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
        });