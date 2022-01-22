using MassTransit;

namespace Consumer.Services
{
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
            _logger.LogInformation("Consumer background bus service started");

            await _busControl.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogError("Consumer background bus service stop!");

            await _busControl.StopAsync(cancellationToken);
        }
    }
}
