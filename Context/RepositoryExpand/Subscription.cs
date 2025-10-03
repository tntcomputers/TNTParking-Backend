using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public class Subscription
    {
        public int Id { get; set; }

        public int IdAreaType { get; set; }

        public decimal? AllDay { get; set; }

        public decimal? AllWeek { get; set; }

        public decimal? AllMonth { get; set; }

        public decimal? AllYear { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public decimal? CustomPrice { get; set; }
        public int? UnitId { get; set; }
        public Subscription()
        {

        }

        public Subscription(ParkingSubscription subscription)
        {
            this.Id = subscription.Id;
            this.UnitId = subscription.UnitId;
            this.IdAreaType = subscription.IdAreaType;
            this.AllDay = subscription.AllDay;
            this.AllWeek = subscription.AllWeek;
            this.AllMonth = subscription.AllMonth;
            this.AllYear = subscription.AllYear;
            this.FromDate = subscription.FromDate;
            this.ToDate = subscription.ToDate;
            this.CustomPrice = subscription.CustomPrice;
        }
    }

    public partial class ParkingSubscription
    {
        public void Map(Subscription subscription)
        {
            this.Id = subscription.Id;
            this.UnitId = subscription.UnitId;
            this.IdAreaType = subscription.IdAreaType;
            this.AllDay = subscription.AllDay;
            this.AllWeek = subscription.AllWeek;
            this.AllMonth = subscription.AllMonth;
            this.AllYear = subscription.AllYear;
            this.FromDate = subscription.FromDate;
            this.ToDate = subscription.ToDate;
            this.CustomPrice = subscription.CustomPrice;
        }
    }
}
