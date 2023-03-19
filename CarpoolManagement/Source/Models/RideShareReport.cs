namespace CarpoolManagement.Source.Models
{
    public class RideShareReport
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Trips { get; set; }
        public Car? Car { get; set; }
        public IEnumerable<Employee> Passengers { get; set; } = new List<Employee>();
    }
}
