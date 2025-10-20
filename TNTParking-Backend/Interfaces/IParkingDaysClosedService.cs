using Context.Repository;

namespace TNTParking_Backend.Interfaces
{
    public interface IParkingDaysClosedService
    {
        public Task<IEnumerable<ParkingDaysClosed>> GetParkingDaysClosed(int unitId);
        public Task<ParkingDaysClosed> GetParkingDayClosed(int dayClosedId);
        public Task<IEnumerable<ParkingDaysClosed>> AddDaysClsoed(int unitId, List<ParkingDaysClosed> daysClosed);
        public Task<ParkingDaysClosed> EditDayClosed(ParkingDaysClosed dayClosed);
        public Task<bool> DeleteDayClosed(int dayClosedId);
        public Task<bool> DeleteDaysClosed(List<int> daysClosedId);
    }
}
