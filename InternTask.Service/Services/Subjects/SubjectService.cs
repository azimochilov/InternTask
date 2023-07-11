using AutoMapper;
using InternTask.Data.IRepositories;
using InternTask.Domain.Entities;
using InternTask.Service.DTOs.Subjects;
using InternTask.Service.DTOs.Users;
using InternTask.Service.Exceptions;
using InternTask.Service.Helpers;
using InternTask.Service.Interfaces.Subjects;
using Microsoft.EntityFrameworkCore;

namespace InternTask.Service.Services.Subjects;
public class SubjectService : ISubjectService
{
    private readonly IRepository<Subject> subjectRepository;
    private readonly IRepository<UserSubject> userSubjectsRepository;
    private readonly IRepository<Grades> gradesRepository;
    private readonly IMapper mapper;
    public SubjectService(IRepository<Subject> subjectRepository, IMapper mapper, IRepository<UserSubject> userSubjectsRepository, IRepository<Grades> gradesRepository)
    {
        this.subjectRepository = subjectRepository;
        this.mapper = mapper;
        this.userSubjectsRepository = userSubjectsRepository;
        this.gradesRepository = gradesRepository;
    }

    public async ValueTask<SubjectResultDto> AddAsync(SubjectCreationDto dto)
    {
        var exist = await this.subjectRepository.SelectAsync(s => s.Name.Equals(dto.Name));
        if (exist is not null && exist.IsDeleted)
        {
            var mapDto = this.mapper.Map(dto, exist);
            mapDto.UpdatedBy = HttpContextHelper.UserId;
            mapDto.UpdatedAt = DateTime.UtcNow;
            exist.IsDeleted = false;
            await this.subjectRepository.SaveAsync();
            return this.mapper.Map<SubjectResultDto>(exist);
        }

        if (exist is not null)
            throw new InternTaskException(409, "Already exist");

        var mappedDto = this.mapper.Map<Subject>(dto);
        var insertedSubject = await this.subjectRepository.InsertAsync(mappedDto);
        await this.subjectRepository.SaveAsync();

        return this.mapper.Map<SubjectResultDto>(insertedSubject);
    }

    public async ValueTask<SubjectResultDto> ModifyAsync(SubjectUpdateDto dto)
    {
        var exist = await this.subjectRepository.SelectAsync(u => u.Id.Equals(dto.Id));
        if (exist is null)
            throw new InternTaskException(404, "Not found");

        if (exist.IsDeleted)
        {
            var updatedDto = this.mapper.Map(dto, exist);
            updatedDto.IsDeleted = false;
            updatedDto.UpdatedAt = DateTime.UtcNow;
            updatedDto.UpdatedBy = HttpContextHelper.UserId;
            await this.subjectRepository.SaveAsync();

            return this.mapper.Map<SubjectResultDto>(updatedDto);
        }

        var mappedDto = this.mapper.Map(dto, exist);
        mappedDto.UpdatedBy = HttpContextHelper.UserId;
        mappedDto.UpdatedAt = DateTime.UtcNow;

        await this.subjectRepository.SaveAsync();

        return this.mapper.Map<SubjectResultDto>(mappedDto);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var exist = await this.subjectRepository.SelectAsync(u => u.Id.Equals(id));
        if (exist is null)
            throw new InternTaskException(404, "Not found");

        if (exist.IsDeleted)
            throw new InternTaskException(409, "Already deleted");

        await this.subjectRepository.DeleteAsync(u => u.Id.Equals(id));
        await this.subjectRepository.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<SubjectResultDto>> RetrieveAllAsync()
    {
        var subjects = await this.subjectRepository
            .SelectAll()
                .Where(u => u.IsDeleted == false)                
                .ToListAsync();

        return this.mapper.Map<IEnumerable<SubjectResultDto>>(subjects);
    }

    public async ValueTask<SubjectResultDto> RetrieveByIdAsync(long id)
    {
        var subject = await this.subjectRepository.SelectAsync(u => u.Id.Equals(id));
        if (subject is null)
            throw new InternTaskException(404, "Not found");

        if (subject.IsDeleted)
            throw new InternTaskException(400, "Subject is deleted");

        return this.mapper.Map<SubjectResultDto>(subject);
    }

   
}
