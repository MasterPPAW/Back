using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele.DTOs
{
    public class WorkoutPlanDTO
    {
        public int PlanId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal DurationWeeks { get; set; }

        [EnumDataType(typeof(FitnessLevel), ErrorMessage = "Invalid Fitness Level")]
        public string FitnessLevel { get; set; }
    }
}
