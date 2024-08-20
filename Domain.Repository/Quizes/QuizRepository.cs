using Domain.Quizes;
using Domain.Services.Quizes;

namespace Domain.Repository.Quizes
{
    /// <inheritdoc/>
    public class QuizRepository : IQuizRepository
    {
        private readonly QuizContext _quizContext;
        public QuizRepository(QuizContext quizContext) { _quizContext = quizContext; }

        public async Task<Quiz?> Get(int id)
        {
            var quizDto = await _quizContext.GetQuiz(id);
            return quizDto?.ToQuiz();
        }

        public Task<string> GetVersion()
        {
            return Task.FromResult(_quizContext.GetVersion());
        }
    }
}
