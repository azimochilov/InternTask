using InternTask.Service.DTOs.Users;

namespace InternTask.Service.Interfaces.Authorization;
public interface IAuthService
{
    Task<LoginResultDto> AuthenticateAsync(string email, string password);
}
