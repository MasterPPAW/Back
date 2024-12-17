
using AutoMapper;
using LibrarieModele.DTOs;
using LibrarieModele;
using NivelAccesDate.Accessors.Abstraction;

using NivelService.Abstraction;
using NivelAccesDate.Accessors;

namespace NivelService
{
    public class WorkoutPlanExercisesService : IWorkoutPlanExercisesService
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutPlanExercisesAccessor _workoutPlanExercisesAccessor;
        private readonly IExercisesService _exercisesService;
        private readonly IWorkoutPlansService _workoutPlansService;

        public WorkoutPlanExercisesService(IMapper mapper, IWorkoutPlanExercisesAccessor workoutPlanExercisesAccessor, 
            IExercisesService exercisesService, IWorkoutPlansService workoutPlansService)
        {
            _mapper = mapper;
            _workoutPlanExercisesAccessor = workoutPlanExercisesAccessor;
            _exercisesService = exercisesService;
            _workoutPlansService = workoutPlansService;
        }

        public async Task<List<WorkoutPlanExerciseDTO>> GetWorkoutPlanExercises()
        {
            var workoutPlanExercises = await _workoutPlanExercisesAccessor.GetWorkoutPlanExercises();

            return workoutPlanExercises.Select(ent => _mapper.Map<WorkoutPlanExerciseDTO>(ent)).ToList();
        }

        public async Task<WorkoutPlanExerciseDTO> GetWorkoutPlanExercise(int planId, int exerciseId)
        {
            return _mapper.Map<WorkoutPlanExerciseDTO>(await _workoutPlanExercisesAccessor.GetWorkoutPlanExercise(planId, exerciseId));
        }

        public async Task<List<ExerciseDTO>> GetByPlanId(int planId)
        {
            var plansOriginal = await _workoutPlanExercisesAccessor.GetByPlanId(planId);
            var plans = plansOriginal.Select(ent => _mapper.Map<WorkoutPlanExerciseDTO>(ent)).ToList();


            var exercisesIds = plans.Select(plan => plan.ExerciseId).ToList();
            
            var exercises = new List<ExerciseDTO>();
            foreach (var exerciseId in exercisesIds)
            {
                var exercise = await _exercisesService.GetExercise(exerciseId);
                if (exercise != null)
                {
                    exercises.Add(exercise);
                }
            }
            return exercises;
        }

        public async Task<List<WorkoutPlanDTO>> GetByExerciseId(int exerciseId)
        {
            var plansOriginal = await _workoutPlanExercisesAccessor.GetByExerciseId(exerciseId);
            var plans = plansOriginal.Select(ent => _mapper.Map<WorkoutPlanExerciseDTO>(ent)).ToList();


            var workoutPlanIds = plans.Select(plan => plan.PlanId).ToList();

            var workoutPlans = new List<WorkoutPlanDTO>();
            foreach (var workoutPlanId in workoutPlanIds)
            {
                var workoutPlan = await _workoutPlansService.GetWorkoutPlan(workoutPlanId);
                if (workoutPlan != null)
                {
                    workoutPlans.Add(workoutPlan);
                }
            }
            return workoutPlans;
        }

        public async Task<List<WorkoutPlanExerciseDTO>> CreateWorkoutPlanExercise(List<WorkoutPlanExerciseDTO> workoutPlanExerciseDTO)
        {
            var wpe = new List<WorkoutPlanExercise>();

            foreach (var workout in workoutPlanExerciseDTO)
            {
                var toEntity = _mapper.Map<WorkoutPlanExercise>(workout);

                wpe.Add(toEntity);
            }

            var wpeReturned = await _workoutPlanExercisesAccessor.CreateWorkoutPlanExercise(wpe);
            var wpeDTO = new List<WorkoutPlanExerciseDTO>();

            foreach (var wpedto in wpeReturned)
            {
                var toEntity = _mapper.Map<WorkoutPlanExerciseDTO>(wpedto);

                wpeDTO.Add(toEntity);
            }

            return wpeDTO;
        }

        public async Task<WorkoutPlanExerciseDTO> UpdateWorkoutPlanExercise(WorkoutPlanExerciseDTO workoutPlanExerciseDTO, int planId, int exerciseId)
        {
            var foundWorkoutPlanExercise = await _workoutPlanExercisesAccessor.GetWorkoutPlanExercise(planId, exerciseId);
            if (foundWorkoutPlanExercise == null)
            {
                throw new ArgumentException("Wrong Id given.");
            }

            _mapper.Map(workoutPlanExerciseDTO, foundWorkoutPlanExercise);

            await _workoutPlanExercisesAccessor.UpdateWorkoutPlanExercise(foundWorkoutPlanExercise);

            return await GetWorkoutPlanExercise(planId, exerciseId);
        }

        public async Task DeleteWorkoutPlanExercise(int planId, int exerciseId)
        {
            await _workoutPlanExercisesAccessor.DeleteWorkoutPlanExercise(planId, exerciseId);
        }

        public async Task<bool> WorkoutPlanExerciseExists(int planId, int exerciseId)
        {
            return await _workoutPlanExercisesAccessor.WorkoutPlanExerciseExists(planId, exerciseId);
        }
    }
}
