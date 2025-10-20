using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class ParkingParkingPayment
{
    public int Id { get; set; }

    public int IdAreaType { get; set; }

    public int UnitId { get; set; }

    public int IdArea { get; set; }

    public string CarPlate { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ParkingArea IdAreaNavigation { get; set; } = null!;

    public virtual ParkingAreaType IdAreaTypeNavigation { get; set; } = null!;
}
