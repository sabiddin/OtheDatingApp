using DatingApp.Api.Data;
using Microsoft.AspNetCore.Mvc;
using DatingApp.Api.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController: ControllerBase
    {        
        public BaseApiController()
        {            
        }
    }
}