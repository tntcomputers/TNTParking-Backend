using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class ParkingInterval
{
    public int Id { get; set; }

    public string? DaysOfWeek { get; set; }

    public string? FromHour { get; set; }

    public string? ToHour { get; set; }

    public int? UnitId { get; set; }

    public virtual ICollection<ParkingAreaInterval> ParkingAreaIntervals { get; set; } = new List<ParkingAreaInterval>();

    public virtual ICollection<ParkingRate> ParkingRates { get; set; } = new List<ParkingRate>();
}
