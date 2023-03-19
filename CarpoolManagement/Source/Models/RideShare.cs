namespace CarpoolManagement.Source.Models
{
    public class RideShare
    {
        public int? Id { get; set; }
        public string StartLocation { get; set; } = string.Empty;
        public string EndLocation { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CarPlate { get; set; } = string.Empty;
        public int DriverId { get; set; }
        public IEnumerable<int> EmployeeIds { get; set; } = new List<int>();

    }
}
