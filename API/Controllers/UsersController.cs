using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  [Authorize]
  public class UsersController : BaseAPIController
  {
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    //in this class we have excess to our database via db context 
    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
      _mapper = mapper;
      _userRepository = userRepository;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {

      var users = await _userRepository.GetMembersAsync();
      //first we specify what we want to map to then we pass the source object
      // var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
      return Ok(users);

    }

    [HttpGet("{username}")]// we can specift route parameter 
    public async Task<ActionResult<MemberDto>> GetUsers(string username)
    {
      // var user= await _userRepository.GetUserByUsername(username);
      // return _mapper.Map<MemberDto>(user);
      return  await _userRepository.GetMemberAsync(username);
      

    }
  }
}