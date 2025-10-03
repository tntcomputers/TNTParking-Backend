using Context.Repository;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
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

    public class AreasController : BaseController
    {
        private readonly IAreasService _areasService;

        public AreasController(IAreasService areasService)
        {
            _areasService = areasService;
        }

        [HttpGet("getAreas")]
        public async Task<ActionResult<List<Area>>> GetAreas()
        {
            return Ok(await _areasService.GetAreas(UNIT_ID));
        }

        [HttpGet("getAreasDs")]
        public async Task<ActionResult<List<Area>>> GetAreas([FromQuery] DataSourceLoadOptions loadOptions)
        {
            return Ok(DataSourceLoader.Load(await _areasService.GetAreas(UNIT_ID), loadOptions));
        }

        [HttpGet("getArea/{areaId}")]
        public async Task<ActionResult<List<Area>>> GetAreas(int areaId)
        {
            return Ok(await _areasService.GetArea(areaId));
        }

        [HttpPost("addArea")]
        public async Task<ActionResult<Area>> AddArea([FromBody] Area area)
        {
            return Ok(await _areasService.AddArea(UNIT_ID, area));
        }

        [HttpPut("editArea")]
        public async Task<ActionResult<Area>> EditArea([FromBody] Area area)
        {
            return Ok(await _areasService.EditArea(area));
        }

        [HttpDelete("deleteArea/{areaId}")]
        public async Task<ActionResult<Area>> DeleteArea(int areaId)
        {
            return Ok(await _areasService.DeleteArea(areaId));
        }
    }
}
