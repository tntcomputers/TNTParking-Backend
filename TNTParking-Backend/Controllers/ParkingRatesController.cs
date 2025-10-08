using Context.Repository;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TNT_ANL_Backend.Services;
using TNTParking_Backend.Interfaces;
using TNTParking_Backend.Services;

namespace TNTParking_Backend.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MainCorsPolicy")]
    [ApiController]
    [LogActionFilter]
    public class ParkingRatesController : BaseController
    {
        private readonly IParkingRatesService _parkingRatesService;

        public ParkingRatesController(IParkingRatesService parkingRatesService)
        {
            _parkingRatesService = parkingRatesService;
        }

        [HttpGet("getParkingIntervals")]
        public async Task<ActionResult<List<ParkingIntervals>>> GetParkingIntervals()
        {
            return Ok(await _parkingRatesService.GetParkingIntervals(UNIT_ID));
        }

        [HttpGet("getParkingIntervalsDs")]
        public async Task<ActionResult<List<ParkingIntervals>>> GetParkingIntervals([FromQuery] DataSourceLoadOptions loadOptions)
        {
            return Ok(DataSourceLoader.Load(await _parkingRatesService.GetParkingIntervals(UNIT_ID), loadOptions));
        }

        [HttpGet("getParkingInterval/{intervalId}")]
        public async Task<ActionResult<ParkingIntervals>> GetParkingInterval(int intervalId)
        {
            return Ok(await _parkingRatesService.GetParkingInterval(intervalId));
        }

        [HttpPost("addParkingInterval")]
        public async Task<ActionResult<ParkingIntervals>> AddParkingInterval([FromBody] ParkingIntervals interval)
        {
            return Ok(await _parkingRatesService.AddParkingInterval(UNIT_ID, interval));
        }

        [HttpPut("editParkingInterval")]
        public async Task<ActionResult<ParkingIntervals>> EditParkingInterval([FromBody] ParkingIntervals interval)
        {
            return Ok(await _parkingRatesService.EditParkingInterval(interval));
        }

        [HttpDelete("deleteParkingInterval/{intervalId}")]
        public async Task<ActionResult<ParkingIntervals>> DeleteParkingInterval(int intervalId)
        {
            return Ok(await _parkingRatesService.DeleteParkingInterval(intervalId));
        }
    }
}
