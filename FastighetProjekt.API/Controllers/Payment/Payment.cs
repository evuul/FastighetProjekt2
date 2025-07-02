using FastighetProjekt.Data;
using Microsoft.AspNetCore.Mvc;

namespace FastighetProjekt.Controllers.Payment;
[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly FastighetProjektDbContext _context;
    public PaymentController(FastighetProjektDbContext context)
    {
        _context = context;
    }
    
    [HttpGet(Name = "GetPayments")]
    public IActionResult GetPayments()
    {
        var payments = _context.Payments
            .Select(p => new
            {
                p.PaymentId,
                p.Amount,
                p.PaidDate,
                p.FeeId,
                Fee = new
                {
                    p.Fee.FeeId,
                    p.Fee.Month,
                    p.Fee.Amount,
                    p.Fee.DueDate
                }
            })
            .ToList();

        return Ok(payments);
    }
    
    [HttpGet("{id}", Name = "GetPaymentById")]
    public IActionResult GetPayment(int id)
    {
        var payment = _context.Payments
            .Where(p => p.PaymentId == id)
            .Select(p => new
            {
                p.PaymentId,
                p.Amount,
                p.PaidDate,
                p.FeeId,
                Fee = new
                {
                    p.Fee.FeeId,
                    p.Fee.Month,
                    p.Fee.Amount,
                    p.Fee.DueDate
                }
            })
            .FirstOrDefault();

        if (payment == null)
        {
            return NotFound();
        }

        return Ok(payment);
    }
    
    [HttpPost(Name = "CreatePayment")]
    public IActionResult CreatePayment([FromBody] Models.Models.Payment.Payment payment)
    {
        if (payment == null)
        {
            return BadRequest("Payment cannot be null");
        }

        _context.Payments.Add(payment);
        _context.SaveChanges();

        return CreatedAtRoute("GetPaymentById", new { id = payment.PaymentId }, payment);
    }
    
    [HttpPut("{id}", Name = "UpdatePayment")]
    public IActionResult UpdatePayment(int id, [FromBody] Models.Models.Payment.Payment payment)
    {
        if (id != payment.PaymentId)
        {
            return BadRequest("Payment ID mismatch");
        }

        _context.Entry(payment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        try
        {
            _context.SaveChanges();
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
        {
            if (!_context.Payments.Any(p => p.PaymentId == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }
    
    [HttpDelete("{id}", Name = "DeletePayment")]
    public IActionResult DeletePayment(int id)
    {
        var payment = _context.Payments.Find(id);
        if (payment == null)
        {
            return NotFound();
        }

        _context.Payments.Remove(payment);
        _context.SaveChanges();

        return NoContent();
    }
}