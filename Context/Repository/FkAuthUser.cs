using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class FkAuthUser
{
    public int Id { get; set; }

    public int UnitId { get; set; }

    public string? UserName { get; set; }

    public bool? IsActive { get; set; }
}
