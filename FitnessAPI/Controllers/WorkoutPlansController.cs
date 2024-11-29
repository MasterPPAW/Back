using LibrarieModele.DTOs;

using Microsoft.AspNetCore.Mvc;

using NivelService.Abstraction;

namespace FitnessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkoutPlansController(IWorkoutPlansService workoutPlanService) : ControllerBase
    {
        private const string PostBaseSuccessMessage = "Successfully registered";

        [HttpGet]
        public async Task<List<WorkoutPlanDTO>> Get()
        {
            return await workoutPlanService.GetWorkoutPlans();
        }

        [HttpGet("{id}")]
        public async Task<WorkoutPlanDTO> GetById(int id)
        {
            return await workoutPlanService.GetWorkoutPlan(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkoutPlanDTO workoutPlanDTO)
        {
            await workoutPlanService.CreateWorkoutPlan(workoutPlanDTO);

            string postMessage = PostBaseSuccessMessage + " " + workoutPlanDTO.Name;
            return CreatedAtAction(nameof(Get), new { id = workoutPlanDTO.PlanId }, postMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] WorkoutPlanDTO workoutPlanDTO, int id)
        {
            return Ok(await workoutPlanService.UpdateWorkoutPlan(workoutPlanDTO, id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await workoutPlanService.DeleteWorkoutPlan(id);
            return NoContent();
        }
    }
}
