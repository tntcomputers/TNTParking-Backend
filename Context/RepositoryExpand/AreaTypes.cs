using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public class AreaTypes
    {
        public int Id { get; set; }

        public int UnitId { get; set; }

        public string Name { get; set; }

        public AreaTypes()
        {

        }

        public AreaTypes(ParkingAreaType areaType)
        {
            this.Id = areaType.Id;
            this.UnitId = areaType.UnitId;
            this.Name = areaType.Name;
        }
    }
    public partial class ParkingAreaType
    {
        public void Map(AreaTypes areaType)
        {
            this.Id = areaType.Id;
            this.UnitId = areaType.UnitId;
            this.Name = areaType.Name;
        }
    }
}
