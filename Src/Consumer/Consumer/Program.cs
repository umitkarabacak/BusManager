using Consumer.Consumers;
using Consumer.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MassTransit
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddConsumer<RegisterConsumer>();
    busConfigurator.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        cfg.ReceiveEndpoint("register-consumer", receieEndpointConfigurator =>
        {
            receieEndpointConfigurator.ConfigureConsumer<RegisterConsumer>(provider);
        });
    }));
});

// Hosted services
builder.Services.AddHostedService<RabbitMqBackgroundService>();

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

app.Run();
