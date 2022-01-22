using MassTransit;
using PublisherB.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MassTransit
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
    {
        //cfg.Message<RegisterViewModel>(x =>
        //{
        //    x.SetEntityName("PublisherA.Controllers:RegisterViewModel");
        //});

        //cfg.Publish<RegisterViewModel>(x =>
        //{
        //    x.BindAlternateExchangeQueue("PublisherA.Controllers:RegisterViewModel", "endpointname");
        //});
    }));
});

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
