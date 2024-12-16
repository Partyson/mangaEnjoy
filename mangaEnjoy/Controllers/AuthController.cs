using System.Security.Claims;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using mangaEnjoy.dto;
using mangaEnjoy.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mangaEnjoy.Controllers;

public class AuthController : ControllerBase
{
    private readonly IAuthService authorizationService;
    private readonly IUnitOfWork unitOfWork;

    public AuthController(IAuthService authorizationService, IUnitOfWork unitOfWork)
    {
        this.authorizationService = authorizationService;
        this.unitOfWork = unitOfWork;
    }


    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserDto user)
    {
        var token = await authorizationService.RegisterUser(user);
        await unitOfWork.SaveChangesAsync();
        Response.Cookies.Append("token", token);
        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequest userLoginRequest)
    {
        var token = await authorizationService.LoginUser(userLoginRequest.Login, userLoginRequest.Password);
        Response.Cookies.Append("token", token);
        return Ok(token);
    }
    
    [HttpGet("get-user-id")]
    public ActionResult<Guid> GetUserIdFromToken()
    {
        var userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Ok(Guid.Parse(userId));
    }
}