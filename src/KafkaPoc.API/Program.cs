using Kafka.Bus.Config;
using Kafka.Bus.Handlers;
using Kafka.Bus.Services;
using Kafka.Bus.Services.DataContracts;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add required services for Kafka and MediatR
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddKafkaLibrary(builder.Configuration);

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

await app.RunAsync();
