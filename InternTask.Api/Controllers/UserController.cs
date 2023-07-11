using InternTask.Api.Models;
using InternTask.Service.DTOs.Users;
using InternTask.Service.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InternTask.Api.Controllers;
public class UserController : BaseController
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(UserCreationDto dto)
        => Ok(new Response
        {
            Code = 200,
            Error = "Success",
            Data = await this.userService.AddAsync(dto)
        });


    [HttpPut]
    public async Task<IActionResult> ModifyAsync(UserUpdateDto dto)
        => Ok(new Response
        {
            Code = 200,
            Error = "Success",
            Data = await this.userService.ModifyAsync(dto)
        });

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(long id)
       => Ok(new Response
       {
           Code = 200,
           Error = "Success",
           Data = await this.userService.RemoveAsync(id)
       });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(long id)
       => Ok(new Response
       {
           Code = 200,
           Error = "Success",
           Data = await this.userService.RetrieveByIdAsync(id)
       });

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
       => Ok(new Response
       {
           Code = 200,
           Error = "Success",
           Data = await this.userService.RetrieveAllAsync()
       });
}
