using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public class Area
    {
        public int Id { get; set; }
        public int IdType { get; set; }

        public string Name { get; set; }

        public string Address { get; set; } = null!;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
        public string PolygonCoords { get; set; }

        public int? UnitId { get; set; }
        public int? ParkingSpaces { get; set; }

        public List<AreaInterval>? ParkingAreaIntervals { get; set; }
        public List<ParkingSpaces>? ParkingSpacesList { get; set; }
        public List<AreasDaysOff>? AreasDaysOff { get; set; }
        //public AreaTypes? AreaType { get; set; }

        public Area()
        {

        }

        public Area(ParkingArea area)
        {
            this.Id = area.Id;
            this.IdType = area.IdType;
            this.UnitId = area.UnitId;
            this.Name = area.Name;
            this.Address = area.Address;
            this.Latitude = area.Latitude;
            this.Longitude = area.Longitude;
            this.PolygonCoords = area.PolygonCoords;
            this.ParkingSpaces = area.ParkingSpaces;
            this.ParkingAreaIntervals = new List<AreaInterval>();
            this.ParkingSpacesList = area.ParkingParkingSpaces != null ? area.ParkingParkingSpaces.Select(x => new ParkingSpaces(x)).ToList() : new List<ParkingSpaces>();
            //this.AreaType = area.IdTypeNavigation != null ? new AreaTypes(area.IdTypeNavigation) : new AreaTypes();
        }
    }

    public partial class ParkingArea
    {
        public void Map(Area area)
        {
            this.Id = area.Id;
            this.IdType = area.IdType;
            this.UnitId = area.UnitId;
            this.Name = area.Name;
            this.Address = area.Address;
            this.Latitude = area.Latitude;
            this.Longitude = area.Longitude;
            this.PolygonCoords = area.PolygonCoords;
            this.ParkingSpaces = area.ParkingSpaces;
        }
    }
}
