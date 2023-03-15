namespace CarpoolManagement.Source.Models
{
    public class Car
    {
        public string Plate { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Color { get; set; } = Models.Color.Other;
        public int NumberOfSeats { get; set; }
    }
}