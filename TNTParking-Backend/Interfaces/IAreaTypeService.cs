using Context.Repository;
using System.Web.Http;

namespace TNTParking_Backend.Interfaces
{
    public interface IAreaTypeService
    {
        Task<IEnumerable<AreaTypes>> GetAreaTypes(int unitId);
        Task<AreaTypes> GetAreaType(int areaTypeId);
        Task<AreaTypes> AddAreaType(int unitId, [FromBody] AreaTypes areaType);
        Task<AreaTypes> EditAreaType([FromBody] AreaTypes areaType);
        Task<bool> DeleteAreaType(int areaTypeid);
    }
}
