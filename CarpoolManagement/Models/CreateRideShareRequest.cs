using System.ComponentModel.DataAnnotations;

namespace CarpoolManagement.Models
{
    public class CreateRideShareRequest
    {
        [Required]
        [MinLength(1)]
        public string? StartLocation { get; set; }

        [Required]
        [MinLength(1)]
        public string? EndLocation { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [RegularExpression("[a-zA-z]{2} [0-9]{3}-[a-zA-z]{2}", ErrorMessage = "Car Plate should be in AA 111-AA format")]
        public string? CarPlate { get; set; }

        [Required]

        public IEnumerable<int> EmployeeIds { get; set; } = new List<int>();
    }
}