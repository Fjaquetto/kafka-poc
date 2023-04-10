using KafkaPoc.Console.Config.Kafka;
using KafkaPoc.Console.Services;
using KafkaPoc.Console.Services.DataContracts;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var host = CreateHostBuilder(args).Build();

// Start Kafka consumers
var kafkaConfig = host.Services.GetRequiredService<KafkaConfig>();
var serviceProvider = host.Services;
if (kafkaConfig.Topics != null)
    foreach (var topic in kafkaConfig.Topics)
    {
        Task.Run(() => KafkaConsumerHostExtensions.StartConsumer(serviceProvider, topic));
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