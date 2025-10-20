using Context.Repository;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TNT_ANL_Backend.Services;
using TNTParking_Backend.Interfaces;

namespace TNTParking_Backend.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MainCorsPolicy")]
    [ApiController]
    [LogActionFilter]
    public class ParkingDayClosedController : BaseController
    {
        private readonly IParkingDaysClosedService _parkingDaysClosed;

        public ParkingDayClosedController(IParkingDaysClosedService parkingDaysClosed)
        {
            _parkingDaysClosed = parkingDaysClosed;
        }

        [HttpGet("getParkingDaysClosed")]
        public async Task<ActionResult<ParkingDaysClosed>> GetAreasDaysClosed()
        {
            return Ok(await _parkingDaysClosed.GetParkingDaysClosed(UNIT_ID));
        }

        [HttpGet("getParkingDayClosed/{dayClosedId}")]
        public async Task<ActionResult<ParkingDaysClosed>> GetParkingDayClosed(int dayClosedId)
        {
            return Ok(await _parkingDaysClosed.GetParkingDayClosed(dayClosedId));
        }

        [HttpGet("getParkingDaysClosedDs")]
        public async Task<ActionResult<ParkingDaysClosed>> GetParkingDaysClosed([FromQuery] DataSourceLoadOptions loadOption)
        {
            return Ok(DataSourceLoader.Load(await _parkingDaysClosed.GetParkingDaysClosed(UNIT_ID), loadOption));
        }

        [HttpPost("addDaysClsoed")]
        public async Task<ActionResult<ParkingDaysClosed>> AddDaysClsoed([FromBody] List<ParkingDaysClosed> parkingDayClosed)
        {
            return Ok(await _parkingDaysClosed.AddDaysClsoed(UNIT_ID, parkingDayClosed));
        }

        [HttpPut("editDayClosed")]
        public async Task<ActionResult<ParkingDaysClosed>> EditDayClosed([FromBody] ParkingDaysClosed parkingDayClosed)
        {
            return Ok(await _parkingDaysClosed.EditDayClosed(parkingDayClosed));
        }


        [HttpDelete("deleteDayClosed/{dayClosedId}")]
        public async Task<ActionResult<bool>> DeleteDayClosed(int dayClosedId)
        {
            return Ok(await _parkingDaysClosed.DeleteDayClosed(dayClosedId));
        }

        [HttpDelete("deleteDaysClosed/{daysClosedIdStr}")]
        public async Task<ActionResult<bool>> DeleteDaysClosed(string daysClosedIdStr)
        {
            var daysClosedId = JsonConvert.DeserializeObject<List<int>>(daysClosedIdStr);
            return Ok(await _parkingDaysClosed.DeleteDaysClosed(daysClosedId));
        }
    }
}
