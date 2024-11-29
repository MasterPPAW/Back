
using LibrarieModele;

namespace NivelAccesDate.Accessors.Abstraction
{
    public interface IWorkoutPlanExercisesAccessor
    {
        Task<List<WorkoutPlanExercise>> GetWorkoutPlanExercises();
        Task<WorkoutPlanExercise> GetWorkoutPlanExercise(int planId, int exerciseId);
        Task CreateWorkoutPlanExercise(WorkoutPlanExercise workoutPlanExercise);
        Task UpdateWorkoutPlanExercise(WorkoutPlanExercise workoutPlanExercise);
        Task DeleteWorkoutPlanExercise(int planId, int exerciseId);
        Task<bool> WorkoutPlanExerciseExists(int planId, int exerciseId);
    }
}
