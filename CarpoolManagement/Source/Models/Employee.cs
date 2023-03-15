namespace CarpoolManagement.Source.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDriver { get; set; }
    }
}
