using Context.Repository;
using System.Web.Http;

namespace TNTParking_Backend.Interfaces
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<Subscription>> GetSubscriptions(int unitId);
        Task<Subscription> GetSubscription(int subscriptionId);
        Task<Subscription> AddSubscription(int unitId, Subscription subscription);
        Task<Subscription> EditSubscription(Subscription subscription);
        Task<bool> DeleteSubscription(int subscriptionId);
    }
}
