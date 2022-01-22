using MassTransit;
using PublisherA.Controllers;
using System.Text.Json;

namespace PublisherA.Controllers
{
    public class RegisterViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}


namespace Consumer.Services
{
    public class RegisterConsumer : IConsumer<RegisterViewModel>
    {
        private readonly ILogger<RegisterConsumer> _logger;

        public RegisterConsumer(ILogger<RegisterConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<RegisterViewModel> context)
        {
            var message = $"Consume register item {JsonSerializer.Serialize(context.Message)}";
            _logger.LogInformation(message);

            await Task.CompletedTask;
        }
    }

    public class RabbitMqBackgroundService : IHostedService
    {
        private readonly IBusControl _busControl;
        private readonly ILogger<RabbitMqBackgroundService> _logger;

        public RabbitMqBackgroundService(IBusControl busControl
            , ILogger<RabbitMqBackgroundService> logger)
        {
            _busControl = busControl;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Background bus service started");

            await _busControl.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogError("Background bus service stop!");

            await _busControl.StopAsync(cancellationToken);
        }
    }
}
