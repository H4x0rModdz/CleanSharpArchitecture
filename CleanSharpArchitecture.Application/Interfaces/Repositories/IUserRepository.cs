using CleanSharpArchitecture.Domain.Entities;

namespace CleanSharpArchitecture.Application.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Cria um novo usuário no repositório.
        /// </summary>
        /// <param name="user">O usuário a ser criado.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona, contendo o usuário criado.</returns>
        Task<User> Create(User user);

        /// <summary>
        /// Seleciona um usuário pelo seu e-mail.
        /// </summary>
        /// <param name="email">O e-mail do usuário a ser buscado.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona, contendo o usuário encontrado ou null.</returns>
        Task<User> SelectByEmail(string email);

        /// <summary>
        /// Seleciona um usuário pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do usuário a ser buscado.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona, contendo o usuário encontrado ou null.</returns>
        Task<User> SelectById(Guid id);

        /// <summary>
        /// Atualiza as informações de um usuário existente.
        /// </summary>
        /// <param name="user">O usuário com as informações atualizadas.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
        Task Update(User user);

        /// <summary>
        /// Remove um usuário do repositório.
        /// </summary>
        /// <param name="id">O ID do usuário a ser removido.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
        Task Delete(Guid id);
    }
}