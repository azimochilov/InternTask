using InternTask.Service.DTOs.Subjects;

namespace InternTask.Service.Interfaces.Subjects;
public interface ISubjectService
{
    ValueTask<SubjectResultDto> AddAsync(SubjectCreationDto dto);
    ValueTask<SubjectResultDto> ModifyAsync(SubjectUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<SubjectResultDto> RetrieveByIdAsync(long id);
    ValueTask<IEnumerable<SubjectResultDto>> RetrieveAllAsync();
}
