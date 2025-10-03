using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class ParkingArea
{
    public int Id { get; set; }

    public int IdType { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string? PolygonCoords { get; set; }

    public int? UnitId { get; set; }

    public int? ParkingSpaces { get; set; }

    public virtual ParkingAreaType IdTypeNavigation { get; set; } = null!;

    public virtual ICollection<ParkingParkingSpace> ParkingParkingSpaces { get; set; } = new List<ParkingParkingSpace>();
}
