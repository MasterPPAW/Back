using System.ComponentModel.DataAnnotations;

namespace LibrarieModele
{
    public class WorkoutPlanExercise
    {
        [Required]
        public int PlanId { get; set; }

        [Required]
        public int ExerciseId { get; set; }

        public virtual WorkoutPlan WorkoutPlan { get; set; }
        public virtual Exercise Exercise { get; set; }
    }
}
