using System;
using System.Collections.Generic;

namespace Context.Repository;

public partial class FkAuthAddress
{
    public int? Id { get; set; }

    public int? UnitId { get; set; }

    public string? Country { get; set; }

    public string? County { get; set; }

    public string? Settlement { get; set; }

    public string? Village { get; set; }

    public string? Street { get; set; }

    public string? Number { get; set; }

    public string? Block { get; set; }

    public string? Sector { get; set; }

    public string? Floor { get; set; }

    public string? Apartment { get; set; }

    public int? NumberInt { get; set; }

    public int? CountryCode { get; set; }

    public int? CountyCode { get; set; }

    public int? SettlementCode { get; set; }

    public int? StreetCode { get; set; }

    public string? PostalCode { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public int? Type { get; set; }

    public string? Stair { get; set; }
}
