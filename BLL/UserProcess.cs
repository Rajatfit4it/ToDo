using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Contracts;
using DAL.Contracts;
using vm = ViewModel.Auth;
using dm = DAL.Domain;

namespace BLL
{
    public class UserProcess : IUserProcess
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserProcess(IUnitOfWork unitOfWork, IUserData userData, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _unitOfWork.UserData = userData;
        }

        public async Task<vm.UserInfo> LoginAsync(vm.UserLogin userLogin)
        {
            var user = await _unitOfWork.UserData.GetUserByUserNameAsync(userLogin.UserName);
            if (user == null)
                return null;

            bool isValidUser = VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt);
            if (isValidUser)
                return _mapper.Map<vm.UserInfo>(user);
            return null;
        }

        public async Task<vm.UserInfo> GetUserByUserNameAsync(string userName)
        {
            var user = await _unitOfWork.UserData.GetUserByUserNameAsync(userName);
            return _mapper.Map<vm.UserInfo>(user);
        }

        public async Task<string> SignUpAsync(vm.UserSignUp userSignUp)
        {
            var user = await _unitOfWork.UserData.GetUserByUserNameAsync(userSignUp.UserName);
            if (user != null)
                return "UserName already exists!!!";
            try
            {
                var dmUser = _mapper.Map<dm.User>(userSignUp);
                CreatePasswordHash(userSignUp.Password, out var passwordHash, out var passwordSalt);
                dmUser.PasswordHash = passwordHash;
                dmUser.PasswordSalt = passwordSalt;
                await _unitOfWork.UserData.CreateUserAsync(dmUser);
                await _unitOfWork.SaveChangesAsync();
                return "User Created Successfully!!!";

            }
            catch (Exception ex)
            {
                return "Some error occured";
            }

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }


    }
}
