using Context.Repository;
using System.Diagnostics.Contracts;
using TNTParking_Backend.Interfaces;
using static iTextSharp.text.pdf.AcroFields;

namespace TNTParking_Backend.Services
{
    public class ParkingDaysOffService : IParkingDaysOffService
    {
        private readonly TntparkingContext _appContext;

        public ParkingDaysOffService(TntparkingContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<IEnumerable<ParkingDaysOff>> AddDaysOff(int unitId, List<ParkingDaysOff> daysOff)
        {
            try
            {
                var lparkingDaysOff = new List<ParkingParkingDaysOff>();

                foreach (var item in daysOff)
                {
                    item.UnitId = unitId;
                    var parkingDayOff = new ParkingParkingDaysOff();
                    parkingDayOff.Map(item);
                    if (!_appContext.ParkingParkingDaysOffs.Any(x => x.UnitId == item.UnitId && x.IdAreaType == item.IdAreaType && x.StartDate >= parkingDayOff.StartDate && x.EndDate <= parkingDayOff.EndDate))
                    {
                        lparkingDaysOff.Add(parkingDayOff);
                    }
                }
                _appContext.ParkingParkingDaysOffs.AddRange(lparkingDaysOff);
                _appContext.SaveChanges();

                return lparkingDaysOff.Select(x => new ParkingDaysOff(x));
            }
            catch (Exception e)
            {
                return new List<ParkingDaysOff>();
            }
        }

        public async Task<bool> DeleteDaysOff(int dayOffId)
        {
            var dayOff = _appContext.ParkingParkingDaysOffs.FirstOrDefault(x => x.Id == dayOffId);

            if (dayOff != null)
            {
                _appContext.Entry(dayOff).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _appContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<ParkingDaysOff> EditDayOff(ParkingDaysOff dayOff)
        {

            var parkingDayOff = new ParkingParkingDaysOff();
            parkingDayOff.Map(dayOff);

            _appContext.Entry(parkingDayOff).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _appContext.SaveChanges();


            return dayOff;
        }

        public async Task<IEnumerable<ParkingDaysOff>> GetParkingDaysOffs(int unitId)
        {
            return _appContext.ParkingParkingDaysOffs.Where(x => x.UnitId == unitId).Select(x => new ParkingDaysOff(x));
        }

        public async Task<IEnumerable<ParkingDaysOff>> EditDaysOff(int unitId, List<ParkingDaysOff> daysOff)
        {
            var daysOffToAdd = daysOff.Where(x => x.Id == 0).ToList();
            var daysOffToEdit = daysOff.Where(x => x.Id > 0).ToList();
            var daysOffExisting = _appContext.ParkingParkingDaysOffs.Where(x => x.UnitId == unitId).ToList();
            var daysOffToDelete = daysOffExisting.Where(x => !daysOff.Select(y => y.Id).Contains(x.Id)).ToList();

            foreach (var dayOff in daysOffToAdd)
            {
                dayOff.UnitId = unitId;
                var parkingDayOff = new ParkingParkingDaysOff();
                parkingDayOff.Map(dayOff);

                _appContext.Entry(parkingDayOff).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _appContext.SaveChanges();
            }

            foreach (var dayOff in daysOffToEdit)
            {
                dayOff.UnitId = unitId;
                var parkingDayOff = new ParkingParkingDaysOff();
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
                var dayOff = _appContext.ParkingParkingDaysOffs.FirstOrDefault(x => x.Id == dayOffId);

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
                var daysOff = _appContext.ParkingParkingDaysOffs.Where(x => daysOffId.Contains(x.Id));

                _appContext.ParkingParkingDaysOffs.RemoveRange(daysOff);
                _appContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ParkingDaysOff> GetParkingDayOff(int dayOffId)
        {
            var dayOff = _appContext.ParkingParkingDaysOffs.FirstOrDefault(x => x.Id == dayOffId);
            return new ParkingDaysOff(dayOff);
        }
    }
}
