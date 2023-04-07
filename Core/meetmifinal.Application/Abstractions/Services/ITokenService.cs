using meetmifinal.models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.services.Interfaces
{
    public interface ITokenService
    {
        Token CreateToken(User user, int tokenLifeTime);
        string CreateRefreshToken();
    }
}
