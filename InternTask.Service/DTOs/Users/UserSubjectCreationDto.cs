using System.ComponentModel.DataAnnotations;

namespace InternTask.Service.DTOs.Users;
public class UserSubjectCreationDto
{
    [Required]
    public long StudentId { get; set; }
    [Required]
    public long SubjectId { get; set; }
}
