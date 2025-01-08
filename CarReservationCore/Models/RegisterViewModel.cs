using System.ComponentModel.DataAnnotations;

namespace CarReservationCore.Models

{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [StringLength(50)]
        public string LastName { get; set; }
    }
}
