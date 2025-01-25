using CleanSharpArchitecture.Domain.Entities;
using System.Threading.Tasks;

namespace CleanSharpArchitecture.Application.Services.Interfaces
{
    public interface ITokenService
    {
        /// <summary>
        /// Gera um token JWT para o usuário fornecido.
        /// </summary>
        /// <param name="user">O usuário para o qual o token será gerado.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona, contendo o token gerado.</returns>
        Task<string> GenerateToken(User user);

        /// <summary>
        /// Valida se o token fornecido é válido.
        /// </summary>
        /// <param name="token">O token a ser validado.</param>
        /// <returns>Um valor booleano que indica se o token é válido.</returns>
        bool ValidateToken(string token);

        /// <summary>
        /// Extrai o ID do usuário do token fornecido.
        /// </summary>
        /// <param name="token">O token do qual o ID do usuário será extraído.</param>
        /// <returns>O ID do usuário como uma string.</returns>
        string GetUserIdFromToken(string token);
    }
}
