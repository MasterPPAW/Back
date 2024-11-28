using LibrarieModele.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelService.Abstraction
{
    public interface IWorkoutPlansService
    {
        Task<List<WorkoutPlanDTO>> GetWorkoutPlans();
        Task<WorkoutPlanDTO> GetWorkoutPlan(int id);
        Task CreateWorkoutPlan(WorkoutPlanDTO workoutPlanDTO);
        Task<WorkoutPlanDTO> UpdateUser(WorkoutPlanDTO workoutPlanDTO, int id);
        Task DeleteWorkoutPlan(int id);
        Task<bool> WorkoutPlanExists(int id);
    }
}
