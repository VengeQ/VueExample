using Domain.Quizes;

namespace Domain.Services.Quizes
{
    /// <summary>
    /// Интерфейс для работы с квизами
    /// </summary>
    public interface IQuizService
    {
        public Quiz? GetQuiz(int id);
    }
}
