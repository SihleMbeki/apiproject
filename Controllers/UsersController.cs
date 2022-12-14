using System;
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
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {

private DataContext _context;
        public UsersController(DataContext context )
        {
            _context=context;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
        //    var users= _context.users.ToList();
        var users= await _context.users.Where(x=>x.role=="public").ToListAsync();
           return users;
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser( int id){
        //    var users= _context.users.ToList();
        var users= await _context.users.FindAsync(id);
           return users;
        }
    }
}