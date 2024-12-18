using mangaEnjoy.dto;
using mangaEnjoy.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace mangaEnjoy.Controllers;

public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }


    [HttpGet("{userId}")]
    public async Task<ActionResult<UserResponseDto>> GetUserProfile(Guid userId)
    {
        var userResponseDto = await userService.GetUserProfile(userId);
        return userResponseDto != null ? Ok(userResponseDto) : NotFound();
    }
}