using System.ComponentModel.DataAnnotations;

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
