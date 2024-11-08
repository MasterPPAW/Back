

namespace LibrarieModele.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string FitnessLevel { get; set; }
        public DateTime? TrialExpiration { get; set; }
        public string? NewProperty1 { get; set; }
        public string? NewProperty2 { get; set; }
    }
}
