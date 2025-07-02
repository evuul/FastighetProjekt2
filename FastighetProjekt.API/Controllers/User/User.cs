using FastighetProjekt.Data;
using FastighetProjekt.Models.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastighetProjekt.Controllers.User;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly FastighetProjektDbContext _context;

    public UserController(FastighetProjektDbContext context)
    {
        _context = context;
    }

    // GET: api/User
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _context.Users
            .Select(u => new
            {
                u.UserId,
                u.FirstName,
                u.LastName,
                u.Email,
                u.Role
            })
            .ToListAsync();

        return Ok(users);
    }

    // GET: api/User/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        return Ok(new
        {
            user.UserId,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Role
        });
    }

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] Models.Models.User.User user)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, new
        {
            user.UserId,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Role
        });
    }

    // PUT: api/User/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] Models.Models.User.User updatedUser)
    {
        if (id != updatedUser.UserId)
            return BadRequest();

        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        user.FirstName = updatedUser.FirstName;
        user.LastName = updatedUser.LastName;
        user.Email = updatedUser.Email;
        user.Role = updatedUser.Role;
        user.PasswordHash = updatedUser.PasswordHash;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
            return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}