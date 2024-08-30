using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Security
{
    /// <summary>
    /// Интерфейс для работы с токенами
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Сгенерировать токен
        /// </summary>
        /// <param name="claim">Требования</param>
        /// <returns>троковое представление токена</returns>
        public string GenerateToken(IDictionary<string, object> claims);
    }
}
