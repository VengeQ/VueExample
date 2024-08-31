using Domain.Services.Quizes;

namespace Security
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _userRepository;

        public UsersService(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Autorize(string name, string password)
        {
            return await _userRepository.Autorize(name, password);
        }
    }
}
