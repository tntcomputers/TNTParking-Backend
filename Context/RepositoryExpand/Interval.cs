using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public class Interval
    {
        public int Id { get; set; }

        public string? DaysOfWeek { get; set; }

        public string? FromHour { get; set; }

        public string? ToHour { get; set; }
        public int? UnitId { get; set; }

        public Interval()
        {

        }
        public Interval(ParkingInterval parkingInterval)
        {
            this.Id = parkingInterval.Id;
            this.UnitId = parkingInterval.UnitId;
            this.DaysOfWeek = parkingInterval.DaysOfWeek;
            this.FromHour = parkingInterval.FromHour;
            this.ToHour = parkingInterval.ToHour;
        }
    }

    public partial class ParkingInterval
    {
        public void Map(Interval parkingInterval)
        {
            this.Id = parkingInterval.Id;
            this.UnitId = parkingInterval.UnitId;
            this.DaysOfWeek = parkingInterval.DaysOfWeek;
            this.FromHour = parkingInterval.FromHour;
            this.ToHour = parkingInterval.ToHour;
        }
    }
}
