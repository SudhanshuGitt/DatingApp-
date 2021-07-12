using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class UserRepository : IUserRepository
  {
    private readonly DataContext _context;
    public readonly IMapper _mapper;
    public UserRepository(DataContext context, IMapper mapper)
    {
      _mapper= mapper;
      _context = context;
    }
    // to restrict select satement we are projecting to our member Dto rather than taking whole AppUser
    public async Task<MemberDto> GetMemberAsync(string username)
    {
      return await _context.Users
        .Where(x => x.UserName == username)
        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();

        // single or default executes the database query

    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
      return await _context.Users
        // Mapper will provide configuration from Auto Mapper profiles
        .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
        .ToListAsync();
    }
    public async Task<AppUser> GetUserByIdAsync(int id)
    {
      return await _context.Users.FindAsync(id);
    }

    public async Task<AppUser> GetUserByUsername(string username)
    {
      return await _context.Users
      .Include(p => p.Photos)
      .SingleOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()
    {// if we want to get related collection too then we have to use eager loading
     // this will give us circular refrence exception bcz out app user have collection of photots and out photo have Appuser 
      return await _context.Users
      .Include(p => p.Photos)
      .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
      // true if greater than zero changes saved in the database it return no of changes saved in the database  
      return await _context.SaveChangesAsync() > 0;
    }

    public void Update(AppUser user)
    { // this let entity framework to update and flag to the entity its being modified
      _context.Entry(user).State = EntityState.Modified;
    }
  }
}