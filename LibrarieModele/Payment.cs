using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarieModele
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int SubscriptionId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(20)]
        public string PaymentMethod { get; set; }

        public virtual User User { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}
