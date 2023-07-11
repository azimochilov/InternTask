using InternTask.Domain.Commons;

namespace InternTask.Domain.Entities;
public class UserSubject : Auditable
{
    public long StudentId { get; set; }
    public User Student { get; set; }

    public long SubjectId { get; set; }
    public Subject Subject { get; set; }
}
