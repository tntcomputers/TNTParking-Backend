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
    public class SubscriptionController : BaseController
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet("getSubscriptions")]
        public async Task<ActionResult<Subscription>> GetSubscriptions()
        {
            return Ok(await _subscriptionService.GetSubscriptions(UNIT_ID));
        }

        [HttpGet("getParkingIntervalsDs")]
        public async Task<ActionResult<Subscription>> GetSubscriptions([FromQuery] DataSourceLoadOptions loadOptions)
        {
            return Ok(DataSourceLoader.Load(await _subscriptionService.GetSubscriptions(UNIT_ID), loadOptions));
        }

        [HttpGet("getSubscription/{subscriptionId}")]
        public async Task<ActionResult<Subscription>> GetSubscription(int subscriptionId)
        {
            return Ok(await _subscriptionService.GetSubscription(subscriptionId));
        }

        [HttpPost("addSubscription")]
        public async Task<ActionResult<Subscription>> AddSubscription([FromBody] Subscription subscription)
        {
            return Ok(await _subscriptionService.AddSubscription(UNIT_ID, subscription));
        }

        [HttpPut("editSubscription")]
        public async Task<ActionResult<Subscription>> EditSubscription([FromBody] Subscription subscription)
        {
            return Ok(await _subscriptionService.EditSubscription(subscription));
        }

        [HttpDelete("deleteSubscription/{subscriptionId}")]
        public async Task<ActionResult<Subscription>> DeleteSubscription(int subscriptionId)
        {
            return Ok(await _subscriptionService.DeleteSubscription(subscriptionId));
        }
    }
}
