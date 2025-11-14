using Business;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Email))
                    return BadRequest(new { message = "Email and password are required" });

                var user = await _userService.RegisterUserAsync(
                    request.Fullname,
                    request.Email,
                    request.Password
                );

                var response = new
                {
                    user.Id,
                    user.Fullname,
                    user.Email,
                    Message = "Registration successful"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.Email) && !string.IsNullOrWhiteSpace(request.Password))
            {
                var user = await _userService.LoginUserAsync(request.Email, request.Password);
                if (user != null)
                {
                    var response = new
                    {
                        user.Id,
                        user.Fullname,
                        user.Email,
                        Message = "Login successful"
                    };
                    return Ok(response);
                }
            }
            return BadRequest(new { message = "Invalid email or password" });
        }

        
    }


   
}