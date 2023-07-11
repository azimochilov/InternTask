using InternTask.Api.Models;
using InternTask.Service.DTOs.Subjects;
using InternTask.Service.Interfaces.Subjects;
using Microsoft.AspNetCore.Mvc;

namespace InternTask.Api.Controllers;
public class SubjectController : BaseController
{
    private readonly ISubjectService subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        this.subjectService = subjectService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(SubjectCreationDto dto)
        => Ok(new Response
        {
            Code = 200,
            Error = "Success",
            Data = await this.subjectService.AddAsync(dto)
        });


    [HttpPut]
    public async Task<IActionResult> ModifyAsync(SubjectUpdateDto dto)
        => Ok(new Response
        {
            Code = 200,
            Error = "Success",
            Data = await this.subjectService.ModifyAsync(dto)
        });

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(long id)
       => Ok(new Response
       {
           Code = 200,
           Error = "Success",
           Data = await this.subjectService.RemoveAsync(id)
       });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(long id)
       => Ok(new Response
       {
           Code = 200,
           Error = "Success",
           Data = await this.subjectService.RetrieveByIdAsync(id)
       });

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
       => Ok(new Response
       {
           Code = 200,
           Error = "Success",
           Data = await this.subjectService.RetrieveAllAsync()
       });

    
}
