using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public class ParkingSpaces
    {
        public int Id { get; set; }

        public int IdArea { get; set; }

        public string? PolygonCoords { get; set; }

        public ParkingSpaces()
        {

        }

        public ParkingSpaces(ParkingParkingSpace parkingSpace)
        {
            this.Id = parkingSpace.Id;
            this.IdArea = parkingSpace.IdArea;
            this.PolygonCoords = parkingSpace.PolygonCoords;
        }
    }
    public partial class ParkingParkingSpace
    {
        public void Map(ParkingSpaces parkingSpace)
        {
            this.Id = parkingSpace.Id;
            this.IdArea = parkingSpace.IdArea;
            this.PolygonCoords = parkingSpace.PolygonCoords;
        }
    }
}
