using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class ParkingAreaType
{
    public int Id { get; set; }

    public int UnitId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ParkingAreaInterval> ParkingAreaIntervals { get; set; } = new List<ParkingAreaInterval>();

    public virtual ICollection<ParkingAreaTypeSubscription> ParkingAreaTypeSubscriptions { get; set; } = new List<ParkingAreaTypeSubscription>();

    public virtual ICollection<ParkingArea> ParkingAreas { get; set; } = new List<ParkingArea>();

    public virtual ICollection<ParkingParkingDaysOff> ParkingParkingDaysOffs { get; set; } = new List<ParkingParkingDaysOff>();

    public virtual ICollection<ParkingRate> ParkingRates { get; set; } = new List<ParkingRate>();
}
