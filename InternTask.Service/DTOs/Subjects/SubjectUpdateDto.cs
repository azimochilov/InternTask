using System.ComponentModel.DataAnnotations;

namespace InternTask.Service.DTOs.Subjects;
public class SubjectUpdateDto
{
    [Required]
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
}
