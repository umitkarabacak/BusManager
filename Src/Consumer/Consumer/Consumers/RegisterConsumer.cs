using Events;
using MassTransit;
using System.Text.Json;

namespace Consumer.Consumers
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
}
