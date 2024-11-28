using System.ComponentModel.DataAnnotations;

namespace LibrarieModele.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }

        [EnumDataType(typeof(FitnessLevel), ErrorMessage = "Invalid Fitness Level")]
        public string FitnessLevel { get; set; }
        public DateTime? TrialExpiration { get; set; }
        public string? NewProperty1 { get; set; }
        public string? NewProperty2 { get; set; }
    }

    public enum FitnessLevel
    {
        beginner,
        intermediate,
        advanced
    }
}
