using System.ComponentModel.DataAnnotations;

namespace CarReservationCore.Models
{
    public class Car
    {
        public int CarId { get; set; }

        [Required(ErrorMessage = "Pole Marka jest wymagane.")]
        [StringLength(50, ErrorMessage = "Marka może mieć maksymalnie 50 znaków.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Pole Model jest wymagane.")]
        [StringLength(50, ErrorMessage = "Model może mieć maksymalnie 50 znaków.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Pole Rok jest wymagane.")]
        [Range(1900, 2025, ErrorMessage = "Rok musi być w przedziale 1900 - 2025.")]
        public int Year { get; set; }
    }
}
