using LibrarieModele.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelService.Abstraction
{
    public interface IExercisesService
    {
        Task<List<ExerciseDTO>> GetExercises();
        Task<ExerciseDTO> GetExercise(int id);
        Task CreateExercise(ExerciseDTO exerciseDTO);
        Task<ExerciseDTO> UpdateExercise(ExerciseDTO exerciseDTO, int id);
        Task DeleteExercise(int id);
        Task<bool> ExerciseExists(int id);
    }
}
