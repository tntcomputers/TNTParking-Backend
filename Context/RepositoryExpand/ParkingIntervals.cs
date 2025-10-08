using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public class ParkingIntervals
    {
        public int Id { get; set; }

        public string? DaysOfWeek { get; set; }

        public string? FromHour { get; set; }

        public string? ToHour { get; set; }

        public int? UnitId { get; set; }

        public List<AreaInterval> AreaIntervals { get; set; }

        public List<Rates> Rates { get; set; }

        public ParkingIntervals()
        {

        }

        public ParkingIntervals(ParkingInterval interval)
        {
            this.Id = interval.Id;
            this.DaysOfWeek = interval.DaysOfWeek;
            this.FromHour = interval.FromHour;
            this.ToHour = interval.ToHour;
            this.UnitId = interval.UnitId;
            this.AreaIntervals = interval.ParkingAreaIntervals != null ? interval.ParkingAreaIntervals.Select(x => new AreaInterval(x)).ToList() : new List<AreaInterval>();
            this.Rates = interval.ParkingRates != null ? interval.ParkingRates.Select(x => new Rates(x)).ToList() : new List<Rates>();
        }
    }
    public partial class ParkingInterval
    {
        public void Map(ParkingIntervals interval)
        {
            this.Id = interval.Id;
            this.DaysOfWeek = interval.DaysOfWeek;
            this.FromHour = interval.FromHour;
            this.ToHour = interval.ToHour;
            this.UnitId = interval.UnitId;
        }
    }
}
