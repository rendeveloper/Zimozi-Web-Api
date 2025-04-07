using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZimoziSolutions.ApiModels.Tokens;
using ZimoziSolutions.ApiModels.Users;
using ZimoziSolutions.Domain.Users;

namespace ZimoziSolutions.Core.Interfaces.Users
{
    public interface IAuthCoreService
    {
        Task<User?> RegisterAsync(UserModel request);
        Task<TokenResponseModel?> LoginAsync(UserModel request);
        Task<TokenResponseModel?> RefreshTokensAsync(RefreshTokenRequestModel request);
    }
}
