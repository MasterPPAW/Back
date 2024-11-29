using LibrarieModele.DTOs;

using Microsoft.AspNetCore.Mvc;

using NivelService.Abstraction;

namespace FitnessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentsController(IPaymentsService paymentsService) : ControllerBase
    {
        private const string PostBaseSuccessMessage = "Successfully registered";

        [HttpGet]
        public async Task<List<PaymentDTO>> Get()
        {
            return await paymentsService.GetPayments();
        }

        [HttpGet("{id}")]
        public async Task<PaymentDTO> GetById(int id)
        {
            return await paymentsService.GetPayment(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentDTO paymentDTO)
        {
            await paymentsService.CreatePayment(paymentDTO);

            string postMessage = PostBaseSuccessMessage + " " + paymentDTO.PaymentMethod;
            return CreatedAtAction(nameof(Get), new { id = paymentDTO.PaymentId }, postMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] PaymentDTO paymentDTO, int id)
        {
            return Ok(await paymentsService.UpdatePayment(paymentDTO, id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await paymentsService.DeletePayment(id);
            return NoContent();
        }
    }
}
