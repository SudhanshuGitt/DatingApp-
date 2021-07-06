using System;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class BuggyController : BaseAPIController
  {
    private readonly DataContext _context;
    public BuggyController(DataContext context)
    {
      _context = context;
    }

    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret()
    { 
      return "Secret Text";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFoud()
    {
      var thing = _context.Users.Find(-1);
      if (thing==null){
        return NotFound();
      }
      else{
        return Ok(thing);
      }
    }

    [HttpGet("server-error")]
    public ActionResult<string> GetServerError ()
    {
      
        var thing = _context.Users.Find(-1);
        // here we are genrating the null refrence exception as thing will return null as there is no primary key as  -1 find use only primay key to find the values
        var thingToReturn = thing.ToString();

        return thingToReturn;
    }
    
    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
      return BadRequest("This was not a good request");
    }
    
  }
}