using Context.Repository;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TNT_ANL_Backend.Services;
using TNTParking_Backend.Interfaces;
using TNTParking_Backend.Models;
using TNTParking_Backend.Services;

namespace TNTParking_Backend.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MainCorsPolicy")]
    [ApiController]
    [LogActionFilter]
    public class ParkingPaymentController : BaseController
    {
        private readonly IParkingPaymentService _parkingPayment;

        public ParkingPaymentController(IParkingPaymentService parkingPayment)
        {
            _parkingPayment = parkingPayment;
        }

        [HttpGet("getParkingPaymentTariff/{unitIdentifier}/{areaTypeId}/{dateString}")]
        public async Task<ActionResult<ParkingPaymentTariff>> GetParkingPaymentTariff(string unitIdentifier, int areaTypeId, string dateString)
        {
            var date = Convert.ToDateTime(dateString);
            return Ok(await _parkingPayment.GetParkingPaymentTariff(unitIdentifier, areaTypeId, date));
        }

        [HttpGet("getEligibleSubscription/{unitIdentifier}/{areaTypeId}/{dateString}")]
        public async Task<ActionResult<Subscription?>> GetSubscription(string unitIdentifier, int areaTypeId, string dateString)
        {
            var date = Convert.ToDateTime(dateString);
            return Ok(await _parkingPayment.GetSubscription(unitIdentifier, areaTypeId, date));
        }

        [HttpGet("getQrCodeToPay")]
        public async Task<ActionResult<KeyValueString?>> GetQrCodeToPay([FromQuery] string url, [FromQuery] int areaTypeId, [FromQuery] int areaId)
        {
            return Ok(await _parkingPayment.GetQrCodeToPay(UNIT_ID, url, areaTypeId, areaId));
        }

        [HttpPost("addParkingPayment")]
        public async Task<ActionResult<ParkingPayment?>> AddParkingPayment([FromQuery] string unitIdentifier, [FromBody] ParkingPayment parkingPayment)
        {
            return Ok(await _parkingPayment.AddParkingPayment(unitIdentifier, parkingPayment));
        }

        [HttpGet("checkParkingStatus")]
        public async Task<ActionResult<KeyValueString?>> CheckParkingStatus([FromQuery] string unitIdentifier, [FromQuery] string parkingDateString, [FromQuery] int areaTypeid, [FromQuery] int? areaId)
        {
            var parkingDate = Convert.ToDateTime(parkingDateString);
            return Ok(await _parkingPayment.CheckParkingStatus(unitIdentifier, parkingDate, areaTypeid, areaId));
        }

        [HttpGet("getParkingPayments")]
        public async Task<ActionResult<ParkingPayment>> GetParkingPayments([FromQuery] DataSourceLoadOptions loadOptions)
        {
            return Ok(DataSourceLoader.Load(await _parkingPayment.GetParkingPayments(UNIT_ID), loadOptions));
        }
    }
}
