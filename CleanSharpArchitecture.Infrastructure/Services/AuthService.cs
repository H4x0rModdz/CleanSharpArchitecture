using CleanSharpArchitecture.Application.DTOs.Auth.Request;
using CleanSharpArchitecture.Application.DTOs.Auth.Response;
using CleanSharpArchitecture.Application.Repositories.Interfaces;
using CleanSharpArchitecture.Application.Services.Interfaces;
using CleanSharpArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Transactions;

namespace CleanSharpArchitecture.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly BlobService _blobService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService, BlobService blobService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _blobService = blobService;
        }

        public async Task<RegisterResultDto> RegisterUserAsync(RegisterDto registerDto)
        {
            try
            {
                ValidateEmail(registerDto.Email);
                ValidatePassword(registerDto.Password);

                var imageUrl = await UploadProfileImageAsync(registerDto.ProfileImage);
                var newUser = await CreateUser(registerDto, imageUrl);

                Log.Information($"User {newUser.Name} registered successfully.");

                return new RegisterResultDto
                {
                    Success = true,
                    Errors = new List<string>()
                };
            }
            catch (Exception ex)
            {
                Log.Error($"Error registering user: {ex.Message}");
                return new RegisterResultDto
                {
                    Success = false,
                    Errors = new List<string> { ex.Message }
                };
            }
        }

        public async Task<LoginResultDto> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _userRepository.SelectByEmail(loginDto.Email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                throw new Exception("Invalid email or password.");

            var token = await _tokenService.GenerateToken(user);

            Log.Information($"User {user.Name} logged in successfully.");

            return new LoginResultDto
            {
                Success = true,
                Token = token,
                Errors = new List<string>()
            };
        }

        private async Task<string> UploadProfileImageAsync(IFormFile profileImage)
        {
            return await _blobService.UploadFileAsync(profileImage, "user-profile");
        }

        private async Task<User> CreateUser(RegisterDto registerDto, string imageUrl)
        {
            var existentUser = await _userRepository.SelectByEmail(registerDto.Email);
            if (existentUser != null)
                throw new Exception("The email is already in use.");

            var newUser = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                ProfileImageUrl = imageUrl,
                Biography = registerDto.Biography,
            };

            return await _userRepository.Create(newUser);
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains("@"))
                throw new Exception("Invalid email format.");
        }

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 8 || password.Length > 32 ||
                !password.Any(char.IsUpper) || !password.Any(char.IsLower) ||
                !password.Any(char.IsDigit) || !password.Any(ch => "!@#$%^&*?".Contains(ch)))
            {
                throw new Exception("Invalid password format.");
            }
        }
    }
}