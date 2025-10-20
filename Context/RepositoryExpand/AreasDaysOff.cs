using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public class AreasDaysOff
    {
        public int Id { get; set; }

        public int UnitId { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string StartDateStr { get; set; }
        public string EndDateStr { get; set; }
        public int? IdAreaType { get; set; }

        public ParkingArea? ParkingArea { get; set; }

        public AreasDaysOff()
        {

        }

        public AreasDaysOff(ParkingAreasDaysOff AreasDaysOff)
        {
            this.Id = AreasDaysOff.Id;
            this.UnitId = AreasDaysOff.UnitId;
            this.StartDate = AreasDaysOff.StartDate;
            this.EndDate = AreasDaysOff.EndDate;
            this.StartDateStr = AreasDaysOff.StartDate.ToString("dd.MM.yyyy HH:mm");
            this.EndDateStr = AreasDaysOff.EndDate.ToString("dd.MM.yyyy HH:mm");
            this.IdAreaType = AreasDaysOff.IdAreaType;
        }
    }
    public partial class ParkingAreasDaysOff
    {
        public void Map(AreasDaysOff AreasDaysOff)
        {
            this.Id = AreasDaysOff.Id;
            this.UnitId = AreasDaysOff.UnitId;
            this.StartDate = Convert.ToDateTime(AreasDaysOff.StartDateStr, CultureInfo.InvariantCulture); //AreasDaysOff.StartDate;
            this.EndDate = Convert.ToDateTime(AreasDaysOff.EndDateStr, CultureInfo.InvariantCulture); //AreasDaysOff.EndDate;
            this.IdAreaType = AreasDaysOff.IdAreaType;
        }
    }
}
