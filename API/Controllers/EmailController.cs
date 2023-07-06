using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Service.Requests;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _service;

        public EmailController(IEmailService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SendEmailAsync([FromForm] EmailRequest request)
        {
            try
            {
                await _service.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
