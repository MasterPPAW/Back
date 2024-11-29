
using AutoMapper;
using LibrarieModele.DTOs;
using LibrarieModele;
using NivelAccesDate.Accessors.Abstraction;

using NivelService.Abstraction;

namespace NivelService
{
    public class WorkoutPlanExercisesService : IWorkoutPlanExercisesService
    {
        private readonly IMapper _mapper;
        private readonly IWorkoutPlanExercisesAccessor _workoutPlanExercisesAccessor;

        public WorkoutPlanExercisesService(IMapper mapper, IWorkoutPlanExercisesAccessor workoutPlanExercisesAccessor)
        {
            _mapper = mapper;
            _workoutPlanExercisesAccessor = workoutPlanExercisesAccessor;
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

        public async Task CreateWorkoutPlanExercise(WorkoutPlanExerciseDTO workoutPlanExerciseDTO)
        {
            var toEntity = _mapper.Map<WorkoutPlanExercise>(workoutPlanExerciseDTO);

            await _workoutPlanExercisesAccessor.CreateWorkoutPlanExercise(toEntity);
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
