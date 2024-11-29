using System.ComponentModel.DataAnnotations;

namespace LibrarieModele.DTOs
{
    public class SubscriptionDTO
    {
        public int SubscriptionId { get; set; }
        public int UserId { get; set; }

        [EnumDataType(typeof(FitnessLevel), ErrorMessage = "Invalid Fitness Level")]
        public string SubscriptionType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
    }
}
