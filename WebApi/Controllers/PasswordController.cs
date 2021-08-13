using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PasswordController : ControllerBase
    {
        private IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [Authorize]
        [HttpPost("validatepassword")]
        public IActionResult ValidatePassword(ValidatePasswordRequest validatePasswordRequest)
        {
            var isValidPassword = _passwordService.ValidatePassword(validatePasswordRequest.Password);
            return Ok(isValidPassword);
        }

        [Authorize]
        [HttpGet("getvalidpassword")]
        public IActionResult GetValidPassword()
        {
            var validPassword = _passwordService.GetValidPassword();
            return Ok(validPassword);
        }
    }
}
