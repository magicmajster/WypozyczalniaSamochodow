using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarReservationCore.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Data rozpoczęcia jest wymagana.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Data zakończenia jest wymagana.")]
      //  [DateGreaterThan("StartDate", ErrorMessage = "Data zakończenia musi być po dacie rozpoczęcia.")]
        public DateTime EndDate { get; set; }

        [Required]
        [ForeignKey("Car")]
        public int CarId { get; set; }
        public virtual Car Car { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [StringLength(30)]
        public string Status { get; set; } = "Pending";

      //  public string UserId { get; set; }

     //   public virtual ApplicationUser User { get; set; }   
    }
}
