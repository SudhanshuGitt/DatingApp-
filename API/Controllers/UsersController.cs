using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class UsersController : BaseAPIController 
  {
    private readonly DataContext _context;

    //in this class we have excess to our database via db context 
    public UsersController(DataContext context)
    {
        _context  = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){

      return  await _context.Users.ToListAsync();   
      
    }

    [Authorize]
    [HttpGet("{id}")]// we can specift route parameter 
    public async Task<ActionResult<AppUser>> GetUsers(int id){

     return await _context.Users.FindAsync(id);     
      
    }
  }
}