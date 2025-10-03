using Context.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context
{

    public interface IDbContext : IDisposable
    {
        DbContext Instance { get; }
    }
    public interface ITntParkingApplicationDbContext : IDbContext
    {
        public DbSet<Databasechangelog> Databasechangelogs { get; set; }

        public DbSet<Databasechangeloglock> Databasechangeloglocks { get; set; }

        public DbSet<ParkingArea> ParkingAreas { get; set; }

        public DbSet<ParkingAreaInterval> ParkingAreaIntervals { get; set; }

        public DbSet<ParkingInterval> ParkingIntervals { get; set; }

        public DbSet<ParkingRate> ParkingRates { get; set; }

        public DbSet<ParkingSubscription> ParkingSubscriptions { get; set; }
    }
}

