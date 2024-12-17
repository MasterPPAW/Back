using LibrarieModele.DTOs;

namespace NivelService.Abstraction
{
    public interface IWorkoutPlansService
    {
        Task<List<WorkoutPlanDTO>> GetWorkoutPlans();
        Task<WorkoutPlanDTO> GetWorkoutPlan(int id);
        Task<WorkoutPlanDTO> CreateWorkoutPlan(WorkoutPlanDTO workoutPlanDTO);
        Task<WorkoutPlanDTO> UpdateWorkoutPlan(WorkoutPlanDTO workoutPlanDTO, int id);
        Task DeleteWorkoutPlan(int id);
        Task<bool> WorkoutPlanExists(int id);
    }
}
