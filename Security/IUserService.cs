using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    public interface IUserService
    {
        public Task<User?> Autorize(string name, string password);
    }
}
