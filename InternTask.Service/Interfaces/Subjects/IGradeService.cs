using InternTask.Service.DTOs.Subjects;

namespace InternTask.Service.Interfaces.Subjects;
public interface IGradeService
{
    ValueTask<GradesResultDto> AddAsync(GradesCreationDto dto);
    ValueTask<GradesResultDto> ModifyAsync(GradesUpdateDto dto);
    ValueTask<bool> RemoveAsync(long id);
    ValueTask<GradesResultDto> RetrieveByIdAsync(long id);    
    ValueTask<IEnumerable<GradesResultDto>> RetrieveAllAsync();
}
