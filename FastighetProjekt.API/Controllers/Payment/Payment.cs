using FastighetProjekt.Models.Models.Payment;
using FastighetProjekt.Repositories.Payment;
using Microsoft.AspNetCore.Mvc;

namespace FastighetProjekt.API.Controllers.Payment
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        // GET: api/Payment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return Ok(payments);
        }

        // GET: api/Payment/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        // POST: api/Payment
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.Models.Payment.Payment payment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _paymentRepository.CreateAsync(payment);
            return CreatedAtAction(nameof(GetById), new { id = created.PaymentId }, created);
        }

        // PUT: api/Payment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.Models.Payment.Payment payment)
        {
            if (id != payment.PaymentId)
                return BadRequest("ID mismatch");

            var updated = await _paymentRepository.UpdateAsync(payment);
            if (updated == null)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Payment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _paymentRepository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}