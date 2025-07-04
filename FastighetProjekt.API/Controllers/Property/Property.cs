using FastighetProjekt.Models.Models.Property;
using FastighetProjekt.Services.Property;
using Microsoft.AspNetCore.Mvc;

namespace FastighetProjekt.API.Controllers.Property
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Models.Property.Property>>> GetAll()
        {
            var properties = await _propertyService.GetAllAsync();
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Models.Property.Property>> GetById(int id)
        {
            var property = await _propertyService.GetByIdAsync(id);
            if (property == null)
                return NotFound();

            return Ok(property);
        }

        [HttpPost]
        public async Task<ActionResult<Models.Models.Property.Property>> Create(Models.Models.Property.Property property)
        {
            var created = await _propertyService.CreateAsync(property);
            return CreatedAtAction(nameof(GetById), new { id = created.PropertyId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Models.Models.Property.Property property)
        {
            if (id != property.PropertyId)
                return BadRequest();

            var updated = await _propertyService.UpdateAsync(property);
            if (updated == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _propertyService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}