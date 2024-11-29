using AutoMapper;
using LibrarieModele.DTOs;
using LibrarieModele;
using NivelAccesDate.Accessors.Abstraction;
using NivelService.Abstraction;

namespace NivelService
{
    public class ExercisesService : IExercisesService
    {
        private readonly IMapper _mapper;
        private readonly IExercisesAccessor _exercisesAccessor;

        public ExercisesService(IMapper mapper, IExercisesAccessor exercisesAccessor)
        {
            _mapper = mapper;
            _exercisesAccessor = exercisesAccessor;
        }

        public async Task<List<ExerciseDTO>> GetExercises()
        {
            var exercises = await _exercisesAccessor.GetExercises();

            return exercises.Select(ent => _mapper.Map<ExerciseDTO>(ent)).ToList();
        }

        public async Task<ExerciseDTO> GetExercise(int id)
        {
            return _mapper.Map<ExerciseDTO>(await _exercisesAccessor.GetExercise(id));
        }

        public async Task CreateExercise(ExerciseDTO exerciseDTO)
        {
            var toEntity = _mapper.Map<Exercise>(exerciseDTO);

            await _exercisesAccessor.CreateExercise(toEntity);
        }

        public async Task<ExerciseDTO> UpdateExercise(ExerciseDTO exerciseDTO, int id)
        {
            var foundExercise = await _exercisesAccessor.GetExercise(id);
            if (foundExercise == null)
            {
                throw new ArgumentException("Wrong Id given.");
            }

            _mapper.Map(exerciseDTO, foundExercise);

            await _exercisesAccessor.UpdateExercise(foundExercise);

            return await GetExercise(id);
        }

        public async Task DeleteExercise(int id)
        {
            await _exercisesAccessor.DeleteExercise(id);
        }

        public async Task<bool> ExerciseExists(int id)
        {
            return await _exercisesAccessor.ExerciseExists(id);
        }
    }
}
