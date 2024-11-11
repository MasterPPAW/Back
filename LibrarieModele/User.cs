using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarieModele
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now; 

        [Required]
        [Column(TypeName = "varchar(15)")]
        public string FitnessLevel { get; set; } = "beginner"; 

        [Column(TypeName = "date")]
        public DateTime? TrialExpiration { get; set; }

        [MaxLength(15)]
        public string? NewProperty1 { get; set; }

        [MaxLength(25)]
        public string? NewProperty2 { get; set; }

        public virtual ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
