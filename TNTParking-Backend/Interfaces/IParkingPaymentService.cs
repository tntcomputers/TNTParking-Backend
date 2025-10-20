using Context.Repository;
using TNTParking_Backend.Models;

namespace TNTParking_Backend.Interfaces
{
    public interface IParkingPaymentService
    {
        Task<ParkingPaymentTariff> GetParkingPaymentTariff(string unitIdentifier, int areaTypeId, DateTime date);
        Task<Subscription> GetSubscription(string unitIdentifier, int areaTypeId, DateTime data);
        Task<KeyValueString> GetQrCodeToPay(int unitId, string url, int areaTypeId, int areaId);
        Task<KeyValueString> CheckParkingStatus(string unitIdentifier, DateTime parkingDate, int areaTypeId, int? areaId);
        Task<ParkingPayment> AddParkingPayment(string unitIdentifier, ParkingPayment parkingPayment);
    }
}
