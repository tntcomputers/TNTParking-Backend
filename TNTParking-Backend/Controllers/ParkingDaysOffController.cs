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
    public class ParkingDaysOffController : BaseController
    {
        private readonly IParkingDaysOffService _parkingDaysOff;

        public ParkingDaysOffController(IParkingDaysOffService parkingDaysOff)
        {
            _parkingDaysOff = parkingDaysOff;
        }

        [HttpGet("getParkingDaysOffs")]
        public async Task<ActionResult<ParkingDaysOff>> GetParkingDaysOffs()
        {
            return Ok(await _parkingDaysOff.GetParkingDaysOffs(UNIT_ID));
        }

        [HttpGet("getParkingDaysOff/{dayOffId}")]
        public async Task<ActionResult<ParkingDaysOff>> GetParkingDayOff(int dayOffId)
        {
            return Ok(await _parkingDaysOff.GetParkingDayOff(dayOffId));
        }

        [HttpGet("getParkingDaysOffsDs")]
        public async Task<ActionResult<ParkingDaysOff>> GetParkingDaysOffs([FromQuery] DataSourceLoadOptions loadOption)
        {
            return Ok(DataSourceLoader.Load(await _parkingDaysOff.GetParkingDaysOffs(UNIT_ID), loadOption));
        }

        [HttpPost("addDaysOff")]
        public async Task<ActionResult<ParkingDaysOff>> AddDaysOff([FromBody] List<ParkingDaysOff> parkingDaysOff)
        {
            return Ok(await _parkingDaysOff.AddDaysOff(UNIT_ID, parkingDaysOff));
        }

        [HttpPut("editDayOff")]
        public async Task<ActionResult<ParkingDaysOff>> EditDayOff([FromBody] ParkingDaysOff parkingDayOff)
        {
            return Ok(await _parkingDaysOff.EditDayOff(parkingDayOff));
        }


        [HttpDelete("deleteDayOff/{dayOffId}")]
        public async Task<ActionResult<bool>> DeleteDayOff(int dayOffId)
        {
            return Ok(await _parkingDaysOff.DeleteDayOff(dayOffId));
        }

        [HttpDelete("deleteDaysOff/{daysOffIdStr}")]
        public async Task<ActionResult<bool>> DeleteDaysOff(string daysOffIdStr)
        {
            var daysOffId = JsonConvert.DeserializeObject<List<int>>(daysOffIdStr);
            return Ok(await _parkingDaysOff.DeleteDaysOff(daysOffId));
        }
    }
}
