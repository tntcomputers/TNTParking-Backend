using Context.Repository;
using System.Diagnostics.Contracts;
using TNTParking_Backend.Interfaces;
using static iTextSharp.text.pdf.AcroFields;

namespace TNTParking_Backend.Services
{
    public class AreasDaysOffService : IAreasDaysOffService
    {
        private readonly TntparkingContext _appContext;

        public AreasDaysOffService(TntparkingContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<IEnumerable<AreasDaysOff>> AddDaysOff(int unitId, List<AreasDaysOff> daysOff)
        {
            try
            {
                var lAreasDaysOff = new List<ParkingAreasDaysOff>();

                foreach (var item in daysOff)
                {
                    item.UnitId = unitId;
                    var parkingDayOff = new ParkingAreasDaysOff();
                    parkingDayOff.Map(item);
                    if (!_appContext.ParkingAreasDaysOffs.Any(x => x.UnitId == item.UnitId && x.IdAreaType == item.IdAreaType && x.StartDate >= parkingDayOff.StartDate && x.EndDate <= parkingDayOff.EndDate))
                    {
                        lAreasDaysOff.Add(parkingDayOff);
                    }
                }
                _appContext.ParkingAreasDaysOffs.AddRange(lAreasDaysOff);
                _appContext.SaveChanges();

                return lAreasDaysOff.Select(x => new AreasDaysOff(x));
            }
            catch (Exception e)
            {
                return new List<AreasDaysOff>();
            }
        }

        public async Task<bool> DeleteDaysOff(int dayOffId)
        {
            var dayOff = _appContext.ParkingAreasDaysOffs.FirstOrDefault(x => x.Id == dayOffId);

            if (dayOff != null)
            {
                _appContext.Entry(dayOff).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _appContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<AreasDaysOff> EditDayOff(AreasDaysOff dayOff)
        {

            var parkingDayOff = new ParkingAreasDaysOff();
            parkingDayOff.Map(dayOff);

            _appContext.Entry(parkingDayOff).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _appContext.SaveChanges();


            return dayOff;
        }

        public async Task<IEnumerable<AreasDaysOff>> GetAreasDaysOffs(int unitId)
        {
            return _appContext.ParkingAreasDaysOffs.Where(x => x.UnitId == unitId).Select(x => new AreasDaysOff(x));
        }

        public async Task<IEnumerable<AreasDaysOff>> EditDaysOff(int unitId, List<AreasDaysOff> daysOff)
        {
            var daysOffToAdd = daysOff.Where(x => x.Id == 0).ToList();
            var daysOffToEdit = daysOff.Where(x => x.Id > 0).ToList();
            var daysOffExisting = _appContext.ParkingAreasDaysOffs.Where(x => x.UnitId == unitId).ToList();
            var daysOffToDelete = daysOffExisting.Where(x => !daysOff.Select(y => y.Id).Contains(x.Id)).ToList();

            foreach (var dayOff in daysOffToAdd)
            {
                dayOff.UnitId = unitId;
                var parkingDayOff = new ParkingAreasDaysOff();
                parkingDayOff.Map(dayOff);

                _appContext.Entry(parkingDayOff).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _appContext.SaveChanges();
            }

            foreach (var dayOff in daysOffToEdit)
            {
                dayOff.UnitId = unitId;
                var parkingDayOff = new ParkingAreasDaysOff();
                parkingDayOff.Map(dayOff);

                _appContext.Entry(parkingDayOff).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _appContext.SaveChanges();
            }

            foreach (var dayOff in daysOffToDelete)
            {
                _appContext.Entry(dayOff).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _appContext.SaveChanges();
            }

            return daysOff;
        }


        public async Task<bool> DeleteDayOff(int dayOffId)
        {
            try
            {
                var dayOff = _appContext.ParkingAreasDaysOffs.FirstOrDefault(x => x.Id == dayOffId);

                _appContext.Entry(dayOff).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _appContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteDaysOff(List<int> daysOffId)
        {
            try
            {
                var daysOff = _appContext.ParkingAreasDaysOffs.Where(x => daysOffId.Contains(x.Id));

                _appContext.ParkingAreasDaysOffs.RemoveRange(daysOff);
                _appContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<AreasDaysOff> GetParkingDayOff(int dayOffId)
        {
            var dayOff = _appContext.ParkingAreasDaysOffs.FirstOrDefault(x => x.Id == dayOffId);
            return new AreasDaysOff(dayOff);
        }
    }
}
