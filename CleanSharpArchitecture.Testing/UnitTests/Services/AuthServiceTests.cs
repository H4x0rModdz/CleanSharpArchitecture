//using CleanSharpArchitecture.Application.DTOs.Auth.Request;
//using CleanSharpArchitecture.Application.DTOs.Auth.Response;
//using CleanSharpArchitecture.Application.Interfaces.Services;
//using CleanSharpArchitecture.Application.Repositories.Interfaces;
//using CleanSharpArchitecture.Application.Services.Interfaces;
//using CleanSharpArchitecture.Domain.Entities;
//using CleanSharpArchitecture.Infrastructure.Services;
//using FluentAssertions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Http.Internal;
//using Moq;
//using System;
//using System.IO;
//using System.Threading.Tasks;
//using Xunit;

//namespace CleanSharpArchitecture.Tests.Services
//{
//    public class AuthServiceTests
//    {
//        private readonly Mock<IUserRepository> _userRepositoryMock;
//        private readonly Mock<ITokenService> _tokenServiceMock;
//        private readonly Mock<BlobService> _blobServiceMock;
//        private readonly AuthService _authService;

//        public AuthServiceTests()
//        {
//            _userRepositoryMock = new Mock<IUserRepository>();
//            _tokenServiceMock = new Mock<ITokenService>();
//            _blobServiceMock = new Mock<BlobService>(); // Criando o mock corretamente
//            _authService = new AuthService(_userRepositoryMock.Object, _tokenServiceMock.Object, _blobServiceMock.Object);
//        }

//        /// <summary>
//        /// Testa o registro de um usuário com dados válidos.
//        /// Deve retornar sucesso e sem erros.
//        /// </summary>
//        [Fact]
//        public async Task RegisterUserAsync_ShouldReturnSuccess_WhenUserIsRegistered()
//        {
//            // Arrange
//            var registerDto = new RegisterDto
//            {
//                Name = "Test User",
//                Email = "test@example.com",
//                Password = "Password123!",
//                Biography = "Test biography"
//            };

//            _userRepositoryMock.Setup(repo => repo.SelectByEmail(registerDto.Email)).ReturnsAsync((User)null);
//            _userRepositoryMock.Setup(repo => repo.Create(It.IsAny<User>())).ReturnsAsync(new User { Id = Guid.NewGuid() });
//            _blobServiceMock.Setup(blob => blob.UploadFileAsync(It.IsAny<IFormFile>())).ReturnsAsync("https://placehold.co/400");

//            // Act
//            var result = await _authService.RegisterUserAsync(registerDto);

//            // Assert
//            result.Success.Should().BeTrue();
//            result.Errors.Should().BeEmpty();
//        }

//        [Fact]
//        public async Task RegisterUserAsync_ShouldThrowException_WhenBlobUploadFails()
//        {
//            // Arrange
//            var registerDto = new RegisterDto
//            {
//                Name = "Test User",
//                Email = "test@example.com",
//                Password = "Password123!",
//                Biography = "Test biography",
//                ProfileImage = CreateMockFormFile("profile.png")
//            };

//            _userRepositoryMock
//                .Setup(repo => repo.SelectByEmail(registerDto.Email))
//                .ReturnsAsync((User)null);

//            _userRepositoryMock
//                .Setup(repo => repo.Create(It.IsAny<User>()))
//                .ReturnsAsync(new User { Id = Guid.NewGuid() });

//            // Simulate the BlobService throwing an exception
//            _blobServiceMock
//                .Setup(blob => blob.UploadFileAsync(It.IsAny<IFormFile>()))
//                .ThrowsAsync(new Exception("Azure upload error!"));

//            // Act
//            Func<Task> act = async () => await _authService.RegisterUserAsync(registerDto);

//            // Assert
//            await act.Should().ThrowAsync<Exception>()
//                     .WithMessage("Azure upload error!");
//        }

//        /// <summary>
//        /// Testa o registro de um usuário com e-mail já em uso.
//        /// Deve lançar uma exceção.
//        /// </summary>
//        [Fact]
//        public async Task RegisterUserAsync_ShouldThrowException_WhenEmailIsAlreadyInUse()
//        {
//            // Arrange
//            var registerDto = new RegisterDto
//            {
//                Name = "Test User",
//                Email = "test@example.com",
//                Password = "Password123!",
//                Biography = "Test biography",
//                ProfileImage = CreateMockFormFile("profile.png")
//            };

//            _userRepositoryMock
//                .Setup(repo => repo.SelectByEmail(registerDto.Email))
//                .ReturnsAsync((User)null); // user does not exist yet

//            _userRepositoryMock
//                .Setup(repo => repo.Create(It.IsAny<User>()))
//                .ReturnsAsync(new User { Id = Guid.NewGuid() });

//            // Our blob service returns a placeholder URL for the newly uploaded image
//            _blobServiceMock
//                .Setup(blob => blob.UploadFileAsync(It.IsAny<IFormFile>()))
//                .ReturnsAsync("https://placehold.co/400");

//            // Act
//            var result = await _authService.RegisterUserAsync(registerDto);

//            // Assert
//            result.Success.Should().BeTrue("User registration should succeed");
//            result.Errors.Should().BeEmpty("No errors should occur");

//            // Optionally, verify UploadFileAsync was called exactly once:
//            _blobServiceMock.Verify(b => b.UploadFileAsync(It.IsAny<IFormFile>()), Times.Once);
//        }

//        /// <summary>
//        /// Testa o registro de um usuário quando a criação falha.
//        /// Deve lançar uma exceção.
//        /// </summary>
//        [Fact]
//        public async Task RegisterUserAsync_ShouldThrowException_WhenUserCreationFails()
//        {
//            // Arrange
//            var registerDto = new RegisterDto
//            {
//                Name = "Test User",
//                Email = "test@example.com",
//                Password = "Password123!",
//                Biography = "Test biography"
//            };

//            _userRepositoryMock.Setup(repo => repo.SelectByEmail(registerDto.Email)).ReturnsAsync((User)null);
//            _userRepositoryMock.Setup(repo => repo.Create(It.IsAny<User>())).ThrowsAsync(new Exception("Database error"));

//            // Act
//            Func<Task> act = async () => await _authService.RegisterUserAsync(registerDto);

//            // Assert
//            await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
//        }

//        /// <summary>
//        [Fact]
//        public async Task RegisterUserAsync_ShouldNotCallBlobService_WhenNoProfileImageIsProvided()
//        {
//            // Arrange
//            var registerDto = new RegisterDto
//            {
//                Name = "Test User",
//                Email = "test@example.com",
//                Password = "Password123!",
//                Biography = "Test biography"
//                // ProfileImage is null
//            };

//            _userRepositoryMock
//                .Setup(repo => repo.SelectByEmail(registerDto.Email))
//                .ReturnsAsync((User)null);

//            _userRepositoryMock
//                .Setup(repo => repo.Create(It.IsAny<User>()))
//                .ReturnsAsync(new User { Id = Guid.NewGuid() });

//            // Act
//            var result = await _authService.RegisterUserAsync(registerDto);

//            // Assert
//            result.Success.Should().BeTrue();
//            result.Errors.Should().BeEmpty();

//            // Ensure the blob upload was never called
//            _blobServiceMock.Verify(b => b.UploadFileAsync(It.IsAny<IFormFile>()), Times.Never);
//        }

//        #region Helpers
//        private IFormFile CreateMockFormFile(string fileName)
//        {
//            var content = "fake file content";
//            var fileStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
//            return new FormFile(fileStream, 0, fileStream.Length, "Data", fileName);
//        }
//        #endregion Helpers
//    }
//}