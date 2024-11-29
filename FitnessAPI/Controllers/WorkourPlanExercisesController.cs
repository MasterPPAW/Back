using LibrarieModele.DTOs;

using Microsoft.AspNetCore.Mvc;

using NivelService.Abstraction;

namespace FitnessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkourPlanExercisesController(IWorkoutPlanExercisesService workoutPlanExercisesService) : ControllerBase
    {
        private const string PostBaseSuccessMessage = "Successfully registered";

        [HttpGet]
        public async Task<List<WorkoutPlanExerciseDTO>> Get()
        {
            return await workoutPlanExercisesService.GetWorkoutPlanExercises();
        }

        [HttpGet("{id}")]
        public async Task<WorkoutPlanExerciseDTO> GetById(int planId, int exerciseId)
        {
            return await workoutPlanExercisesService.GetWorkoutPlanExercise(planId, exerciseId);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WorkoutPlanExerciseDTO workoutPlanExerciseDTO)
        {
            await workoutPlanExercisesService.CreateWorkoutPlanExercise(workoutPlanExerciseDTO);

            string postMessage = PostBaseSuccessMessage + " " + workoutPlanExerciseDTO.PlanId + "_" + workoutPlanExerciseDTO.ExerciseId;
            return CreatedAtAction(nameof(Get), new { id = workoutPlanExerciseDTO.PlanId + workoutPlanExerciseDTO.ExerciseId }, postMessage);
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
