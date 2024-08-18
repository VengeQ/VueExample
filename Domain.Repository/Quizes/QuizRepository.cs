using Domain.Quizes;
using Domain.Services.Quizes;

namespace Domain.Repository.Quizes
{
    public class QuizRepository : IQuizRepository
    {
        private readonly QuizContext _quizContext;
        public QuizRepository(QuizContext quizContext) { _quizContext = quizContext; }

        public async Task<Quiz?> Get(int id)
        {
            var quizDto = await _quizContext.GetQuiz(id);
            if (quizDto == null)
            {
                return null;
            }

            var items = quizDto.QuizItemDtos.Select(item => new QuizItem(
                item.QuizDtoId, 
                item.Question,
                ConvertAnswerOptions(item.AnswerOptions), 
                item.CorrectAnswerId)
            );
            return new Quiz(quizDto.QuizDtoId, quizDto.Title, items);
        }

        private static SortedDictionary<int, string> ConvertAnswerOptions(IEnumerable<AnswerOptionDto> answerOptions)
        {
            var dictionary = new SortedDictionary<int, string>();
            foreach (var answerOptionDto in answerOptions)
            {
                dictionary[answerOptionDto.Id] = answerOptionDto.Answer;
            }
            return dictionary;
        }

        public Task<string> GetVersion()
        {
            return Task.FromResult(_quizContext.GetVersion());
        }
    }
}
