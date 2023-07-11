using InternTask.Service.DTOs.Users;

namespace InternTask.Service.Interfaces.Users;
public interface IUserSubjectService
{
    ValueTask<UserSubjectResultDto> AddAsync(UserSubjectCreationDto dto);
    ValueTask<UserSubjectResultDto> ModifyAsync(UserSubjectUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<UserSubjectResultDto> RetrieveByIdAsync(long id);    
    ValueTask<IEnumerable<UserSubjectResultDto>> RetrieveAllAsync();
}
