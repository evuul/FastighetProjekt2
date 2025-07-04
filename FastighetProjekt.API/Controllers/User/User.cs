using FastighetProjekt.Models.Models.User;
using FastighetProjekt.Repositories.User;
using Microsoft.AspNetCore.Mvc;

namespace FastighetProjekt.API.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Models.User.User>>> GetAllUsers()
        {
            var users = await _repository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Models.User.User>> GetUserById(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Models.Models.User.User>> CreateUser(Models.Models.User.User user)
        {
            var createdUser = await _repository.CreateUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, Models.Models.User.User user)
        {
            if (id != user.UserId)
                return BadRequest("ID mismatch");

            var updatedUser = await _repository.UpdateUserAsync(user);
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _repository.DeleteUserAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}