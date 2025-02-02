﻿
using LibrarieModele;

using Microsoft.EntityFrameworkCore;

using NivelAccesDate.Accessors.Abstraction;

using Repository_CodeFirst;

namespace NivelAccesDate.Accessors
{
    public class ExercisesAccessor : IExercisesAccessor
    {
        private readonly FitnessDBContext _context;

        public ExercisesAccessor(FitnessDBContext context)
        {
            _context = context;
        }

        public async Task<List<Exercise>> GetExercises()
        {
            return await _context.Exercises.ToListAsync();
        }

        public async Task<List<Exercise>> GetExercisesByPlanId(int planId)
        {
            return await (from wpe in _context.WorkoutPlanExercises
                          join ex in _context.Exercises
                          on wpe.ExerciseId equals ex.ExerciseId
                          where wpe.PlanId == planId
                          select ex).ToListAsync();
        }


        public async Task<Exercise> GetExercise(int id)
        {
            return await _context.Exercises.FirstOrDefaultAsync(m => m.ExerciseId == id);
        }

        public async Task<Exercise> CreateExercise(Exercise exercise)
        {
            await _context.Exercises.AddAsync(exercise);
            await _context.SaveChangesAsync();

            return exercise;
        }

        public async Task UpdateExercise(Exercise exercise)
        {
            _context.Exercises.Update(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteExercise(int id)
        {
            var exercise = await _context.Exercises.FirstOrDefaultAsync(m => m.ExerciseId == id);
            if (exercise != null)
            {
                _context.Exercises.Remove(exercise);  
            }

            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExerciseExists(int id)
        {
            return await _context.Exercises.AnyAsync(u => u.ExerciseId == id);
        }
    }
}
