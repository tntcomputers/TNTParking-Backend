using Context.Repository;
using DevExpress.Utils.Filtering.Internal;
using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;
using TNTParking_Backend.Interfaces;

namespace TNTParking_Backend.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly TntparkingContext _appContext;

        public SubscriptionService(TntparkingContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Subscription> AddSubscription(int unitId, Subscription subscription)
        {
            try
            {
                var parkingSubscription = new ParkingSubscription();
                subscription.UnitId = unitId;
                parkingSubscription.Map(subscription);
                _appContext.Entry(parkingSubscription).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _appContext.SaveChanges();

                subscription.AreaTypeSubscriptions.ForEach(s =>
                {
                    s.Id = 0;
                    s.IdSubscription = parkingSubscription.Id;
                    var parkingAreaTypeSubs = new ParkingAreaTypeSubscription();
                    parkingAreaTypeSubs.Map(s);
                    _appContext.Entry(parkingAreaTypeSubs).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                });
                _appContext.SaveChanges();

                return new Subscription(parkingSubscription);
            }
            catch (Exception e)
            {
                return new Subscription();
            }
        }

        public async Task<bool> DeleteSubscription(int subscriptionId)
        {
            try
            {
                var areaTypeSubscriptions = _appContext.ParkingAreaTypeSubscriptions.Where(x => x.IdSubscription == subscriptionId).ToList();
                _appContext.RemoveRange(areaTypeSubscriptions);
                _appContext.SaveChanges();

                var subs = _appContext.ParkingSubscriptions.Where(x => x.Id == subscriptionId).FirstOrDefault();
                if (subs != null)
                {
                    _appContext.Entry(subs).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    _appContext.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Subscription> EditSubscription(Subscription subscription)
        {
            try
            {
                var parkingSubscription = new ParkingSubscription();
                parkingSubscription.Map(subscription);
                _appContext.Entry(parkingSubscription).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                var areaTypeSubscriptions = _appContext.ParkingAreaTypeSubscriptions.Where(x => x.IdSubscription == subscription.Id).AsNoTracking();
                var areaTypeSubscriptionsToRemove = areaTypeSubscriptions.Where(x => !subscription.AreaTypeSubscriptions.Select(y => y.Id).Contains(x.Id));
                var areaTypeSubscriptionsToAdd = subscription.AreaTypeSubscriptions.Where(x => x.Id < 0);

                _appContext.RemoveRange(areaTypeSubscriptionsToRemove);

                if (areaTypeSubscriptionsToAdd.Count() > 0)
                {
                    areaTypeSubscriptionsToAdd.ForEach(x =>
                    {
                        var areaTypeSubscription = new ParkingAreaTypeSubscription();
                        x.Id = 0;
                        areaTypeSubscription.Map(x);
                        _appContext.Entry(areaTypeSubscription).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                    });
                }

                _appContext.SaveChanges();

                return new Subscription(parkingSubscription);
            }
            catch (Exception e)
            {
                return new Subscription();
            }
        }

        public async Task<Subscription> GetSubscription(int subscriptionId)
        {
            return _appContext.ParkingSubscriptions.Where(x => x.Id == subscriptionId).Include(x => x.ParkingAreaTypeSubscriptions).Select(x => new Subscription(x)).FirstOrDefault() ?? new Subscription();
        }

        public async Task<IEnumerable<Subscription>> GetSubscriptions(int unitId)
        {
            return _appContext.ParkingSubscriptions.Where(x => x.UnitId == unitId).Include(x => x.ParkingAreaTypeSubscriptions).Select(x => new Subscription(x)).ToList();
        }
    }
}
