using Context.Repository;

namespace TNTParking_Backend.Interfaces
{
    public interface IAreasDaysOffService
    {
        public Task<IEnumerable<AreasDaysOff>> GetAreasDaysOffs(int unitId);
        public Task<AreasDaysOff> GetParkingDayOff(int dayOffId);
        public Task<IEnumerable<AreasDaysOff>> AddDaysOff(int unitId, List<AreasDaysOff> daysOff);
        public Task<AreasDaysOff> EditDayOff(AreasDaysOff dayOff);
        public Task<bool> DeleteDayOff(int dayOffId);
        public Task<bool> DeleteDaysOff(List<int> daysOffId);
        public Task<IEnumerable<AreasDaysOff>> EditDaysOff(int unitId, List<AreasDaysOff> daysOff);
    }
}
