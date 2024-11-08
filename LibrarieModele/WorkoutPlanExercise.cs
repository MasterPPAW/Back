using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
