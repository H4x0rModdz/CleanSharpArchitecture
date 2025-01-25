using CleanSharpArchitecture.Application.DTOs.Auth.Request;
using CleanSharpArchitecture.Application.DTOs.Auth.Response;

namespace CleanSharpArchitecture.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResultDto> RegisterUserAsync(RegisterDto registerDto);
        Task<LoginResultDto> LoginUserAsync(LoginDto loginDto);
    }
}
