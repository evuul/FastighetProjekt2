using FastighetProjekt.Models.Models.Fee;
using FastighetProjekt.Repositories.Fee;
using Microsoft.AspNetCore.Mvc;

namespace FastighetProjekt.Controllers.Fee;

[ApiController]
[Route("api/[controller]")]
public class FeeController : ControllerBase
{
    private readonly IFeeRepository _feeRepository;

    public FeeController(IFeeRepository feeRepository)
    {
        _feeRepository = feeRepository;
    }

    [HttpGet(Name = "GetFees")]
    public async Task<IActionResult> GetFees()
    {
        var fees = await _feeRepository.GetAllFeesAsync();

        var result = fees.Select(f => new
        {
            f.FeeId,
            f.Month,
            f.Amount,
            f.DueDate,
            f.ApartmentId,
            // Antingen hantera IsPaid i repository eller här, beroende på design
            IsPaid = false // Du behöver lägga till detta, om du vill
        });

        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetFeeById")]
    public async Task<IActionResult> GetFee(int id)
    {
        var fee = await _feeRepository.GetFeeByIdAsync(id);

        if (fee == null)
            return NotFound();

        return Ok(fee);
    }

    [HttpPost(Name = "CreateFee")]
    public async Task<IActionResult> CreateFee([FromBody] Models.Models.Fee.Fee fee)
    {
        if (fee == null)
            return BadRequest("Fee cannot be null");

        await _feeRepository.AddFeeAsync(fee);

        return CreatedAtRoute("GetFeeById", new { id = fee.FeeId }, fee);
    }

    [HttpPut("{id}", Name = "UpdateFee")]
    public async Task<IActionResult> UpdateFee(int id, Models.Models.Fee.Fee fee)
    {
        if (id != fee.FeeId)
            return BadRequest();

        try
        {
            await _feeRepository.UpdateFeeAsync(fee);
        }
        catch (Exception ex)
        {
            // Hantera t.ex concurrency exceptions om du vill
            if (!await _feeRepository.FeeExistsAsync(id))
                return NotFound();

            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteFee")]
    public async Task<IActionResult> DeleteFee(int id)
    {
        if (!await _feeRepository.FeeExistsAsync(id))
            return NotFound();

        await _feeRepository.DeleteFeeAsync(id);

        return NoContent();
    }
}