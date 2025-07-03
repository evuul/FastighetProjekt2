using FastighetProjekt.Models.Models.MaintenanceRequest;
using FastighetProjekt.Repositories.MaintenanceRequest;
using Microsoft.AspNetCore.Mvc;

namespace FastighetProjekt.Controllers.MaintenanceRequest;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceRequestController : ControllerBase
{
    private readonly IMaintenanceRequestRepository _maintenanceRequestRepository;

    public MaintenanceRequestController(IMaintenanceRequestRepository maintenanceRequestRepository)
    {
        _maintenanceRequestRepository = maintenanceRequestRepository;
    }

    // GET: api/MaintenanceRequest
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var requests = await _maintenanceRequestRepository.GetAllAsync();
        return Ok(requests);
    }

    // GET: api/MaintenanceRequest/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var request = await _maintenanceRequestRepository.GetByIdAsync(id);
        if (request == null)
            return NotFound();

        return Ok(request);
    }

    // POST: api/MaintenanceRequest
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Models.Models.MaintenanceRequest.MaintenanceRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _maintenanceRequestRepository.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.MaintenanceRequestId }, created);
    }

    // PUT: api/MaintenanceRequest/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Models.Models.MaintenanceRequest.MaintenanceRequest request)
    {
        if (id != request.MaintenanceRequestId)
            return BadRequest("ID mismatch");

        var updated = await _maintenanceRequestRepository.UpdateAsync(request);
        if (updated == null)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/MaintenanceRequest/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _maintenanceRequestRepository.DeleteAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}