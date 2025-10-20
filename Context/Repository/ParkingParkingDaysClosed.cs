using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class ParkingParkingDaysClosed
{
    public int Id { get; set; }

    public int UnitId { get; set; }

    public int IdArea { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public virtual ParkingArea IdAreaNavigation { get; set; } = null!;
}
