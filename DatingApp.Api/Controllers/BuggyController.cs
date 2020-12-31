using DatingApp.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Api.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly DataContext _context;
        public BuggyController(DataContext context) : base(context)
        {
            _context = context;
        }        
        [HttpGet("not-found")]    
        public IActionResult GetNotFound(){
            var thing = _context.Users.Find(-1);
            if(thing == null) return NotFound();
            return Ok(thing);
        }        
        [HttpGet("server-error")]    
        public IActionResult GetServerError(){
            var thing = _context.Users.Find(-1);
            var thingToReturn = thing.ToString();            
            return Ok(thingToReturn);
        }        
        [HttpGet("bad-request")]    
        public IActionResult GetBadRequest(){

            return BadRequest("This was  not a  good request");
        }
                
        [Authorize]
        [HttpGet("auth")]    
        public IActionResult GetSecret(){
            return Ok("Secret text");
        }     
    }
}