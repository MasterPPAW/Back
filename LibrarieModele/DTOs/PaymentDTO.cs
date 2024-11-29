
namespace LibrarieModele.DTOs
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public int SubscriptionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }

    }
}
