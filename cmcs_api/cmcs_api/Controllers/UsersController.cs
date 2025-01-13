using cmcs_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cmcs_api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public UsersController(ApplicationDbContext context)
            {
                _context = context;
            }


        //protecting the endpoints
        [Authorize]
        [HttpPost]
            public async Task<ActionResult<Users>> CreateUser(Users user)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUser), new { id = user.UserID }, user);
            }

        //protecting the endpoints
        [Authorize]
        [HttpGet("{id}")]
            public async Task<ActionResult<Users>> GetUser(int id)
            {
                var user = _context.Users.Find(id);

                if (user == null)
                {
                    return NotFound();
                }
                return user;
            }
        }
 }
