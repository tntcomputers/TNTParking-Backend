using Context.Repository;
using TNTParking_Backend.Interfaces;

namespace TNTParking_Backend.Services
{
    public class ParkingDaysClosedService : IParkingDaysClosedService
    {
        private readonly TntparkingContext _appContext;
        public ParkingDaysClosedService(TntparkingContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<IEnumerable<ParkingDaysClosed>> AddDaysClsoed(int unitId, List<ParkingDaysClosed> daysClosed)
        {
            try
            {
                var lAreasDaysClosed = new List<ParkingParkingDaysClosed>();

                foreach (var item in daysClosed)
                {
                    item.UnitId = unitId;
                    var parkingDayClosed = new ParkingParkingDaysClosed();
                    parkingDayClosed.Map(item);
                    if (!_appContext.ParkingParkingDaysCloseds.Any(x => x.UnitId == item.UnitId && x.IdArea == item.IdArea && x.StartDate >= parkingDayClosed.StartDate && x.EndDate <= parkingDayClosed.EndDate))
                    {
                        lAreasDaysClosed.Add(parkingDayClosed);
                    }
                }
                _appContext.ParkingParkingDaysCloseds.AddRange(lAreasDaysClosed);
                _appContext.SaveChanges();

                return lAreasDaysClosed.Select(x => new ParkingDaysClosed(x));
            }
            catch (Exception e)
            {
                return new List<ParkingDaysClosed>();
            }
        }

        public async Task<bool> DeleteDayClosed(int dayClosedId)
        {
            var dayClosed = _appContext.ParkingParkingDaysCloseds.FirstOrDefault(x => x.Id == dayClosedId);

            if (dayClosed != null)
            {
                _appContext.Entry(dayClosed).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _appContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteDaysClosed(List<int> daysClosedId)
        {
            try
            {
                var daysClosed = _appContext.ParkingParkingDaysCloseds.Where(x => daysClosedId.Contains(x.Id));

                _appContext.ParkingParkingDaysCloseds.RemoveRange(daysClosed);
                _appContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ParkingDaysClosed> EditDayClosed(ParkingDaysClosed dayClosed)
        {
            var parkingDayClosed = new ParkingParkingDaysClosed();
            parkingDayClosed.Map(dayClosed);

            _appContext.Entry(parkingDayClosed).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _appContext.SaveChanges();


            return dayClosed;
        }

        public async Task<IEnumerable<ParkingDaysClosed>> GetParkingDaysClosed(int unitId)
        {
            return _appContext.ParkingParkingDaysCloseds.Where(x => x.UnitId == unitId).Select(x => new ParkingDaysClosed(x));
        }

        public async Task<ParkingDaysClosed> GetParkingDayClosed(int dayClosedId)
        {
            var dayClsoed = _appContext.ParkingParkingDaysCloseds.FirstOrDefault(x => x.Id == dayClosedId);
            return new ParkingDaysClosed(dayClsoed);
        }
    }
}
