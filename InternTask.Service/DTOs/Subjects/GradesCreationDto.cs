using System.ComponentModel.DataAnnotations;

namespace InternTask.Service.DTOs.Subjects;
public class GradesCreationDto
{
    [Required]
    public long UserId { get; set; }
    [Required]
    public long SubjectId { get; set; }
    [Required]
    public int Grade { get; set; }
}
