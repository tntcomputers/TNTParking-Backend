using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public class AreaInterval
    {
        public int Id { get; set; }

        public int IdAreaType { get; set; }

        public int IdInterval { get; set; }
        public int? UnitId { get; set; }

        public Area? Area { get; set; }

        public Interval? Interval { get; set; }

        public AreaInterval()
        {

        }

        public AreaInterval(ParkingAreaInterval areaInterval)
        {
            this.Id = areaInterval.Id;
            this.UnitId = areaInterval.UnitId;
            this.IdAreaType = areaInterval.IdAreaType;
            this.IdInterval = areaInterval.IdInterval;
        }
    }

    public partial class ParkingAreaInterval
    {
        public void Map(AreaInterval areaInterval)
        {
            this.Id = areaInterval.Id;
            this.UnitId = areaInterval.UnitId;
            this.IdAreaType = areaInterval.IdAreaType;
            this.IdInterval = areaInterval.IdInterval;
        }
    }
}
