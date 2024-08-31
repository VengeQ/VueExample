using Domain.Services.Quizes;

namespace Security
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Autorize(string name, string password)
        {
            return await _userRepository.Autorize(name, password);
        }
    }
}
