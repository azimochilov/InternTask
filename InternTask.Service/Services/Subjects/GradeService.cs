using AutoMapper;
using InternTask.Data.IRepositories;
using InternTask.Domain.Entities;
using InternTask.Service.DTOs.Subjects;
using InternTask.Service.Exceptions;
using InternTask.Service.Helpers;
using InternTask.Service.Interfaces.Subjects;
using Microsoft.EntityFrameworkCore;

namespace InternTask.Service.Services.Subjects;
public class GradeService : IGradeService
{
    private readonly IRepository<Grades> gradesRepository;
    private readonly IMapper mapper;

    public GradeService(IRepository<Grades> gradesRepository, IMapper mapper)
    {
        this.gradesRepository = gradesRepository;
        this.mapper = mapper;
    }

    public async ValueTask<GradesResultDto> AddAsync(GradesCreationDto dto)
    {
        var exist = await this.gradesRepository
            .SelectAsync(g => g.UserId.Equals(dto.UserId) && g.SubjectId.Equals(dto.SubjectId));

        if (exist is not null && exist.IsDeleted)
        {
            var mapDto = this.mapper.Map(dto, exist);
            mapDto.IsDeleted = false;
            mapDto.UpdatedAt = DateTime.UtcNow;
            mapDto.UpdatedBy = HttpContextHelper.UserId;
            await this.gradesRepository.SaveAsync();
            return this.mapper.Map<GradesResultDto>(exist);
        }

        if (exist is not null)
            throw new InternTaskException(409, "Already exist grade, for this subject");

        var mappedDto = mapper.Map<Grades>(dto);
        var insertedGrades = await this.gradesRepository.InsertAsync(mappedDto);
        await this.gradesRepository.SaveAsync();

        return this.mapper.Map<GradesResultDto>(insertedGrades);
    }

    public async ValueTask<GradesResultDto> ModifyAsync(GradesUpdateDto dto)
    {
        var exist = await this.gradesRepository
            .SelectAsync(g => g.UserId.Equals(dto.UserId) && g.SubjectId.Equals(dto.SubjectId), new string[] { "User", "Subject" });

        if (exist is null)
            throw new InternTaskException(404, "Not found");


        if (exist.IsDeleted)
        {
            exist.IsDeleted = false;
            var res = this.mapper.Map(dto, exist);
            res.UpdatedBy = HttpContextHelper.UserId;
            res.UpdatedAt = DateTime.UtcNow;
            await this.gradesRepository.SaveAsync();
            return this.mapper.Map<GradesResultDto>(exist);
        }
        var mappedDto = this.mapper.Map(dto, exist);
        mappedDto.UpdatedBy = HttpContextHelper.UserId;
        mappedDto.UpdatedAt = DateTime.UtcNow;
        await this.gradesRepository.SaveAsync();

        return mapper.Map<GradesResultDto>(mappedDto);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var exist = await this.gradesRepository.SelectAsync(g => g.Id.Equals(id) && !g.IsDeleted);
        if (exist is null)
            throw new InternTaskException(404, "Not found");

        if (exist.IsDeleted)
            throw new InternTaskException(409, "Grades is already removed");

        await this.gradesRepository.DeleteAsync(g => g.Id.Equals(id));
        await this.gradesRepository.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<GradesResultDto>> RetrieveAllAsync()
    {
        var grades = await this.gradesRepository
            .SelectAll(g => !g.IsDeleted, new string[] { "User", "Subject" })            
            .ToListAsync();

        return this.mapper.Map<IEnumerable<GradesResultDto>>(grades);

    }
    
    public async ValueTask<GradesResultDto> RetrieveByIdAsync(long id)
    {
        var grade = await this.gradesRepository.SelectAsync(g => g.Id.Equals(id) && !g.IsDeleted, new string[] { "User", "Subject" });
        if (grade is null)
            throw new InternTaskException(404, "Not found");

        return this.mapper.Map<GradesResultDto>(grade);
    }
}
