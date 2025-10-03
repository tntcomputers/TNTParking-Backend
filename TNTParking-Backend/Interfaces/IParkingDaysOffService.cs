using Context.Repository;

namespace TNTParking_Backend.Interfaces
{
    public interface IParkingDaysOffService
    {
        public Task<IEnumerable<ParkingDaysOff>> GetParkingDaysOffs(int unitId);
        public Task<ParkingDaysOff> GetParkingDayOff(int dayOffId);
        public Task<IEnumerable<ParkingDaysOff>> AddDaysOff(int unitId, List<ParkingDaysOff> daysOff);
        public Task<ParkingDaysOff> EditDayOff(ParkingDaysOff dayOff);
        public Task<bool> DeleteDayOff(int dayOffId);
        public Task<bool> DeleteDaysOff(List<int> daysOffId);
        public Task<IEnumerable<ParkingDaysOff>> EditDaysOff(int unitId, List<ParkingDaysOff> daysOff);
    }
}
