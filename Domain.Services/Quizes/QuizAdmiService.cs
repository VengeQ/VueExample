﻿using Domain.Quizes;
using System.Collections.Frozen;

namespace Domain.Services.Quizes
{
    public class QuizAdmiService : IQuizAdminService
    {
        public Task Add(Quiz quiz)
        {
            throw new NotImplementedException();
        }

        public Task EditQuizItem(int quizId, int itemId, QuizItem newItem)
        {
            throw new NotImplementedException();
        }

        public Task<FrozenSet<Quiz>> Find(string title)
        {
            throw new NotImplementedException();
        }

        public Task<Quiz?> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
