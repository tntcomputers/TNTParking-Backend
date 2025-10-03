using Context.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TNT_ANL_Backend.Services;
using TNTParking_Backend.Interfaces;

namespace TNTParking_Backend.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MainCorsPolicy")]
    [ApiController]
    [LogActionFilter]

    public class AreaTypeController : BaseController
    {
        private readonly IAreaTypeService _areaTypeService;

        public AreaTypeController(IAreaTypeService areaTypeService)
        {
            _areaTypeService = areaTypeService;
        }

        [HttpGet("getAreaTypes")]
        public async Task<ActionResult<List<AreaTypes>>> GetAreaTypes()
        {
            return Ok(await _areaTypeService.GetAreaTypes(UNIT_ID));
        }

        [HttpGet("getAreaType/{areaTypeId}")]
        public async Task<ActionResult<AreaTypes>> GetAreaType(int areaTypeId)
        {
            return Ok(await _areaTypeService.GetAreaType(areaTypeId));
        }

        [HttpPost("addAreaType")]
        public async Task<ActionResult<AreaTypes>> AddAreaType([FromBody] AreaTypes areaType)
        {
            return Ok(await _areaTypeService.AddAreaType(UNIT_ID, areaType));
        }

        [HttpPut("editAreaType")]
        public async Task<ActionResult<AreaTypes>> EditAreaType([FromBody] AreaTypes areaType)
        {
            return Ok(await _areaTypeService.EditAreaType(areaType));
        }

        [HttpDelete("deleteAreaType/{areaTypeId}")]
        public async Task<ActionResult<bool>> DeleteAreaType(int areaTypeId)
        {
            return Ok(await _areaTypeService.DeleteAreaType(areaTypeId));
        }
    }
}
