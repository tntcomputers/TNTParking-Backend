using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class ParkingParkingSpace
{
    public int Id { get; set; }

    public int IdArea { get; set; }

    public string PolygonCoords { get; set; } = null!;

    public virtual ParkingArea IdAreaNavigation { get; set; } = null!;
}
