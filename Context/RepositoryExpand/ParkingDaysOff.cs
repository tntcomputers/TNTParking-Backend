using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public class ParkingDaysOff
    {
        public int Id { get; set; }

        public int UnitId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartDateStr { get; set; }
        public string EndDateStr { get; set; }
        public int? IdAreaType { get; set; }

        public ParkingArea? ParkingArea { get; set; }

        public ParkingDaysOff()
        {

        }

        public ParkingDaysOff(ParkingParkingDaysOff parkingDaysOff)
        {
            this.Id = parkingDaysOff.Id;
            this.UnitId = parkingDaysOff.UnitId;
            this.StartDate = parkingDaysOff.StartDate;
            this.EndDate = parkingDaysOff.EndDate;
            this.StartDateStr = parkingDaysOff.StartDate.ToString("dd.MM.yyyy HH:mm");
            this.EndDateStr = parkingDaysOff.EndDate.ToString("dd.MM.yyyy HH:mm");
            this.IdAreaType = parkingDaysOff.IdAreaType;
        }
    }
    public partial class ParkingParkingDaysOff
    {
        public void Map(ParkingDaysOff parkingDaysOff)
        {
            this.Id = parkingDaysOff.Id;
            this.UnitId = parkingDaysOff.UnitId;
            this.StartDate = Convert.ToDateTime(parkingDaysOff.StartDateStr, CultureInfo.InvariantCulture); //parkingDaysOff.StartDate;
            this.EndDate = Convert.ToDateTime(parkingDaysOff.EndDateStr, CultureInfo.InvariantCulture); //parkingDaysOff.EndDate;
            this.IdAreaType = parkingDaysOff.IdAreaType;
        }
    }
}
