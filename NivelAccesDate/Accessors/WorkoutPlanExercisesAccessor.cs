using LibrarieModele;
using Microsoft.EntityFrameworkCore;
using NivelAccesDate.Accessors.Abstraction;
using Repository_CodeFirst;

namespace NivelAccesDate.Accessors
{
    public class WorkoutPlanExercisesAccessor : IWorkoutPlanExercisesAccessor
    {
        private readonly FitnessDBContext _context;

        public WorkoutPlanExercisesAccessor(FitnessDBContext context)
        {
            _context = context;
        }

        public async Task<List<WorkoutPlanExercise>> GetWorkoutPlanExercises()
        {
            return await _context.WorkoutPlanExercises.ToListAsync();
        }

        public async Task<WorkoutPlanExercise> GetWorkoutPlanExercise(int planId, int exerciseId)
        {
            return await _context.WorkoutPlanExercises.FirstOrDefaultAsync(m => m.PlanId == planId && m.ExerciseId == exerciseId);
        }

        public async Task<List<WorkoutPlanExercise>> GetByPlanId(int planId)
        {
            return await _context.WorkoutPlanExercises.Where(m => m.PlanId == planId).ToListAsync();
        }

        public async Task<List<WorkoutPlanExercise>> GetByExerciseId(int exerciseId)
        {
            return await _context.WorkoutPlanExercises.Where(m => m.ExerciseId == exerciseId).ToListAsync();
        }

        public async Task<List<WorkoutPlanExercise>> CreateWorkoutPlanExercise(List<WorkoutPlanExercise> workoutPlanExercise)
        {
            var wpe = new List<WorkoutPlanExercise>();

            foreach (var workout in workoutPlanExercise)
            {
                await _context.WorkoutPlanExercises.AddAsync(workout);
                await _context.SaveChangesAsync();

                wpe.Add(workout);
            }

            return wpe;
        }

        public async Task UpdateWorkoutPlanExercise(WorkoutPlanExercise workoutPlanExercise)
        {
            _context.WorkoutPlanExercises.Update(workoutPlanExercise);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWorkoutPlanExercise(int planId, int exerciseId)
        {
            var workoutPlanExercise = await _context.WorkoutPlanExercises.FirstOrDefaultAsync(m => m.PlanId == planId && m.ExerciseId == exerciseId);
            if (workoutPlanExercise != null)
            {
                _context.WorkoutPlanExercises.Remove(workoutPlanExercise);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<bool> WorkoutPlanExerciseExists(int planId, int exerciseId)
        {
            return await _context.WorkoutPlanExercises.AnyAsync(u => u.PlanId == planId && u.ExerciseId == exerciseId);
        }
    }
}
