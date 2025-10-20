using Context.Repository;
using Microsoft.EntityFrameworkCore;
using QRCoder;
using System.Drawing;
using TNTParking_Backend.Interfaces;
using TNTParking_Backend.Models;

namespace TNTParking_Backend.Services
{
    public class ParkingPaymentService : IParkingPaymentService
    {

        private readonly TntparkingContext _appContext;

        public ParkingPaymentService(TntparkingContext appContext)
        {
            _appContext = appContext;
        }


        public async Task<ParkingPaymentTariff> GetParkingPaymentTariff(string unitIdentifier, int areaTypeId, DateTime date)
        {
            string dayOfWeek = DateTimeToCode(date);
            var hour = date.ToString("HH:mm");

            var unit = _appContext.UnitsViews.FirstOrDefault(x => x.Uniqueidentifier == unitIdentifier);
            var intervals = await _appContext.ParkingIntervals
                          .Where(i => i.UnitId == unit.Id)
                          .Where(i => i.ParkingAreaIntervals.Any(ai => ai.IdAreaType == areaTypeId))
                          .AsNoTracking()
                          .ToListAsync();

            var validInterval = intervals.Where(i =>
            {
                var days = ConvertDaysFromInterval(i.DaysOfWeek);
                if (!days.Contains(dayOfWeek)) return false;

                var from = TimeSpan.Parse(i.FromHour ?? "00:00");
                var to = TimeSpan.Parse(i.ToHour ?? "23:59");
                var t = TimeSpan.Parse(hour);

                return from <= to ? (t >= from && t <= to) : (t >= from || t <= to);
            }).FirstOrDefault();

            if (validInterval == null) return new ParkingPaymentTariff();

            var rate = await _appContext.ParkingRates
              .Where(r => r.UnitId == unit.Id && r.IdAreaType == areaTypeId && validInterval.Id == r.IdInterval)
              .Where(r => (r.FromDate.Value.Date <= date.Date) &&
                          (r.ToDate.Value.Date >= date.Date))
              .OrderByDescending(r => r.FromDate)
              .FirstOrDefaultAsync();

            return new ParkingPaymentTariff(rate.Id, rate.Price ?? 0, validInterval.FromHour, validInterval.ToHour);
        }

        private static HashSet<string> ConvertDaysFromInterval(string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return new HashSet<string>();
            try
            {
                var arr = System.Text.Json.JsonSerializer.Deserialize<string[]>(json.Replace("\"\"", "\""));
                return new HashSet<string>(arr ?? Array.Empty<string>());
            }
            catch
            {
                return new HashSet<string>(json.Split(',').Select(s => s.Trim()));
            }
        }

        private static string DateTimeToCode(DateTime dt) => dt.DayOfWeek switch
        {
            DayOfWeek.Monday => "Mo",
            DayOfWeek.Tuesday => "Tu",
            DayOfWeek.Wednesday => "We",
            DayOfWeek.Thursday => "Th",
            DayOfWeek.Friday => "Fr",
            DayOfWeek.Saturday => "Sat",
            DayOfWeek.Sunday => "Su",
            _ => "Mo"
        };

        public async Task<Subscription> GetSubscription(string unitIdentifier, int areaTypeId, DateTime data)
        {
            var unitId = _appContext.UnitsViews.FirstOrDefault(x => x.Uniqueidentifier == unitIdentifier)?.Id;
            var sub = _appContext.ParkingSubscriptions.Include(x => x.ParkingAreaTypeSubscriptions).AsNoTracking()
                    .Where(s => s.UnitId == unitId && s.FromDate.Value.Date <= data.Date && s.ToDate.Value.Date >= data.Date)
                    .Where(s => s.ParkingAreaTypeSubscriptions.Any(a => a.IdAreaType == areaTypeId)).FirstOrDefault();

            if (sub != null)
            {
                return new Subscription(sub);
            }

            return new Subscription();
        }

        public async Task<KeyValueString> GetQrCodeToPay(int unitId, string url, int areaTypeId, int areaId)
        {
            var unitIdentifier = _appContext.UnitsViews.FirstOrDefault(x => x.Id == unitId);
            if (unitIdentifier != null)
            {
                var qrCodeData = $@"{url}?areaTypeId={areaTypeId}&areaId={areaId}&uidf={unitIdentifier.Uniqueidentifier}";
                var qrCodeBase64 = GenerateQrCodeBase64(qrCodeData);
                return new KeyValueString(1, qrCodeBase64);
            }
            return new KeyValueString();
        }

        private string GenerateQrCodeBase64(string text)
        {
            using var generator = new QRCodeGenerator();
            using var data = generator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

            var base64 = new Base64QRCode(data).GetGraphic(
                                        20,
                                        Color.Black,
                                        Color.White,
                                        drawQuietZones: false
                                    );
            return base64;
        }

        public async Task<ParkingPayment> AddParkingPayment(string unitIdentifier, ParkingPayment parkingPayment)
        {
            try
            {
                var unitId = _appContext.UnitsViews.FirstOrDefault(x => x.Uniqueidentifier == unitIdentifier).Id;
                parkingPayment.UnitId = unitId ?? 0;
                var pPayment = new ParkingParkingPayment();
                pPayment.Map(parkingPayment);

                _appContext.Entry(pPayment).State = EntityState.Added;
                _appContext.SaveChanges();

                return new ParkingPayment(pPayment);
            }
            catch (Exception e)
            {
                return new ParkingPayment();
            }
        }

        public async Task<KeyValueString> CheckParkingStatus(string unitIdentifier, DateTime parkingDate, int areaTypeId, int? areaId)
        {
            var unitId = _appContext.UnitsViews.FirstOrDefault(x => x.Uniqueidentifier == unitIdentifier).Id;
            var areaDayOff = _appContext.ParkingAreasDaysOffs.Where(x => x.UnitId == unitId && parkingDate >= x.StartDate && parkingDate <= x.EndDate && x.IdAreaType == areaTypeId).FirstOrDefault();

            if (areaId != null)
            {
                if (areaId > 0)
                {
                    var parkingDayClosed = _appContext.ParkingParkingDaysCloseds.Where(x => x.UnitId == unitId && x.IdArea == areaId && parkingDate >= x.StartDate && parkingDate <= x.EndDate).FirstOrDefault();
                    if (parkingDayClosed != null)
                    {
                        return new KeyValueString(1, "CLOSED");
                    }
                }
            }
            if (areaDayOff != null)
            {
                return new KeyValueString(1, "DAY_OFF");
            }
            return new KeyValueString(1, "OPEN");
        }
    }
}
