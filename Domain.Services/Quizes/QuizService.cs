using Domain.Quizes;

namespace Domain.Services.Quizes
{
    /// <inheritdoc/>
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository) { _quizRepository = quizRepository; }

        public async Task<Quiz?> GetQuiz(int id)
        {
            return await _quizRepository.Get(id);
        }
    }
}
