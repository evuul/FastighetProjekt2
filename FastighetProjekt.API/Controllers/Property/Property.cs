using FastighetProjekt.Repositories.Property;
using Microsoft.AspNetCore.Mvc;

namespace FastighetProjekt.Controllers.Property
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyRepository _repository;

        public PropertyController(IPropertyRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Property
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Models.Property.Property>>> GetAll()
        {
            var properties = await _repository.GetAllAsync();
            return Ok(properties);
        }

        // GET: api/Property/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Models.Property.Property>> GetById(int id)
        {
            var property = await _repository.GetByIdAsync(id);
            if (property == null)
                return NotFound();

            return Ok(property);
        }

        // POST: api/Property
        [HttpPost]
        public async Task<ActionResult<Models.Models.Property.Property>> Create(Models.Models.Property.Property property)
        {
            var created = await _repository.CreateAsync(property);
            return CreatedAtAction(nameof(GetById), new { id = created.PropertyId }, created);
        }

        // PUT: api/Property/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Models.Models.Property.Property property)
        {
            if (id != property.PropertyId)
                return BadRequest("Id mismatch");

            var updated = await _repository.UpdateAsync(property);
            if (updated == null)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Property/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}