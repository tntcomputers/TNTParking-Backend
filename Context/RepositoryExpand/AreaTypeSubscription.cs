using Context.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public class AreaTypeSubscription
    {
        public int Id { get; set; }

        public int? IdAreaType { get; set; }

        public int? IdSubscription { get; set; }


        public AreaTypeSubscription()
        {
            
        }

        public AreaTypeSubscription(ParkingAreaTypeSubscription areaTypeSubscription)
        {
            this.Id = areaTypeSubscription.Id;
            this.IdAreaType = areaTypeSubscription.IdAreaType;
            this.IdSubscription = areaTypeSubscription.IdSubscription;
        }
    }
    public partial class ParkingAreaTypeSubscription
    {
        public void Map(AreaTypeSubscription areaTypeSubscription)
        {
            this.Id = areaTypeSubscription.Id;
            this.IdAreaType = areaTypeSubscription.IdAreaType;
            this.IdSubscription = areaTypeSubscription.IdSubscription;
        }
    }
}
