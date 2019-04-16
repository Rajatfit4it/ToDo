using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Auth;

namespace BLL.Contracts
{
    public interface IUserProcess
    {
        Task<string> SignUpAsync(UserSignUp userSignUp);
        Task<UserInfo> LoginAsync(UserLogin userLogin);
        Task<UserInfo> GetUserByUserNameAsync(string userName);

    }
}
