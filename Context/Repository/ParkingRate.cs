using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class ParkingRate
{
    public int Id { get; set; }

    public int IdAreaType { get; set; }

    public int IdInterval { get; set; }

    public decimal? Price { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public int? UnitId { get; set; }

    public virtual ParkingAreaType IdAreaTypeNavigation { get; set; } = null!;

    public virtual ParkingInterval IdIntervalNavigation { get; set; } = null!;
}
