using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
       // private readonly IMapper _mapper;
        public AccountController(DataContext context, ITokenService tokenService)
        {
           // _mapper = mapper;
            _tokenService = tokenService;
            _context = context;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                role="public",
                email=registerDto.Email,
                userName = registerDto.Username.ToLower(),
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                passwordKey=hmac.Key
            };
            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                username = user.userName,
                Token = _tokenService.CreateToken(user),
                role = "public"
            };
        }
        [AllowAnonymous]
        [HttpPost("admin/21/register")]
        public async Task<ActionResult<UserDto>> AdminRegister(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                role="admin",
                email=registerDto.Email,
                userName = registerDto.Username.ToLower(),
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                passwordKey=hmac.Key
            };
            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                username = user.userName,
                Token = _tokenService.CreateToken(user),
                role = user.role
            };
        }
         [EnableCors("AllowOrigin")] 
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto  loginDto)
        {
            var user = await _context.users.Where(x => x.userName == loginDto.Username)
            //     .Include(p => p.Photos)
            .SingleOrDefaultAsync();

            if (user == null) return Unauthorized("Invalid username");
                      if (loginDto.Password == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.passwordKey);

             var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.passwordHash[i]) return Unauthorized("Invalid password");
            }

             return new UserDto
            {
                username = user.userName,
                Token = _tokenService.CreateToken(user),
                role = user.role,
                id=user.Id
            };
        }

        private async Task<bool> UserExists(string username)
        {
            return await _context.users.AnyAsync(x => x.userName == username.ToLower());
        }
    }
}