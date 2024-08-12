using Domain.Services.Quizes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Quizes
{
    public class QuizRepository : IQuizRepository
    {
        private readonly QuizContext _quizContext;
        public QuizRepository(QuizContext quizContext) { _quizContext = quizContext; }

        public Domain.Quizes.Quiz Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetVersion()
        {
            return Task.FromResult(_quizContext.GetVersion());
        }
    }
}
