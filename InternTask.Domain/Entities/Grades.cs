using InternTask.Domain.Commons;

namespace InternTask.Domain.Entities;
public class Grades : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }

    public long SubjectId { get; set; }
    public Subject Subject { get; set; }

    public int Grade { get; set; }
}
