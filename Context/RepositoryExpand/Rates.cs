using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public class Rates
    {
        public int Id { get; set; }
        public int? UnitId { get; set; }
        public int IdAreaType { get; set; }

        public int IdInterval { get; set; }

        public decimal? Price { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public Rates()
        {

        }

        public Rates(ParkingRate parkingRates)
        {
            this.Id = parkingRates.Id;
            this.UnitId = parkingRates.UnitId;
            this.IdAreaType = parkingRates.IdAreaType;
            this.IdInterval = parkingRates.IdInterval;
            this.Price = parkingRates.Price;
            this.FromDate = parkingRates.FromDate;
            this.ToDate = parkingRates.ToDate;
        }
    }

    public partial class ParkingRate
    {
        public void Map(Rates parkingRates)
        {
            this.Id = parkingRates.Id;
            this.UnitId = parkingRates.UnitId;
            this.IdAreaType = parkingRates.IdAreaType;
            this.IdInterval = parkingRates.IdInterval;
            this.Price = parkingRates.Price;
            this.FromDate = parkingRates.FromDate;
            this.ToDate = parkingRates.ToDate;
        }
    }
}
