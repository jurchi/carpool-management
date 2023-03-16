namespace CarpoolManagement.Models
{
    public class FindEmployeesRequest
    {
        public IEnumerable<int> Ids { get; set; } = new List<int>();
    }
}
