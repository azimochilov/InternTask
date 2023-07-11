using InternTask.Api.Models;
using InternTask.Service.DTOs.Subjects;
using InternTask.Service.Interfaces.Subjects;
using Microsoft.AspNetCore.Mvc;

namespace InternTask.Api.Controllers;
public class GradeController : BaseController
{
    private readonly IGradeService gradesService;

    public GradeController(IGradeService gradesService)
    {
        this.gradesService = gradesService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(GradesCreationDto dto)
        => Ok(new Response
        {
            Code = 200,
            Error = "Success",
            Data = await this.gradesService.AddAsync(dto)
        });


    [HttpPut]
    public async Task<IActionResult> ModifyAsync(GradesUpdateDto dto)
        => Ok(new Response
        {
            Code = 200,
            Error = "Success",
            Data = await this.gradesService.ModifyAsync(dto)
        });

    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(long id)
       => Ok(new Response
       {
           Code = 200,
           Error = "Success",
           Data = await this.gradesService.RemoveAsync(id)
       });

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(long id)
       => Ok(new Response
       {
           Code = 200,
           Error = "Success",
           Data = await this.gradesService.RetrieveByIdAsync(id)
       });

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
       => Ok(new Response
       {
           Code = 200,
           Error = "Success",
           Data = await this.gradesService.RetrieveAllAsync()
       });
    
}
