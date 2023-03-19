namespace CarpoolManagement.Source.Models
{
    public class RideShareFullDetails
    {
        public int? Id { get; set; }
        public string StartLocation { get; set; } = string.Empty;
        public string EndLocation { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Car? Car { get; set; }
        public int DriverId { get; set; }
        public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();

    }
}
