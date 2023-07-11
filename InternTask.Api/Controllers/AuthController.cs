using InternTask.Api.Models;
using InternTask.Service.DTOs.Users;
using InternTask.Service.Interfaces.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternTask.Api.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> AuthenticateAsync(LoginDto dto)
    {
        return Ok(new Response
        {
            Code = 200,
            Error = "Success",
            Data = await this.authService.AuthenticateAsync(dto.Email, dto.Password)
        });
    }
}
