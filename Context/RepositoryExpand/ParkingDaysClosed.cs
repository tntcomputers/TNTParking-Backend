using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public class ParkingDaysClosed
    {
        public int Id { get; set; }

        public int UnitId { get; set; }

        public int IdArea { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string StartDateStr { get; set; }
        public string EndDateStr { get; set; }

        public Area? Area { get; set; }

        public ParkingDaysClosed()
        {

        }

        public ParkingDaysClosed(ParkingParkingDaysClosed parkingDaysClosed)
        {
            this.Id = parkingDaysClosed.Id;
            this.UnitId = parkingDaysClosed.UnitId;
            this.StartDate = parkingDaysClosed.StartDate;
            this.EndDate = parkingDaysClosed.EndDate;
            this.StartDateStr = parkingDaysClosed.StartDate.ToString("dd.MM.yyyy HH:mm");
            this.EndDateStr = parkingDaysClosed.EndDate.ToString("dd.MM.yyyy HH:mm");
            this.IdArea = parkingDaysClosed.IdArea;
        }
    }

    public partial class ParkingParkingDaysClosed
    {
        public void Map(ParkingDaysClosed parkingDayClosed)
        {
            this.Id = parkingDayClosed.Id;
            this.UnitId = parkingDayClosed.UnitId;
            this.StartDate = Convert.ToDateTime(parkingDayClosed.StartDateStr, CultureInfo.InvariantCulture);
            this.EndDate = Convert.ToDateTime(parkingDayClosed.EndDateStr, CultureInfo.InvariantCulture);
            this.IdArea = parkingDayClosed.IdArea;
        }
    }
}
