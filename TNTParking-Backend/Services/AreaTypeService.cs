using Context.Repository;
using System.Web.Http;
using TNTParking_Backend.Interfaces;

namespace TNTParking_Backend.Services
{
    public class AreaTypeService : IAreaTypeService
    {
        private readonly TntparkingContext _appContext;

        public AreaTypeService(TntparkingContext appContext)
        {
            _appContext = appContext;
        }
        public async Task<AreaTypes> AddAreaType(int unitId, [FromBody] AreaTypes areaType)
        {
            try
            {
                areaType.UnitId = unitId;
                var parkingAreaType = new ParkingAreaType();
                parkingAreaType.Map(areaType);
                _appContext.Entry(parkingAreaType).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _appContext.SaveChanges();

                return new AreaTypes(parkingAreaType);
            }
            catch (Exception)
            {
                return new AreaTypes();
            }
        }

        public async Task<bool> DeleteAreaType(int areaTypeid)
        {
            var areaType = _appContext.ParkingAreaTypes.FirstOrDefault(x => x.Id == areaTypeid);
            if (areaType != null)
            {
                _appContext.Entry(areaType).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _appContext.SaveChanges();
            }
            return false;
        }

        public async Task<AreaTypes> EditAreaType([FromBody] AreaTypes areaType)
        {
            try
            {
                var parkingAreaType = new ParkingAreaType();
                parkingAreaType.Map(areaType);
                _appContext.Entry(parkingAreaType).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _appContext.SaveChanges();

                return new AreaTypes(parkingAreaType);
            }
            catch (Exception)
            {
                return new AreaTypes();
            }
        }

        public async Task<AreaTypes> GetAreaType(int areaTypeId)
        {
            return _appContext.ParkingAreaTypes.Where(x => x.Id == areaTypeId).Select(x => new AreaTypes(x)).FirstOrDefault();
        }

        public async Task<IEnumerable<AreaTypes>> GetAreaTypes(int unitId)
        {
            return _appContext.ParkingAreaTypes.Where(x => x.UnitId == unitId).Select(x => new AreaTypes(x)).ToList();
        }
    }
}
