using System.ComponentModel.DataAnnotations;

namespace InternTask.Service.DTOs.Users;
public class UserUpdateDto
{
    [Required]
    public long Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Email { get; set; }
    
}
