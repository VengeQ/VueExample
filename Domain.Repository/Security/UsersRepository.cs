using Domain.Repository.Quizes;
using Domain.Services.Quizes;
using Security;

namespace Domain.Repository.Security
{
    public class UsersRepository : IUserRepository
    {
        private readonly QuizContext _quizContext;

        public UsersRepository(QuizContext quizContext) { _quizContext = quizContext; }

        public async Task<User?> Autorize(string name, string password)
        {
            var userDto = await _quizContext.Autorize(name, password);

            return userDto?.ToUser();
        }
    }
}
