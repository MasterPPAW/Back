using LibrarieModele.DTOs;

using Microsoft.AspNetCore.Mvc;

using NivelService;
using NivelService.Abstraction;

namespace FitnessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExercisesController(IExercisesService exercisesService) : ControllerBase
    {
        private const string PostBaseSuccessMessage = "Successfully registered";

        [HttpGet]
        public async Task<List<ExerciseDTO>> Get()
        {
            return await exercisesService.GetExercises();
        }

        [HttpGet("{id}")]
        public async Task<ExerciseDTO> GetById(int id)
        {
            return await exercisesService.GetExercise(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExerciseDTO exerciseDTO)
        {
            var createdExercise = await exercisesService.CreateExercise(exerciseDTO);

            return CreatedAtAction(
                nameof(Get), 
                new { id = exerciseDTO.ExerciseId },
                createdExercise
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] ExerciseDTO exerciseDTO, int id)
        {
            return Ok(await exercisesService.UpdateExercise(exerciseDTO, id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await exercisesService.DeleteExercise(id);
            return NoContent();
        }
    }
}
