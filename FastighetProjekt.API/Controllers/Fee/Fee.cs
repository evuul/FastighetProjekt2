using FastighetProjekt.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FastighetProjekt.Controllers.Fee;

[ApiController]
[Route("api/[controller]")]
public class FeeController : ControllerBase
{
    private readonly FastighetProjektDbContext _context;
    public FeeController(FastighetProjektDbContext context)
    {
        _context = context;
    }

    [HttpGet(Name = "GetFees")]
    public async Task<IActionResult> GetFees()
    {
        var fees = await _context.Fees
            .Include(f => f.Apartment)
            .Select(f => new
            {
                f.FeeId,
                f.Month,
                f.Amount,
                f.DueDate,
                f.ApartmentId,
                IsPaid = _context.Payments.Any(p => p.FeeId == f.FeeId)
            })
            .ToListAsync();

        return Ok(fees);
    }

    [HttpGet("{id}", Name = "GetFeeById")]
    public IActionResult GetFee(int id)
    {
        var fee = _context.Fees.Find(id);
        if (fee == null)
        {
            return NotFound();
        }
        return Ok(fee);
    }

    [HttpPost(Name = "CreateFee")]
    public IActionResult CreateFee([FromBody] Models.Models.Fee.Fee fee)
    {
        if (fee == null)
        {
            return BadRequest("Fee cannot be null");
        }

        _context.Fees.Add(fee);
        _context.SaveChanges();

        return CreatedAtRoute("GetFeeById", new { id = fee.FeeId }, fee);
    }

    [HttpPut("{id}", Name = "UpdateFee")]
    public async Task<IActionResult> UpdateFee(int id, Models.Models.Fee.Fee fee)
    {
        if (id != fee.FeeId)
            return BadRequest();

        _context.Entry(fee).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Fees.Any(f => f.FeeId == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteFee")]
    public async Task<IActionResult> DeleteFee(int id)
    {
        var fee = await _context.Fees.FindAsync(id);
        if (fee == null)
        {
            return NotFound();
        }

        _context.Fees.Remove(fee);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}