using InternTask.Service.DTOs.Users;

namespace InternTask.Service.DTOs.Subjects;
public class GradesResultDto
{
    public long Id { get; set; }

    public long UserId { get; set; }
    public UserResultDto User { get; set; }

    public long SubjectId { get; set; }
    public SubjectResultDto Subject { get; set; }

    public int Grade { get; set; }
}
