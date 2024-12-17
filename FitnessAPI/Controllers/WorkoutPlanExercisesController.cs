using LibrarieModele.DTOs;

using Microsoft.AspNetCore.Mvc;

using NivelService.Abstraction;

namespace FitnessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkoutPlanExercisesController(IWorkoutPlanExercisesService workoutPlanExercisesService) : ControllerBase
    {
        private const string PostBaseSuccessMessage = "Successfully registered";

        [HttpGet]
        public async Task<List<WorkoutPlanExerciseDTO>> Get()
        {
            return await workoutPlanExercisesService.GetWorkoutPlanExercises();
        }

        [HttpGet("{planId}/{exerciseId}")]
        public async Task<WorkoutPlanExerciseDTO> GetById(int planId, int exerciseId)
        {
            return await workoutPlanExercisesService.GetWorkoutPlanExercise(planId, exerciseId);
        }

        [HttpGet("plan/{planId}")]
        public async Task<List<ExerciseDTO>> GetExercisesByPlanId(int planId)
        {
            return await workoutPlanExercisesService.GetByPlanId(planId);
        }

        [HttpGet("exercise/{exerciseId}")]
        public async Task<List<WorkoutPlanDTO>> GetWorkoutPlansByExerciseId(int exerciseId)
        {
            return await workoutPlanExercisesService.GetByExerciseId(exerciseId);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<WorkoutPlanExerciseDTO> workoutPlanExerciseDTO)
        {
            var createdWPE = await workoutPlanExercisesService.CreateWorkoutPlanExercise(workoutPlanExerciseDTO);

            return CreatedAtAction(
                nameof(Get), 
                new { id = workoutPlanExerciseDTO[0].PlanId + workoutPlanExerciseDTO[0].ExerciseId }, 
                createdWPE);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] WorkoutPlanExerciseDTO workoutPlanExerciseDTO, int planId, int exerciseId)
        {
            return Ok(await workoutPlanExercisesService.UpdateWorkoutPlanExercise(workoutPlanExerciseDTO, planId, exerciseId));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int planId, int exerciseId)
        {
            await workoutPlanExercisesService.DeleteWorkoutPlanExercise(planId, exerciseId);
            return NoContent();
        }
    }
}
