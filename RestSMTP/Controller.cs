using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSMTP.Dtos;

namespace RestSMTP
{
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly ILogger<Controller> _logger;
        private readonly Service _service;

        public Controller(
            ILogger<Controller> logger,
            Service service)
        {
            _logger = logger;
            _service = service;
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
        public async Task<IActionResult> Send(EmailDto dto)
        {
            try
            {
                await _service.Send(dto);
                return NoContent();
            }
            catch (ValidationException)
            {
                return BadRequest();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
