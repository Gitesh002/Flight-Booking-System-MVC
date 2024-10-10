using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fbs.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }    // Primary Key
        [ForeignKey("ReservationId")]
        public int ReservationId { get; set; }    // Foreign Key to Reservation
        public float Amount { get; set; }
        public string PaymentMethod { get; set; }    // e.g., CreditCard, PayPal
        public DateTime PaymentDate { get; set; }

        // Navigation property
        public virtual Reservation Reservation { get; set; }
    }

}
