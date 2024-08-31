using Domain.Services.Quizes;
using Microsoft.EntityFrameworkCore;
using Security;
using Utils;

namespace Domain.Repository.Security
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationContext _context;

        public UsersRepository(ApplicationContext quizContext) { _context = quizContext; }

        public async Task<User> Autorize(string name, string password)
        {
            var userDto = await _context.Users.FirstOrDefaultAsync(x => x.Name == name);

            if (userDto == null)
            {
                throw new AutorizationException("Неверное имя пользователя");
            }

            if(userDto.Password != password)
            {
                throw new AutorizationException("Неверный пароль");
            }

            return userDto.ToUser();
        }
    }
}
