using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RestSMTP
{
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly ILogger<Controller> _logger;

        public Controller(ILogger<Controller> logger)
        {
            _logger = logger;
        }

        [HttpPost("/")]
        public IActionResult Send(Dto dto)
        {
            _logger.LogInformation($"Sending `{dto.Subject}` from `{dto.From}`");

            return Ok(dto.From);
        }
    }
}
