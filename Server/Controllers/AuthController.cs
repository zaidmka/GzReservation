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

    [HttpPost("change-password"), Authorize]
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

    [HttpPost("registerall")]

    public async Task<ActionResult<ServiceResponse<List<int>>>> Register([FromBody] UserRegister[] requests)
    {
        var responses = new List<ServiceResponse<int>>();

        foreach (var request in requests)
        {
            var response = await _authService.Register(new UserEntity
            {
                Email = request.Email,
                fullname = request.fullname,
                EntityId = request.EntityId
            },
            request.Password);

            responses.Add(response);
        }

        // Check if all registrations were successful
        if (responses.Any(r => !r.Success))
        {
            return BadRequest(responses);
        }

        return Ok(responses);
    }
    [HttpPost("firstLogin"), Authorize]
    public async Task<ActionResult<ServiceResponse<bool>>> FirstLogin(UserFirstLogin user)
    {
        // Check if the user is authenticated
        if (!User.Identity.IsAuthenticated)
        {
            return Unauthorized(); // Return 401 Unauthorized if the user is not authenticated
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var response = await _authService.FirstLogin(int.Parse(userId), user.Password, user.full_name);

        if (!response.Success)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpGet("{userEmail}"),Authorize]
    public async Task<ActionResult<ServiceResponse<UserEntity>>>GetUserInfo(string userEmail)
    {
        var result = await _authService.GetUserByEmail(userEmail);
        return Ok(result);
    }
	[HttpPost("changeUserEntityName")]
	[Authorize(Policy = "AdminRolePolicy")] // Only the register action is under the AdminRolePolicy
	public async Task<ActionResult<ServiceResponse<UserEntityChangeDetails>>> ChangeUserEntityName(UserEntityChangeDetails userEntityChangeName)
	{
		var result = await _authService.ChangeUserName(userEntityChangeName);
		if (result.Success)
		{
			return Ok(result); // Return 200 OK for successful operation
		}
		else
		{
			return BadRequest(result); // Return 400 Bad Request for failed operation
		}
	}

	[HttpPost("AdminChangePassword")]
	[Authorize(Policy = "AdminRolePolicy")] // Only the register action is under the AdminRolePolicy
	public async Task<ActionResult<ServiceResponse<bool>>> ChangePasswordAdmin(UserLogin user)
	{
		
		var response = await _authService.ChangePasswordAdmin(user);

		if (!response.Success)
		{
			return BadRequest(response);
		}

		return Ok(response);
	}
}
