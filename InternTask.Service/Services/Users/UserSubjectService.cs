using AutoMapper;
using InternTask.Data.IRepositories;
using InternTask.Domain.Entities;
using InternTask.Service.DTOs.Users;
using InternTask.Service.Exceptions;
using InternTask.Service.Helpers;
using InternTask.Service.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace InternTask.Service.Services.Users;
public class UserSubjectService : IUserSubjectService
{
    private readonly IRepository<UserSubject> userSubjectRepository;
    private readonly IMapper mapper;
    public UserSubjectService(IRepository<UserSubject> userSubjectRepository, IMapper mapper)
    {
        this.userSubjectRepository = userSubjectRepository;
        this.mapper = mapper;
    }

    public async ValueTask<UserSubjectResultDto> AddAsync(UserSubjectCreationDto dto)
    {
        var exist = await this.userSubjectRepository
            .SelectAsync(u => u.StudentId.Equals(dto.StudentId) && u.SubjectId.Equals(dto.SubjectId));

        if (exist is not null && exist.IsDeleted)
        {
            var mappedDto = this.mapper.Map(dto, exist);
            mappedDto.UpdatedBy = HttpContextHelper.UserId;
            mappedDto.UpdatedAt = DateTime.UtcNow;
            await this.userSubjectRepository.SaveAsync();

            return this.mapper.Map<UserSubjectResultDto>(mappedDto);
        }

        if (exist is not null)
            throw new InternTaskException(409, "Already exist");

        var mapDto = this.mapper.Map<UserSubject>(dto);
        var insertedDto = await this.userSubjectRepository.InsertAsync(mapDto);
        await this.userSubjectRepository.SaveAsync();

        return this.mapper.Map<UserSubjectResultDto>(insertedDto);
    }

    public async ValueTask<UserSubjectResultDto> ModifyAsync(UserSubjectUpdateDto dto)
    {
        var exist = await this.userSubjectRepository
            .SelectAsync(u => u.Id.Equals(dto.Id));

        if (exist is not null && exist.IsDeleted)
        {
            var mappedDto = this.mapper.Map(dto, exist);
            mappedDto.UpdatedBy = HttpContextHelper.UserId;
            mappedDto.UpdatedAt = DateTime.UtcNow;
            mappedDto.IsDeleted = false;
            await this.userSubjectRepository.SaveAsync();

            return this.mapper.Map<UserSubjectResultDto>(mappedDto);
        }

        if (exist is null)
            throw new InternTaskException(404, "Not found");

        var updatedDto = this.mapper.Map(dto, exist);
        updatedDto.UpdatedAt = DateTime.UtcNow;
        updatedDto.UpdatedBy = HttpContextHelper.UserId;
        await this.userSubjectRepository.SaveAsync();

        return this.mapper.Map<UserSubjectResultDto>(updatedDto);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var exist = await this.userSubjectRepository.SelectAsync(g => g.Id.Equals(id) && !g.IsDeleted);
        if (exist is null)
            throw new InternTaskException(404, "Not found");

        if (exist.IsDeleted)
            throw new InternTaskException(409, "Grades is already removed");

        await this.userSubjectRepository.DeleteAsync(g => g.Id.Equals(id));
        await this.userSubjectRepository.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<UserSubjectResultDto>> RetrieveAllAsync()
    {
        var userSubjects = await this.userSubjectRepository
            .SelectAll(g => !g.IsDeleted)
            .ToListAsync();

        return this.mapper.Map<IEnumerable<UserSubjectResultDto>>(userSubjects);
    }   

    public async ValueTask<UserSubjectResultDto> RetrieveByIdAsync(long id)
    {
        var userSubject = await this.userSubjectRepository.SelectAsync(g => g.Id.Equals(id) && !g.IsDeleted);
        if (userSubject is null)
            throw new InternTaskException(404, "Not found");

        return this.mapper.Map<UserSubjectResultDto>(userSubject);
    }

}
