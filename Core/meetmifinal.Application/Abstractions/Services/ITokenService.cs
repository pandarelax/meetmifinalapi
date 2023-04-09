using meetmifinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.Application.Abstractions.Services
{
    public interface ITokenService
    {
        Token CreateAccessToken(User user, int tokenLifeTime);
        string CreateRefreshToken();
    }
}
