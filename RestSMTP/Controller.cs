using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSMTP.Dtos;

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

        [HttpGet("/")]
        public ActionResult<PingDto> Ping()
        {
            var assembly = Assembly.GetEntryAssembly();
            return new PingDto
            {
                Name = assembly.GetName().Name,
                Version = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion
            };
        }

        [HttpPost("/")]
        public IActionResult Send(EmailDto dto)
        {
            _logger.LogInformation($"Sending `{dto.Subject}` from `{dto.ReplyTo}`");
            return Ok(dto.ReplyTo);
        }
    }
}
