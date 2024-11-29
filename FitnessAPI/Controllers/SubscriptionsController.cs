using LibrarieModele.DTOs;

using Microsoft.AspNetCore.Mvc;

using NivelService.Abstraction;

namespace FitnessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubscriptionsController(ISubscriptionsService subscriptionsService) : ControllerBase
    {
        private const string PostBaseSuccessMessage = "Successfully registered";

        [HttpGet]
        public async Task<List<SubscriptionDTO>> Get()
        {
            return await subscriptionsService.GetSubscriptions();
        }

        [HttpGet("{id}")]
        public async Task<SubscriptionDTO> GetById(int id)
        {
            return await subscriptionsService.GetSubscription(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SubscriptionDTO subscriptionDTO)
        {
            await subscriptionsService.CreateSubscription(subscriptionDTO);

            string postMessage = PostBaseSuccessMessage + " " + subscriptionDTO.SubscriptionId;
            return CreatedAtAction(nameof(Get), new { id = subscriptionDTO.SubscriptionId }, postMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] SubscriptionDTO subscriptionDTO, int id)
        {
            return Ok(await subscriptionsService.UpdateSubscription(subscriptionDTO, id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await subscriptionsService.DeleteSubscription(id);
            return NoContent();
        }
    }
}
