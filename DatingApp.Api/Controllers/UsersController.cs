using DatingApp.Api.Data;
using Microsoft.AspNetCore.Mvc;
using DatingApp.Api.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DatingApp.Api.Interfaces;

namespace DatingApp.Api.Controllers
{

    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {            
            return Ok( await _userRepository.GetUsersAsync());
        }
        //api/users/3        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {            
            return Ok(await _userRepository.GetUserByIdAsync(id));
        }
        //api/users/3        
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUser(string username)
        {            
            return Ok(await _userRepository.GetUserByUsernameAsync(username));
        }
    }
}