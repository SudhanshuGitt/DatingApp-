using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class AccountController : BaseAPIController
  {
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    public AccountController(DataContext context, ITokenService tokenService)
    {
      _tokenService = tokenService;
      _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
      if (await UserExists(registerDto.Username))
        return BadRequest("User name is taken");

      // provides hashing algorithm we gona use to create the password Hash
      // using satement ensure when we done with this class it gona disposed off correctly.  
      using var hmac = new HMACSHA512();

      var user = new AppUser
      {
        UserName = registerDto.Username.ToLower(),
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        PasswordSalt = hmac.Key
      };
      // it says to ef we want to add this to our Users collection or table
      // Add method will track this in ef 
      _context.Users.Add(user);
      // to save in the database we use Save Changes
      await _context.SaveChangesAsync();

      return new UserDto
      {
        Username = user.UserName,
        Token = _tokenService.CreateToken(user)
      };

    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
      var user = await _context.Users
       .SingleOrDefaultAsync(x => x.UserName == loginDto.Username);

      if (user == null)
        return Unauthorized("Invalid username");
      // we gona pass the password salt as the key 
      // this will give same computed hash of the password as we are givig same key
      using var hmac = new HMACSHA512(user.PasswordSalt);

      var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
      // as we have supply same key used whilw register computed hash must match with the hash computed while registering the user if password is correct
      for (int i = 0; i < computedHash.Length; i++)
      {
        // if not matches
        if (computedHash[i] != user.PasswordHash[i])
          return Unauthorized("Invalid Password");
      }
      // if matches
      return new UserDto
      {
        Username = user.UserName,
        Token = _tokenService.CreateToken(user)
      };

    }

    private async Task<bool> UserExists(string username)
    {
      return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }




  }
}