using AutoMapper;
using InternTask.Data.IRepositories;
using InternTask.Domain.Entities;
using InternTask.Service.DTOs.Subjects;
using InternTask.Service.DTOs.Users;
using InternTask.Service.Exceptions;
using InternTask.Service.Helpers;
using InternTask.Service.Interfaces.Users;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace InternTask.Service.Services.Users;
public class UserService : IUserService
{
    private readonly IRepository<User> userRepository;
    private readonly IRepository<Subject> subjectRepository;
    private readonly IRepository<Grades> gradesRepository;
    private readonly IMapper mapper;
    public UserService(IRepository<User> userRepository, IMapper mapper, IRepository<Subject> subjectRepository, IRepository<Grades> gradesRepository)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.subjectRepository = subjectRepository;
        this.gradesRepository = gradesRepository;
    }

    public async ValueTask<UserResultDto> AddAsync(UserCreationDto dto)
    {
        var exist = await this.userRepository.SelectAsync(u => u.Email.Equals(dto.Email));

        if (exist is not null && exist.IsDeleted )
        {            
            exist.IsDeleted = false;
            exist.UpdatedAt = DateTime.UtcNow;
                 
            var mappedDto = this.mapper.Map(dto, exist);

            await this.userRepository.SaveAsync();
            return this.mapper.Map<UserResultDto>(exist);
        }
        else if (exist is not null && exist.IsDeleted )
        {
            throw new InternTaskException(400, "Email or password is wrong");
        }

        else if (exist is not null)
        {
            throw new InternTaskException(409, "User already exist");
        }

        var newDto = this.mapper.Map<User>(dto);        
        
        await this.userRepository.InsertAsync(newDto);
        await this.userRepository.SaveAsync();

        return this.mapper.Map<UserResultDto>(newDto);
    }

    public async ValueTask<UserResultDto> ModifyAsync(UserUpdateDto dto)
    {
        var exist = await this.userRepository.SelectAsync(u => u.Id.Equals(dto.Id));
        if (exist is null)
            throw new InternTaskException(404, "Not found");

        if (exist.IsDeleted)
        {
            var updatedDto = this.mapper.Map(dto, exist);
            updatedDto.IsDeleted = false;
            updatedDto.UpdatedAt = DateTime.UtcNow;
            updatedDto.UpdatedBy = HttpContextHelper.UserId;
            await this.userRepository.SaveAsync();

            return this.mapper.Map<UserResultDto>(updatedDto);
        }

        var mappedDto = this.mapper.Map(dto, exist);
        mappedDto.UpdatedBy = HttpContextHelper.UserId;
        mappedDto.UpdatedAt = DateTime.UtcNow;

        await this.userRepository.SaveAsync();

        return this.mapper.Map<UserResultDto>(mappedDto);
    }

    public async ValueTask<bool> RemoveAsync(long id)
    {
        var exist = await this.userRepository.SelectAsync(u => u.Id.Equals(id));
        if (exist is null)
            throw new InternTaskException(404, "Not found");

        if (exist.IsDeleted)
            throw new InternTaskException(409, "Already deleted");

        await this.userRepository.DeleteAsync(u => u.Id.Equals(id));
        await this.userRepository.SaveAsync();

        return true;
    }

    public async ValueTask<User> RetrieveByEmailForAuthAsync(string email)
    {
        var user = await this.userRepository.SelectAsync(u => u.Email.Equals(email));
        if (user is null)
            throw new InternTaskException(404, "Email or password wrong");

        if (user.IsDeleted)
            throw new InternTaskException(400, "This accaount is deleted");

        return user;
    }

    public async ValueTask<UserResultDto> RetrieveByIdAsync(long id)
    {
        var user = await this.userRepository.SelectAsync(u => u.Id.Equals(id));
        if (user is null)
            throw new InternTaskException(404, "Not found");

        if (user.IsDeleted)
            throw new InternTaskException(400, "This accaount is deleted");

        return this.mapper.Map<UserResultDto>(user);
    }


    public async ValueTask<IEnumerable<UserResultDto>> RetrieveAllAsync(string search = null)
    {
        if (search == null)
        {
            var users2 = userRepository
            .SelectAll(u => u.IsDeleted == false);
                
                

            return this.mapper.Map<IEnumerable<UserResultDto>>(users2);
        }
        var users = await this.userRepository
        .SelectAll()
            .Where(u => u.IsDeleted == false &&
            u.FirstName.ToLower().Contains(search.ToLower()) ||
            u.LastName.ToLower().Contains(search.ToLower()) ||
            u.Email.ToLower().Contains(search.ToLower()) ||
            u.Phone.ToLower().Contains(search.ToLower()))
            .ToListAsync();

        return this.mapper.Map<IEnumerable<UserResultDto>>(users);
    }
    
}
