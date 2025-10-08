using Context.Repository;

namespace TNTParking_Backend.Interfaces
{
    public interface IParkingRatesService
    {
        Task<IEnumerable<ParkingIntervals>> GetParkingIntervals(int unitId);
        Task<ParkingIntervals> GetParkingInterval(int parkingIntervarId);
        Task<ParkingIntervals> AddParkingInterval(int unitId, ParkingIntervals parkingInterval);
        Task<ParkingIntervals> EditParkingInterval(ParkingIntervals parkingInterval);
        Task<bool> DeleteParkingInterval(int parkingIntervarId);
    }
}
