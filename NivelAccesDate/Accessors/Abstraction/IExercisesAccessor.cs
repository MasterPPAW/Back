
using LibrarieModele;

namespace NivelAccesDate.Accessors.Abstraction
{
    public interface IExercisesAccessor
    {
        Task<List<Exercise>> GetExercises();
        Task<Exercise> GetExercise(int id);
        Task<Exercise> CreateExercise(Exercise exercise);
        Task UpdateExercise(Exercise exercise);
        Task DeleteExercise(int id);
        Task<bool> ExerciseExists(int id);
    }
}
