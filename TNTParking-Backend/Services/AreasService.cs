using Context.Repository;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using TNTParking_Backend.Interfaces;

namespace TNTParking_Backend.Services
{
    public class AreasService : IAreasService
    {
        public readonly TntparkingContext _appContext;
        public AreasService(TntparkingContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Area> AddArea(int unitId, Area area)
        {
            using (var transaction = _appContext.Database.BeginTransaction())
            {
                try
                {
                    var parkingArea = new ParkingArea();
                    area.UnitId = unitId;
                    parkingArea.Map(area);
                    _appContext.Entry(parkingArea).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    _appContext.SaveChanges();
                    transaction.Commit();
                    return new Area(parkingArea);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return new Area();
                }
            }
        }

        public async Task<bool> DeleteArea(int areaId)
        {
            throw new NotImplementedException();
        }

        public async Task<Area> EditArea(Area area)
        {
            using (var transaction = _appContext.Database.BeginTransaction())
            {
                try
                {
                    var parkingArea = new ParkingArea();
                    parkingArea.Map(area);
                    _appContext.Entry(parkingArea).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _appContext.SaveChanges();
                    transaction.Commit();
                    return new Area(parkingArea);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return new Area();
                }
            }
        }

        public async Task<Area> GetArea(int areaId)
        {
            return _appContext.ParkingAreas.Include(x => x.ParkingParkingSpaces).Where(x => x.Id == areaId).Select(x => new Area(x)).FirstOrDefault() ?? new Area();
        }

        public async Task<IEnumerable<Area>> GetAreas(int unitId)
        {
            return _appContext.ParkingAreas.Where(x => x.UnitId == unitId).Select(x => new Area(x)).ToList();
        }
    }
}
