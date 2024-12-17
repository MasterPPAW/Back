using AutoMapper;
using LibrarieModele.DTOs;
using LibrarieModele;
using NivelAccesDate.Accessors.Abstraction;

using NivelService.Abstraction;
using Microsoft.Extensions.Caching.Memory;

namespace NivelService
{
    public class WorkoutPlansService : IWorkoutPlansService
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutPlansAccessor _workoutPlansAccessor;
        private readonly IWorkoutPlanExercisesAccessor _workoutPlanExercisesAccessor;
        private readonly IMemoryCache _cache;

        public WorkoutPlansService(IMapper mapper, IWorkoutPlansAccessor workoutPlansAccessor, IWorkoutPlanExercisesAccessor workoutPlanExercisesAccessor, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _workoutPlansAccessor = workoutPlansAccessor;
            _workoutPlanExercisesAccessor = workoutPlanExercisesAccessor;
            _cache = memoryCache;
        }

        public async Task<List<WorkoutPlanDTO>> GetWorkoutPlans()
        {
            const string cacheKey = "WorkoutPlansCacheKey";

            /*var workoutPlans = await _workoutPlansAccessor.GetWorkoutPlans();

            return workoutPlans.Select(ent => _mapper.Map<WorkoutPlanDTO>(ent)).ToList();*/

            if (!_cache.TryGetValue(cacheKey, out List<WorkoutPlanDTO> cachedWorkoutPlans))
            {
                var workoutPlans = await _workoutPlansAccessor.GetWorkoutPlans();
                cachedWorkoutPlans = workoutPlans.Select(ent => _mapper.Map<WorkoutPlanDTO>(ent)).ToList();

                _cache.Set(cacheKey, cachedWorkoutPlans, TimeSpan.FromHours(1));
            }

            return cachedWorkoutPlans;
        }

        public async Task<WorkoutPlanDTO> GetWorkoutPlan(int id)
        {
            //return _mapper.Map<WorkoutPlanDTO>(await _workoutPlansAccessor.GetWorkoutPlan(id));

            var cacheKey = $"WorkoutPlan_{id}";  
            if (!_cache.TryGetValue(cacheKey, out WorkoutPlanDTO cachedWorkoutPlan))
            {
                var workoutPlan = await _workoutPlansAccessor.GetWorkoutPlan(id);
                if (workoutPlan == null)
                {
                    return null;
                }

                cachedWorkoutPlan = _mapper.Map<WorkoutPlanDTO>(workoutPlan);

                _cache.Set(cacheKey, cachedWorkoutPlan, TimeSpan.FromHours(1));
            }

            return cachedWorkoutPlan;
        }

        public async Task<WorkoutPlanDTO> CreateWorkoutPlan(WorkoutPlanDTO workoutPlanDTO)
        {
            var toEntity = _mapper.Map<WorkoutPlan>(workoutPlanDTO);

            return _mapper.Map<WorkoutPlanDTO>(await _workoutPlansAccessor.CreateWorkoutPlan(toEntity));
        }

        public async Task<WorkoutPlanDTO> UpdateWorkoutPlan(WorkoutPlanDTO workoutPlanDTO, int id)
        {
            var foundWorkoutPlan = await _workoutPlansAccessor.GetWorkoutPlan(id);
            if (foundWorkoutPlan == null)
            {
                throw new ArgumentException("Wrong Id given.");
            }

            _mapper.Map(workoutPlanDTO, foundWorkoutPlan);

            await _workoutPlansAccessor.UpdateWorkoutPlan(foundWorkoutPlan);

            _cache.Remove($"WorkoutPlan_{id}");
            _cache.Remove("WorkoutPlansCacheKey"); 

            return await GetWorkoutPlan(id);
        }

        public async Task DeleteWorkoutPlan(int id)
        {
            var workoutPlanExercisesToDelete = await _workoutPlanExercisesAccessor.GetByPlanId(id);

            foreach (var objToDel in workoutPlanExercisesToDelete)
            {
                await _workoutPlanExercisesAccessor.DeleteWorkoutPlanExercise(objToDel.PlanId, objToDel.ExerciseId);
            }

            await _workoutPlansAccessor.DeleteWorkoutPlan(id);

            _cache.Remove($"WorkoutPlan_{id}");
            _cache.Remove("WorkoutPlansCacheKey");
        }

        public async Task<bool> WorkoutPlanExists(int id)
        {
            return await _workoutPlansAccessor.WorkoutPlanExists(id);
        }
    }
}
