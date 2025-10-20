using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using TNT_ANL_Backend.Services;

namespace TNTParking_Backend.Controllers
{

    [Route("api/[controller]")]
    [EnableCors("MainCorsPolicy")]
    [ApiController]
    [LogActionFilter]
    public class ApplePayController : BaseController
    {
        private readonly IConfiguration _cfg;
        public ApplePayController(IConfiguration cfg) { _cfg = cfg; }

        [HttpPost("validate-merchant")]
        public async Task<IActionResult> ValidateMerchant([FromBody] MerchantValidationDto dto)
        {

            var pfxPath = _cfg["ApplePay:MerchantIdentityPfxPath"];
            var pfxPwd = _cfg["ApplePay:MerchantIdentityPfxPassword"];
            var cert = new X509Certificate2(pfxPath, pfxPwd, X509KeyStorageFlags.MachineKeySet);

            var handler = new HttpClientHandler();
            handler.ClientCertificates.Add(cert);

            using var http = new HttpClient(handler);

            var url = _cfg.GetValue<bool>("ApplePay:UseSandbox", true)
              ? "https://apple-pay-gateway-cert.apple.com/paymentservices/startSession"
              : "https://apple-pay-gateway.apple.com/paymentservices/startSession";

            var body = new
            {
                merchantIdentifier = _cfg["ApplePay:MerchantIdentifier"],
                domainName = new Uri(dto.Origin).Host,
                displayName = _cfg["ApplePay:DisplayName"] ?? "SMARTAgriculture"
            };

            var resp = await http.PostAsJsonAsync(url, body);
            var json = await resp.Content.ReadAsStringAsync();
            if (!resp.IsSuccessStatusCode) return BadRequest(json);

            return Content(json, "application/json");
        }

        public class MerchantValidationDto
        {
            public string ValidationUrl { get; set; } = "";
            public string Origin { get; set; } = "";
        }

        //[HttpPost("authorize")]
        //public async Task<IActionResult> Authorize([FromBody] AppleAuthorizeDto dto)
        //{
        //    //var ok = await MyPsp.ChargeApplePayAsync(dto.Token, dto.Amount, dto.Currency);
        //    //return Ok(new { success = ok });
        //}

        public class AppleAuthorizeDto
        {
            public object Token { get; set; } = default!;
            public string Amount { get; set; } = "0.00";
            public string Currency { get; set; } = "RON";
        }
    }
}
