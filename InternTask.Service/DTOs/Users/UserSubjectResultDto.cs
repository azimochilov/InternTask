using InternTask.Service.DTOs.Subjects;

namespace InternTask.Service.DTOs.Users;
public class UserSubjectResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public UserResultDto User { get; set; }

    public long SubjectId { get; set; }
    public SubjectResultDto Subject { get; set; }
}
