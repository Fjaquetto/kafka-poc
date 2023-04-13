using Kafka.Bus.Config;
using Kafka.Bus.Handlers;
using Kafka.Bus.Services;
using Kafka.Bus.Services.DataContracts;
using KafkaPoc.Console.Application.Events.EventHandlers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var host = CreateHostBuilder(args).Build();

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
            services.AddKafkaLibrary(hostContext.Configuration);
            services.Configure<KafkaConfig>(hostContext.Configuration.GetSection("Kafka"));
            services.AddSingleton(cfg => cfg.GetRequiredService<IOptions<KafkaConfig>>().Value);
            services.AddMediatR(typeof(Program).Assembly);
            services.AddTransient<IKafkaMessageHandler, ProductCreatedEventHandler>();
        });