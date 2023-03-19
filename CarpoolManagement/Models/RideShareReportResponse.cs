using CarpoolManagement.Source.Models;

namespace CarpoolManagement.Models
{
    public class RideShareReportResponse
    {
        public IEnumerable<RideShareReport> Reports { get; set; }
    }
}
