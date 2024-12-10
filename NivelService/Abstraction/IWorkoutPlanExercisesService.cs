
using LibrarieModele.DTOs;

namespace NivelService.Abstraction
{
    public interface IWorkoutPlanExercisesService
    {
        Task<List<WorkoutPlanExerciseDTO>> GetWorkoutPlanExercises();
        Task<WorkoutPlanExerciseDTO> GetWorkoutPlanExercise(int planId, int exerciseId);
        Task<List<ExerciseDTO>> GetWorkoutPlanExercise(int planId);
        Task<List<WorkoutPlanDTO>> GetExerciseWorkoutPlan(int exerciseId);
        Task CreateWorkoutPlanExercise(WorkoutPlanExerciseDTO workoutPlanExerciseDTO);
        Task<WorkoutPlanExerciseDTO> UpdateWorkoutPlanExercise(WorkoutPlanExerciseDTO workoutPlanExerciseDTO, int planId, int exerciseId);
        Task DeleteWorkoutPlanExercise(int planId, int exerciseId);
        Task<bool> WorkoutPlanExerciseExists(int planId, int exerciseId);
    }
}
