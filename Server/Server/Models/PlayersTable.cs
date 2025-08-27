using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class PlayersTable
    {
        [Required(ErrorMessage = "First name is required.")]
        [MinLength(2, ErrorMessage = "First name must be at least 2 characters long.")]
        public string? FirstName { get; set; }

        [MinLength(2, ErrorMessage = "Last name must be at least 2 characters long.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "ID is required.")]
        [Range(1, 1000, ErrorMessage = "ID must be between 1 and 1000.")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must contain 10 digits.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[A-Za-z]{3})(?=.*\d{3})[A-Za-z\d]{6}$", ErrorMessage = "Password must cotain 3 letter and 3 digits.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string? Country { get; set; }
    }
}
