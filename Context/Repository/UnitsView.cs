using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class UnitsView
{
    public int? Id { get; set; }

    public int? CompanyId { get; set; }

    public bool? IsActive { get; set; }

    public string? Uniqueidentifier { get; set; }
}
