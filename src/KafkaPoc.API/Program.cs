using KafkaPoc.API.Application.Events.MessageHandler;
using KafkaPoc.API.Config.Kafka;
using KafkaPoc.API.Services;
using KafkaPoc.API.Services.DataContracts;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add required services for Kafka and MediatR
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
builder.Services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Start the Kafka consumers for the topics
var messageHandlers = app.Services.GetServices<IKafkaMessageHandler>();
foreach (var handler in messageHandlers)
{
    Task.Run(() => KafkaConsumerHostExtensions.StartConsumer(app.Services, handler.Topic));
}

await app.RunAsync();
