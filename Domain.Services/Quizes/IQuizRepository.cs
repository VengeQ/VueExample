using Domain.Quizes;

namespace Domain.Services.Quizes
{
    /// <summary>
    /// Интерфейс для работы с репозиторием
    /// </summary>
    public interface IQuizRepository
    {
        public Task<Quiz?> Get(int id);

        public Task<string> GetVersion();
    }
}
