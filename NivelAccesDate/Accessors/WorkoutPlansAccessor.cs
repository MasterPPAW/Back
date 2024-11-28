using LibrarieModele;

using Microsoft.EntityFrameworkCore;

using NivelAccesDate.Accessors.Abstraction;

using Repository_CodeFirst;

namespace NivelAccesDate.Accessors
{
    public class WorkoutPlansAccessor : IWorkoutPlansAccessor
    {
        private readonly FitnessDBContext _context;

        public WorkoutPlansAccessor(FitnessDBContext context)
        {
            _context = context;
        }

        public async Task<List<WorkoutPlan>> GetWorkoutPlans()
        {
            return await _context.WorkoutPlans.ToListAsync();
        }

        public async Task<WorkoutPlan> GetWorkoutPlan(int id)
        {
            return await _context.WorkoutPlans.FirstOrDefaultAsync(m => m.PlanId == id);
        }

        public async Task CreateWorkoutPlan(WorkoutPlan workoutPlan)
        {
            await _context.WorkoutPlans.AddAsync(workoutPlan);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWorkoutPlan(WorkoutPlan workoutPlan)
        {
            _context.WorkoutPlans.Update(workoutPlan);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWorkoutPlan(int id)
        {
            var workoutPlan = await _context.WorkoutPlans.FirstOrDefaultAsync(m => m.PlanId == id);
            if (workoutPlan != null)
            {
                _context.WorkoutPlans.Remove(workoutPlan);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<bool> WorkoutPlanExists(int id)
        {
            return await _context.WorkoutPlans.AnyAsync(u => u.PlanId == id);
        }
    }
}
