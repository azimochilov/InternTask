using System.ComponentModel.DataAnnotations;

namespace InternTask.Service.DTOs.Subjects;
public class SubjectCreationDto
{
    [Required]
    public string Name { get; set; }
}
