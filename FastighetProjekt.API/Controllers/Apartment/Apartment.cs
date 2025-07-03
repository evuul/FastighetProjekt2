using FastighetProjekt.Models.Models.Apartment;
using FastighetProjekt.Repositories.Apartment;
using Microsoft.AspNetCore.Mvc;

namespace FastighetProjekt.Controllers.Apartment;

[ApiController]
[Route("api/[controller]")]
public class ApartmentController : ControllerBase
{
    private readonly IApartmentRepository _apartmentRepository;

    public ApartmentController(IApartmentRepository apartmentRepository)
    {
        _apartmentRepository = apartmentRepository;
    }

    // GET: api/Apartment
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var apartments = await _apartmentRepository.GetAllAsync();
        return Ok(apartments);
    }

    // GET: api/Apartment/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var apartment = await _apartmentRepository.GetByIdAsync(id);
        if (apartment == null)
            return NotFound();

        return Ok(apartment);
    }

    // POST: api/Apartment
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Models.Models.Apartment.Apartment apartment)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _apartmentRepository.CreateAsync(apartment);
        return CreatedAtAction(nameof(GetById), new { id = created.ApartmentId }, created);
    }

    // PUT: api/Apartment/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Models.Models.Apartment.Apartment apartment)
    {
        if (id != apartment.ApartmentId)
            return BadRequest("ID mismatch");

        var existing = await _apartmentRepository.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        await _apartmentRepository.UpdateAsync(apartment);
        return NoContent();
    }

    // DELETE: api/Apartment/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _apartmentRepository.DeleteAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}