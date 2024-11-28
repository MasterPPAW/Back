using LibrarieModele;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelAccesDate.Accessors.Abstraction
{
    public interface IWorkoutPlansAccessor
    {
        Task<List<WorkoutPlan>> GetWorkoutPlans();
        Task<WorkoutPlan> GetWorkoutPlan(int id);
        Task CreateWorkoutPlan(WorkoutPlan workoutPlan);
        Task UpdateWorkoutPlan(WorkoutPlan workoutPlan);
        Task DeleteWorkoutPlan(int id);
        Task<bool> WorkoutPlanExists(int id);
    }
}
