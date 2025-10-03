using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class ParkingSubscription
{
    public int Id { get; set; }

    public int IdAreaType { get; set; }

    public decimal? AllDay { get; set; }

    public decimal? AllWeek { get; set; }

    public decimal? AllMonth { get; set; }

    public decimal? AllYear { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public decimal? CustomPrice { get; set; }

    public int? UnitId { get; set; }
}
