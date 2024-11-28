using AutoMapper;
using LibrarieModele.DTOs;
using LibrarieModele;
using NivelAccesDate.Accessors.Abstraction;

using NivelService.Abstraction;

namespace NivelService
{
    public class WorkoutPlansService : IWorkoutPlansService
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutPlansAccessor _workoutPlansAccessor;

        public WorkoutPlansService(IMapper mapper, IWorkoutPlansAccessor workoutPlansAccessor)
        {
            _mapper = mapper;
            _workoutPlansAccessor = workoutPlansAccessor;
        }

        public async Task<List<WorkoutPlanDTO>> GetWorkoutPlans()
        {
            var workoutPlans = await _workoutPlansAccessor.GetWorkoutPlans();

            return workoutPlans.Select(ent => _mapper.Map<WorkoutPlanDTO>(ent)).ToList();
        }

        public async Task<WorkoutPlanDTO> GetWorkoutPlan(int id)
        {
            return _mapper.Map<WorkoutPlanDTO>(await _workoutPlansAccessor.GetWorkoutPlan(id));
        }

        public async Task CreateWorkoutPlan(WorkoutPlanDTO workoutPlanDTO)
        {
            var toEntity = _mapper.Map<WorkoutPlan>(workoutPlanDTO);

            await _workoutPlansAccessor.CreateWorkoutPlan(toEntity);
        }

        public async Task<WorkoutPlanDTO> UpdateUser(WorkoutPlanDTO workoutPlanDTO, int id)
        {
            var foundWorkoutPlan = await _workoutPlansAccessor.GetWorkoutPlan(id);
            if (foundWorkoutPlan == null)
            {
                throw new ArgumentException("Wrong Id given.");
            }

            _mapper.Map(workoutPlanDTO, foundWorkoutPlan);

            await _workoutPlansAccessor.UpdateWorkoutPlan(foundWorkoutPlan);

            return await GetWorkoutPlan(id);
        }

        public async Task DeleteWorkoutPlan(int id)
        {
            await _workoutPlansAccessor.DeleteWorkoutPlan(id);
        }

        public async Task<bool> WorkoutPlanExists(int id)
        {
            return await _workoutPlansAccessor.WorkoutPlanExists(id);
        }
    }
}
