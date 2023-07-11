using System.ComponentModel.DataAnnotations;

namespace InternTask.Service.DTOs.Users;
public class UserSubjectUpdateDto
{
    [Required]
    public long Id { get; set; }
    [Required]
    public long StudentId { get; set; }
    [Required]
    public long SubjectId { get; set; }
}
