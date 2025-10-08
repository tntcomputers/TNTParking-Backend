using Context.Repository;
using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using TNTParking_Backend.Interfaces;

namespace TNTParking_Backend.Services
{
    public class ParkingRatesService : IParkingRatesService
    {
        private readonly TntparkingContext _appContext;
        public ParkingRatesService(TntparkingContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<ParkingIntervals> AddParkingInterval(int unitId, ParkingIntervals interval)
        {
            using (var transaction = _appContext.Database.BeginTransaction())
            {
                try
                {

                    interval.UnitId = unitId;
                    var parkingInterval = new ParkingInterval();
                    parkingInterval.Map(interval);

                    _appContext.Entry(parkingInterval).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    _appContext.SaveChanges();

                    foreach (var areaInterval in interval.AreaIntervals)
                    {
                        areaInterval.UnitId = unitId;
                        areaInterval.Id = 0;
                        areaInterval.IdInterval = parkingInterval.Id;
                        var parkingAreaInterval = new ParkingAreaInterval();
                        parkingAreaInterval.Map(areaInterval);
                        _appContext.Entry(parkingAreaInterval).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }

                    foreach (var rate in interval.Rates)
                    {
                        rate.UnitId = unitId;
                        rate.Id = 0;
                        rate.IdInterval = parkingInterval.Id;
                        var parkingRates = new ParkingRate();
                        parkingRates.Map(rate);
                        _appContext.Entry(parkingRates).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    }


                    _appContext.SaveChanges();
                    transaction.Commit();
                    return interval;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return new ParkingIntervals();
                }
            }
        }

        public async Task<bool> DeleteParkingInterval(int parkingIntervarId)
        {
            try
            {

                var rates = _appContext.ParkingRates.Where(x => x.IdInterval == parkingIntervarId);
                if (rates.Count() > 0)
                {
                    _appContext.RemoveRange(rates);
                    _appContext.SaveChanges();
                }

                var areaIntervals = _appContext.ParkingAreaIntervals.Where(x => x.IdInterval == parkingIntervarId);

                if (areaIntervals.Count() > 0)
                {
                    _appContext.RemoveRange(areaIntervals);
                    _appContext.SaveChanges();
                }

                var interval = _appContext.ParkingIntervals.FirstOrDefault(x => x.Id == parkingIntervarId);
                if (interval != null)
                {
                    _appContext.Entry(interval).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    _appContext.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ParkingIntervals> EditParkingInterval(ParkingIntervals interval)
        {
            using (var transaction = _appContext.Database.BeginTransaction())
            {
                try
                {
                    var parkingRatesInterval = _appContext.ParkingRates.Where(x => x.IdInterval == interval.Id).AsNoTracking();
                    var parkingAreasInterval = _appContext.ParkingAreaIntervals.Where(x => x.IdInterval == interval.Id).AsNoTracking();

                    var parkingRatesIntervalToRemove = parkingRatesInterval.Where(x => !interval.Rates.Select(y => y.Id).Contains(x.Id));
                    var parkingRatesIntervalToAdd = interval.Rates.Where(x => x.Id < 0);
                    var ratesToEdit = interval.Rates.Except(parkingRatesIntervalToAdd);

                    _appContext.RemoveRange(parkingRatesIntervalToRemove);
                    if (ratesToEdit.Count() > 0)
                    {
                        ratesToEdit.ForEach(x =>
                        {
                            var parkingRate = new ParkingRate();
                            parkingRate.Map(x);
                            _appContext.Entry(parkingRate).State = EntityState.Modified;
                        });
                    }

                    if (parkingRatesIntervalToAdd.Count() > 0)
                    {
                        parkingRatesIntervalToAdd.ForEach(x =>
                        {
                            var parkingRates = new ParkingRate();
                            x.UnitId = interval.UnitId;
                            x.Id = 0;
                            x.IdInterval = interval.Id;
                            parkingRates.Map(x);
                            _appContext.Entry(parkingRates).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                        });
                    }


                    var parkingAreasIntervalToRemove = parkingAreasInterval.Where(x => !interval.AreaIntervals.Select(y => y.Id).Contains(x.Id));
                    var parkingAreasIntervalToAdd = interval.AreaIntervals.Where(x => x.Id < 0);
                    var areasIntervalToEdit = interval.AreaIntervals.Except(parkingAreasIntervalToAdd);

                    _appContext.RemoveRange(parkingAreasIntervalToRemove);
                    if (areasIntervalToEdit.Count() > 0)
                    {
                        var parkingAreasIntervalToEdit = new List<ParkingAreaInterval>();
                        areasIntervalToEdit.ForEach(x =>
                        {
                            var parkingAreaInterval = new ParkingAreaInterval();
                            parkingAreaInterval.Map(x);
                            _appContext.Entry(parkingAreaInterval).State = EntityState.Modified;
                        });
                    }

                    if (parkingAreasIntervalToAdd.Count() > 0)
                    {
                        parkingAreasIntervalToAdd.ForEach(x =>
                            {
                                var parkingAreaInterval = new ParkingAreaInterval();
                                x.UnitId = interval.UnitId;
                                x.Id = 0;
                                x.IdInterval = interval.Id;
                                parkingAreaInterval.Map(x);
                                _appContext.Entry(parkingAreaInterval).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                            });
                    }


                    var parkingInterval = new ParkingInterval();
                    parkingInterval.Map(interval);
                    _appContext.Entry(parkingInterval).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    _appContext.SaveChanges();
                    transaction.Commit();
                    return new ParkingIntervals(parkingInterval);
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    return new ParkingIntervals();
                }
            }
        }

        public async Task<ParkingIntervals> GetParkingInterval(int parkingIntervarId)
        {
            return _appContext.ParkingIntervals.Include(x => x.ParkingAreaIntervals).Include(x => x.ParkingRates).Where(x => x.Id == parkingIntervarId).Select(x => new ParkingIntervals(x)).FirstOrDefault() ?? new ParkingIntervals();
        }

        public async Task<IEnumerable<ParkingIntervals>> GetParkingIntervals(int unitId)
        {
            return _appContext.ParkingIntervals.Include(x => x.ParkingAreaIntervals).Include(x => x.ParkingRates).Where(x => x.UnitId == unitId).Select(x => new ParkingIntervals(x)).ToList();
        }
    }
}
