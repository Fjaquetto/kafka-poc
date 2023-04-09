using KafkaPoc.API.Services.DataContracts;
using KafkaPoc.API.Services;
using MediatR;
using KafkaPoc.API.Config.Kafka;

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

// Start the Kafka consumer for the "product_created" topic
var kafkaConfig = app.Configuration.GetSection("Kafka").Get<KafkaConfig>();
foreach (var topic in kafkaConfig.Topics)
{
    await KafkaConsumerHostExtensions.StartConsumer(app.Services, app.Environment, topic);
}

await app.RunAsync();

await app.RunAsync();