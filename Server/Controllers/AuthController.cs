using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    [Authorize(Policy = "AdminRolePolicy")] // Only the register action is under the AdminRolePolicy
    public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegister request)
    {
        var response = await _authService.Register(new UserEntity
        {
            Email = request.Email,
            fullname = request.fullname,
            EntityId = request.EntityId
            
        },
        request.Password);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
    {
        var response = await _authService.Login(request.Email, request.Password);
        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("change-password"),Authorize]
    public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword([FromBody] string newPassword)
    {
        // Check if the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized(); // Return 401 Unauthorized if the user is not authenticated
       }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await _authService.ChangePassword(int.Parse(userId), newPassword);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
