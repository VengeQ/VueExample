using Security;

namespace Domain.Services.Quizes
{
    /// <summary>
    /// Интерфейс для работы с пользователями
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Авторизоваться
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="password">Пароль</param>
        /// <returns>Пользователь</returns>
        public Task<User?> Autorize(string name, string password);
    }
}
