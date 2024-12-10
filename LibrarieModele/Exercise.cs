using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele
{
    public class Exercise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExerciseId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [MaxLength(15)]
        public string DifficultyLevel { get; set; }

        [Required]
        [MaxLength(20)]
        public string Category { get; set; }

        [MaxLength(255)]
        public string? VideoUrl { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<WorkoutPlanExercise> WorkoutPlanExercises { get; set; } = new List<WorkoutPlanExercise>();
    }
}
