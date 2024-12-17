
using LibrarieModele.DTOs;

namespace NivelService.Abstraction
{
    public interface IWorkoutPlanExercisesService
    {
        Task<List<WorkoutPlanExerciseDTO>> GetWorkoutPlanExercises();
        Task<WorkoutPlanExerciseDTO> GetWorkoutPlanExercise(int planId, int exerciseId);
        Task<List<ExerciseDTO>> GetByPlanId(int planId);
        Task<List<WorkoutPlanDTO>> GetByExerciseId(int exerciseId);
        Task<List<WorkoutPlanExerciseDTO>> CreateWorkoutPlanExercise(List<WorkoutPlanExerciseDTO> workoutPlanExerciseDTO);
        Task<WorkoutPlanExerciseDTO> UpdateWorkoutPlanExercise(WorkoutPlanExerciseDTO workoutPlanExerciseDTO, int planId, int exerciseId);
        Task DeleteWorkoutPlanExercise(int planId, int exerciseId);
        Task<bool> WorkoutPlanExerciseExists(int planId, int exerciseId);
    }
}
