using InternTask.Api.Models;
using InternTask.Service.DTOs.Users;
using InternTask.Service.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace InternTask.Api.Controllers;
public class UserSubjectController : BaseController
{
    private readonly IUserSubjectService userSubjectService;

    public UserSubjectController(IUserSubjectService userSubjectService)
    {
        this.userSubjectService = userSubjectService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(UserSubjectCreationDto dto)
        => Ok(new Response
        {
            Code = 200,
            Error = "Success",
            Data = await this.userSubjectService.AddAsync(dto)
        });


    [HttpPut]
    public async Task<IActionResult> ModifyAsync(UserSubjectUpdateDto dto)
        => Ok(new Response
        {
            Code = 200,
            Error = "Success",
            Data = await this.userSubjectService.ModifyAsync(dto)
        });

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(long id)
       => Ok(new Response
       {
           Code = 200,
           Error = "Success",
           Data = await this.userSubjectService.RemoveAsync(id)
       });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(long id)
       => Ok(new Response
       {
           Code = 200,
           Error = "Success",
           Data = await this.userSubjectService.RetrieveByIdAsync(id)
       });

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
       => Ok(new Response
       {
           Code = 200,
           Error = "Success",
           Data = await this.userSubjectService.RetrieveAllAsync()
       });

    
}
