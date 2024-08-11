using Domain.Quizes;

namespace Domain.Services.Quizes
{
    /// <summary>
    /// Интерфейс для работы с квизами
    /// </summary>
    public interface IQuizService
    {
        /// <summary>
        /// Получить викторину
        /// </summary>
        /// <param name="id">Идентификатор викторины</param>
        /// <returns>Викторина</returns>
        public Quiz? GetQuiz(int id);
    }
}
