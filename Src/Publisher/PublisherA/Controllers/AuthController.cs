using Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace PublisherA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IBus _bus;

        public AuthController(ILogger<AuthController> logger
            , IBus bus)
        {
            _logger = logger;
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = $"Error serialized item type is: {typeof(RegisterViewModel)} and current item: {JsonSerializer.Serialize(model)}";
                _logger.LogError(errorMessage);

                return BadRequest(ModelState);
            }

            await _bus.Publish(model);

            return Ok();
        }
    }
}