using Context.Repository;

namespace TNTParking_Backend.Interfaces
{
    public interface IAreasService
    {
        public Task<IEnumerable<Area>> GetAreas(int unitId);
        public Task<Area> GetArea(int areaId);
        public Task<Area> AddArea(int unitId, Area area);
        public Task<Area> EditArea(Area area);
        public Task<bool> DeleteArea(int areaId);
    }
}
