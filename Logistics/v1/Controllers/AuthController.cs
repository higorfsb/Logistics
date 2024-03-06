using Logistics.Application.v1.Controllers.Base;
using Logistics.Domain.Dto.User;
using Logistics.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Logistics.Application.v1.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/auth/")]
    public class AuthController : MainController
    {
        private readonly IAuthService _authServices;

        public AuthController(IAuthService authServices)
        {
            _authServices = authServices;
        }

        [HttpGet("login")]
        [SwaggerOperation("Authenticate user")]
        [SwaggerResponse(StatusCodes.Status200OK, "", typeof(LoginResponse))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found", typeof(string))]
        public async Task<IActionResult> Login(string userName, string password)
        {
            return Ok(await _authServices.LoginAsync(userName, password));
        }
    }
}
