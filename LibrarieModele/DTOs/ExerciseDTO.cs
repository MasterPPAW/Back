using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele.DTOs
{
    public class ExerciseDTO
    {
        public int ExerciseId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        [EnumDataType(typeof(FitnessLevel), ErrorMessage = "Invalid Fitness Level")]
        public string DifficultyLevel { get; set; }
        public string Category { get; set; }
        public string? VideoUrl { get; set; }
        public int Duration { get; set; }
        public bool IsDeleted { get; set; }
    }
}
