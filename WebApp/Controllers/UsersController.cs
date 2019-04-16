using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ViewModel.Auth;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserProcess _userProcess;
        private readonly AppSettings _appSettings;

        public UsersController(IUserProcess userProcess, IOptions<AppSettings> appSettings)
        {
            _userProcess = userProcess;
            _appSettings = appSettings.Value;
        }

        [HttpPost("register")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUp userSignUp)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                string message = await _userProcess.SignUpAsync(userSignUp);
                return Ok(message);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> LoginIn([FromBody] UserLogin userLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userProcess.LoginAsync(userLogin);
            if (user == null)
                return NotFound("Invalid credentials!!!");

            user.Token = GenerateToken(user);
            return Ok(user);
        }

        private string GenerateToken(UserInfo userInfo)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, userInfo.UserName),
                new Claim(ClaimTypes.Email, userInfo.Email),
                new Claim(ClaimTypes.Role,
                    string.IsNullOrWhiteSpace(userInfo.Role) ? string.Empty : userInfo.Role)
            };
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                issuer: "www.test.com",
                audience: "www.test.com",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signingCredentials
                );
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

    }
}