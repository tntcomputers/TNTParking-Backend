using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class ParkingAreasDaysOff
{
    public int Id { get; set; }

    public int UnitId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int? IdAreaType { get; set; }

    public virtual ParkingAreaType? IdAreaTypeNavigation { get; set; }
}
