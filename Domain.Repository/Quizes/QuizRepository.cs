using Domain.Quizes;
using Domain.Services.Quizes;

namespace Domain.Repository.Quizes
{
    /// <inheritdoc/>
    public class QuizRepository : IQuizRepository
    {
        private readonly ApplicationContext _context;
        public QuizRepository(ApplicationContext quizContext) { _context = quizContext; }

        public async Task<Quiz?> Get(int id)
        {
            var quizDto = await _context.GetQuiz(id);
            return quizDto?.ToQuiz();
        }

        public async Task<IEnumerable<Quiz>> Get()
        {
            var quizDtos = await _context.Get();
            return quizDtos.Select(q => q.ToQuiz());
        }

        public Task<string> GetVersion()
        {
            return Task.FromResult(_context.GetVersion());
        }
    }
}
