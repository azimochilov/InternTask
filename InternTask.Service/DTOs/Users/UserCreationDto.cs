using System.ComponentModel.DataAnnotations;

namespace InternTask.Service.DTOs.Users;
public class UserCreationDto
{
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
