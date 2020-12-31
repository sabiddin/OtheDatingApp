using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DatingApp.Api.Data;
using DatingApp.Api.DTOs;
using DatingApp.Api.Entities;
using DatingApp.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService) : base(context)
        {
            _tokenService = tokenService;
            _context = context;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();
            var user = new AppUser()
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key

            };
            var result = _context.Add(user);
            await _context.SaveChangesAsync();
            var userDto =new UserDto{
                Username = registerDto.Username,
                Token =_tokenService.CreateToken(user)
            };
            return Ok(userDto);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> login(LoginDto loginDto)
        {

            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (user == null) return Unauthorized("Invalid username or password");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var completedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < completedHash.Length; i++)
            {
                if (completedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid username or password");
            }
             var userDto =new UserDto{
                Username = loginDto.Username,
                Token =_tokenService.CreateToken(user)
            };

            return Ok(userDto);
        }

        private async Task<bool> UserExists(string username)
        {

            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }

    }
}