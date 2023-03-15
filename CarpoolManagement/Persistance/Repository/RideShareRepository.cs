using CarpoolManagement.Source.Models;

namespace CarpoolManagement.Persistance.Repository
{
    public class RideShareRepository
    {
        private Dictionary<int, RideShare> _rideShares = new();

        public IEnumerable<RideShare> GetAll() => _rideShares.Values.ToList();

        public RideShare? GetById(int id)
        {
            _rideShares.TryGetValue(id, out RideShare? rideShare);
            return rideShare;
        }

        public RideShare Add(RideShare rideShare)
        {
            rideShare.Id = _rideShares.Keys.Count > 0 ? _rideShares.Keys.Max() + 1 : 1;
           
            _rideShares.Add(rideShare.Id.Value, rideShare);

            return rideShare;
        }

        public void Update(RideShare rideShare)
        {
            int id = rideShare.Id.Value;

            if (_rideShares.ContainsKey(id))
            {
                _rideShares[id] = rideShare;
            }
        }

        public void DeleteRideShare(int id)
        {
            if (_rideShares.ContainsKey(id))
            {
                _rideShares.Remove(id);
            }
        }

        public IEnumerable<RideShare> GetRideSharesForCar(string plate)
        {
            return _rideShares.Values.Where(rideShare => rideShare.CarPlate == plate).ToList();
        }
    }
}
