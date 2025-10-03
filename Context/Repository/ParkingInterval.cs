using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class ParkingInterval
{
    public int Id { get; set; }

    public string? DayOfWeek { get; set; }

    public string? FromHour { get; set; }

    public string? ToHour { get; set; }

    public int? UnitId { get; set; }

    public virtual ICollection<ParkingAreaInterval> ParkingAreaIntervals { get; set; } = new List<ParkingAreaInterval>();
}
