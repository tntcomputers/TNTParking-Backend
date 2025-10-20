using Context.Repository;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TNT_ANL_Backend.Services;
using TNTParking_Backend.Interfaces;
using TNTParking_Backend.Services;

namespace TNTParking_Backend.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MainCorsPolicy")]
    [ApiController]
    [LogActionFilter]
    public class AreasDaysOffController : BaseController
    {
        private readonly IAreasDaysOffService _AreasDaysOff;

        public AreasDaysOffController(IAreasDaysOffService AreasDaysOff)
        {
            _AreasDaysOff = AreasDaysOff;
        }

        [HttpGet("getAreasDaysOffs")]
        public async Task<ActionResult<AreasDaysOff>> GetAreasDaysOffs()
        {
            return Ok(await _AreasDaysOff.GetAreasDaysOffs(UNIT_ID));
        }

        [HttpGet("getAreasDaysOff/{dayOffId}")]
        public async Task<ActionResult<AreasDaysOff>> GetParkingDayOff(int dayOffId)
        {
            return Ok(await _AreasDaysOff.GetParkingDayOff(dayOffId));
        }

        [HttpGet("getAreasDaysOffsDs")]
        public async Task<ActionResult<AreasDaysOff>> GetAreasDaysOffs([FromQuery] DataSourceLoadOptions loadOption)
        {
            return Ok(DataSourceLoader.Load(await _AreasDaysOff.GetAreasDaysOffs(UNIT_ID), loadOption));
        }

        [HttpPost("addDaysOff")]
        public async Task<ActionResult<AreasDaysOff>> AddDaysOff([FromBody] List<AreasDaysOff> AreasDaysOff)
        {
            return Ok(await _AreasDaysOff.AddDaysOff(UNIT_ID, AreasDaysOff));
        }

        [HttpPut("editDayOff")]
        public async Task<ActionResult<AreasDaysOff>> EditDayOff([FromBody] AreasDaysOff parkingDayOff)
        {
            return Ok(await _AreasDaysOff.EditDayOff(parkingDayOff));
        }


        [HttpDelete("deleteDayOff/{dayOffId}")]
        public async Task<ActionResult<bool>> DeleteDayOff(int dayOffId)
        {
            return Ok(await _AreasDaysOff.DeleteDayOff(dayOffId));
        }

        [HttpDelete("deleteDaysOff/{daysOffIdStr}")]
        public async Task<ActionResult<bool>> DeleteDaysOff(string daysOffIdStr)
        {
            var daysOffId = JsonConvert.DeserializeObject<List<int>>(daysOffIdStr);
            return Ok(await _AreasDaysOff.DeleteDaysOff(daysOffId));
        }
    }
}
