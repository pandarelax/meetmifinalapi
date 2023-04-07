using meetmifinal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<Token> LoginAsync(string email, string password, int tokenLifeTime);
    }
}
