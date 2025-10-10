using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class ParkingAreaTypeSubscription
{
    public int Id { get; set; }

    public int? IdAreaType { get; set; }

    public int? IdSubscription { get; set; }

    public virtual ParkingAreaType? IdAreaTypeNavigation { get; set; }

    public virtual ParkingSubscription? IdSubscriptionNavigation { get; set; }
}
