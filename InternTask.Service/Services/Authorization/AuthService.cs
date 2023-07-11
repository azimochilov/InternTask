using InternTask.Domain.Entities;
using InternTask.Service.DTOs.Users;
using InternTask.Service.Exceptions;
using InternTask.Service.Interfaces.Authorization;
using InternTask.Service.Interfaces.Users;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;

namespace InternTask.Service.Services.Authorization;
public class AuthService : IAuthService
{
    private readonly IUserService userService;
    private readonly IConfiguration configuration;

    public AuthService(IUserService userService, IConfiguration configuration)
    {
        this.userService = userService;
        this.configuration = configuration;
    }

    public async Task<LoginResultDto> AuthenticateAsync(string email, string password)
    {
        var user = await userService.RetrieveByEmailForAuthAsync(email);
        if (user == null)
            throw new InternTaskException(400, "Email or password is incorrect");
        
        return new LoginResultDto
        {
            Token = GenerateToken(user)
        };
    }

    private string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                 new Claim("Id", user.Id.ToString()),
                 new Claim(ClaimTypes.Name, user.FirstName)
            }),
            Audience = configuration["JWT:Audience"],
            Issuer = configuration["JWT:Issuer"],
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(configuration["JWT:Expire"])),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
