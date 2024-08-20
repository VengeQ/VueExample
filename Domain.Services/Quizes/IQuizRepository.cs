using Domain.Quizes;

namespace Domain.Services.Quizes
{
    /// <summary>
    /// Интерфейс для работы с репозиторием
    /// </summary>
    public interface IQuizRepository
    {
        /// <summary>
        /// Получение викторины
        /// </summary>
        /// <param name="id">Идентификатор викторины</param>
        /// <returns>Викторина</returns>
        public Task<Quiz?> Get(int id);

        /// <summary>
        /// Получить версию БД
        /// </summary>
        /// <returns>Версия БД</returns>
        public Task<string> GetVersion();
    }
}
