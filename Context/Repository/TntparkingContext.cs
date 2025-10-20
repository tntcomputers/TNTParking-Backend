using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Context.Repository;

public partial class TntparkingContext : DbContext
{
    //public TntparkingContext()
    //{
    //}

    //public TntparkingContext(DbContextOptions<TntparkingContext> options)
    //    : base(options)
    //{
    //}

    public virtual DbSet<AddressesView> AddressesViews { get; set; }

    public virtual DbSet<Databasechangelog> Databasechangelogs { get; set; }

    public virtual DbSet<Databasechangeloglock> Databasechangeloglocks { get; set; }

    public virtual DbSet<FkAuthAddress> FkAuthAddresses { get; set; }

    public virtual DbSet<FkAuthUnit> FkAuthUnits { get; set; }

    public virtual DbSet<FkAuthUser> FkAuthUsers { get; set; }

    public virtual DbSet<ParkingArea> ParkingAreas { get; set; }

    public virtual DbSet<ParkingAreaInterval> ParkingAreaIntervals { get; set; }

    public virtual DbSet<ParkingAreaType> ParkingAreaTypes { get; set; }

    public virtual DbSet<ParkingAreaTypeSubscription> ParkingAreaTypeSubscriptions { get; set; }

    public virtual DbSet<ParkingAreasDaysOff> ParkingAreasDaysOffs { get; set; }

    public virtual DbSet<ParkingInterval> ParkingIntervals { get; set; }

    public virtual DbSet<ParkingParkingDaysClosed> ParkingParkingDaysCloseds { get; set; }

    public virtual DbSet<ParkingParkingPayment> ParkingParkingPayments { get; set; }

    public virtual DbSet<ParkingParkingSpace> ParkingParkingSpaces { get; set; }

    public virtual DbSet<ParkingRate> ParkingRates { get; set; }

    public virtual DbSet<ParkingSubscription> ParkingSubscriptions { get; set; }

    public virtual DbSet<UnitsView> UnitsViews { get; set; }

    public virtual DbSet<UsersView> UsersViews { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost:5439;Database=TNTParking;Username=tntcomputers;Password=Traxdata13?");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgres_fdw");

        modelBuilder.Entity<AddressesView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("addresses_view");

            entity.Property(e => e.Apartment).HasMaxLength(20);
            entity.Property(e => e.Block).HasMaxLength(20);
            entity.Property(e => e.Country).HasMaxLength(200);
            entity.Property(e => e.County).HasMaxLength(200);
            entity.Property(e => e.Floor).HasMaxLength(20);
            entity.Property(e => e.Latitude).HasPrecision(10, 4);
            entity.Property(e => e.Longitude).HasPrecision(10, 4);
            entity.Property(e => e.Number).HasMaxLength(20);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.Sector).HasMaxLength(20);
            entity.Property(e => e.Settlement).HasMaxLength(200);
            entity.Property(e => e.Stair).HasMaxLength(10);
            entity.Property(e => e.Street).HasMaxLength(200);
            entity.Property(e => e.Village).HasMaxLength(200);
        });

        modelBuilder.Entity<Databasechangelog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("databasechangelog");

            entity.Property(e => e.Author)
                .HasMaxLength(255)
                .HasColumnName("author");
            entity.Property(e => e.Comments)
                .HasMaxLength(255)
                .HasColumnName("comments");
            entity.Property(e => e.Contexts)
                .HasMaxLength(255)
                .HasColumnName("contexts");
            entity.Property(e => e.Dateexecuted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dateexecuted");
            entity.Property(e => e.DeploymentId)
                .HasMaxLength(10)
                .HasColumnName("deployment_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Exectype)
                .HasMaxLength(10)
                .HasColumnName("exectype");
            entity.Property(e => e.Filename)
                .HasMaxLength(255)
                .HasColumnName("filename");
            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasColumnName("id");
            entity.Property(e => e.Labels)
                .HasMaxLength(255)
                .HasColumnName("labels");
            entity.Property(e => e.Liquibase)
                .HasMaxLength(20)
                .HasColumnName("liquibase");
            entity.Property(e => e.Md5sum)
                .HasMaxLength(35)
                .HasColumnName("md5sum");
            entity.Property(e => e.Orderexecuted).HasColumnName("orderexecuted");
            entity.Property(e => e.Tag)
                .HasMaxLength(255)
                .HasColumnName("tag");
        });

        modelBuilder.Entity<Databasechangeloglock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("databasechangeloglock_pkey");

            entity.ToTable("databasechangeloglock");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Locked).HasColumnName("locked");
            entity.Property(e => e.Lockedby)
                .HasMaxLength(255)
                .HasColumnName("lockedby");
            entity.Property(e => e.Lockgranted)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lockgranted");
        });

        modelBuilder.Entity<FkAuthAddress>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("fk_auth_addresses");

            entity.Property(e => e.Apartment).HasMaxLength(20);
            entity.Property(e => e.Block).HasMaxLength(20);
            entity.Property(e => e.Country).HasMaxLength(200);
            entity.Property(e => e.County).HasMaxLength(200);
            entity.Property(e => e.Floor).HasMaxLength(20);
            entity.Property(e => e.Latitude).HasPrecision(10, 4);
            entity.Property(e => e.Longitude).HasPrecision(10, 4);
            entity.Property(e => e.Number).HasMaxLength(20);
            entity.Property(e => e.PostalCode).HasMaxLength(20);
            entity.Property(e => e.Sector).HasMaxLength(20);
            entity.Property(e => e.Settlement).HasMaxLength(200);
            entity.Property(e => e.Stair).HasMaxLength(10);
            entity.Property(e => e.Street).HasMaxLength(200);
            entity.Property(e => e.Village).HasMaxLength(200);
        });

        modelBuilder.Entity<FkAuthUnit>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("fk_auth_units");

            entity.Property(e => e.Uniqueidentifier).HasMaxLength(200);
        });

        modelBuilder.Entity<FkAuthUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("fk_auth_users");

            entity.Property(e => e.UserName).HasMaxLength(200);
        });

        modelBuilder.Entity<ParkingArea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Areas_pkey");

            entity.ToTable("parking.Areas");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Latitude).HasPrecision(18, 8);
            entity.Property(e => e.Longitude).HasPrecision(18, 8);
            entity.Property(e => e.Name).HasMaxLength(500);

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.ParkingAreas)
                .HasForeignKey(d => d.IdType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Areas_AreaTypes");
        });

        modelBuilder.Entity<ParkingAreaInterval>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AreaIntervals_pkey");

            entity.ToTable("parking.AreaIntervals");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.IdAreaTypeNavigation).WithMany(p => p.ParkingAreaIntervals)
                .HasForeignKey(d => d.IdAreaType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_AreaIntervals_Area");

            entity.HasOne(d => d.IdIntervalNavigation).WithMany(p => p.ParkingAreaIntervals)
                .HasForeignKey(d => d.IdInterval)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_AreaIntervals_Interval");
        });

        modelBuilder.Entity<ParkingAreaType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AreaTypes_pkey");

            entity.ToTable("parking.AreaTypes");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<ParkingAreaTypeSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AreaTypeSubscriptions_pkey");

            entity.ToTable("parking.AreaTypeSubscriptions");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.IdAreaTypeNavigation).WithMany(p => p.ParkingAreaTypeSubscriptions)
                .HasForeignKey(d => d.IdAreaType)
                .HasConstraintName("fk_AreaType_AreaTypeSubscriptions");

            entity.HasOne(d => d.IdSubscriptionNavigation).WithMany(p => p.ParkingAreaTypeSubscriptions)
                .HasForeignKey(d => d.IdSubscription)
                .HasConstraintName("fk_Subscriptions_AreaTypeSubscriptions");
        });

        modelBuilder.Entity<ParkingAreasDaysOff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("AreasDaysOff_pkey");

            entity.ToTable("parking.AreasDaysOff");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.EndDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.StartDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.IdAreaTypeNavigation).WithMany(p => p.ParkingAreasDaysOffs)
                .HasForeignKey(d => d.IdAreaType)
                .HasConstraintName("fk_AreasDaysOff_Area");
        });

        modelBuilder.Entity<ParkingInterval>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Intervals_pkey");

            entity.ToTable("parking.Intervals");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.DaysOfWeek).HasMaxLength(500);
            entity.Property(e => e.FromHour).HasMaxLength(10);
            entity.Property(e => e.ToHour).HasMaxLength(10);
        });

        modelBuilder.Entity<ParkingParkingDaysClosed>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ParkingDaysClosed_pkey");

            entity.ToTable("parking.ParkingDaysClosed");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.EndDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.StartDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.ParkingParkingDaysCloseds)
                .HasForeignKey(d => d.IdArea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Area_ParkingDaysClosed");
        });

        modelBuilder.Entity<ParkingParkingPayment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ParkingPayments_pkey");

            entity.ToTable("parking.ParkingPayments");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.EndDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.StartDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.ParkingParkingPayments)
                .HasForeignKey(d => d.IdArea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Area_ParkingPayments");

            entity.HasOne(d => d.IdAreaTypeNavigation).WithMany(p => p.ParkingParkingPayments)
                .HasForeignKey(d => d.IdAreaType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ParkingPayments_AreaTypes");
        });

        modelBuilder.Entity<ParkingParkingSpace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ParkingSpaces_pkey");

            entity.ToTable("parking.ParkingSpaces");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.ParkingParkingSpaces)
                .HasForeignKey(d => d.IdArea)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_AreaParkingSpace_Area");
        });

        modelBuilder.Entity<ParkingRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Rates_pkey");

            entity.ToTable("parking.Rates");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.FromDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Price).HasPrecision(18, 2);
            entity.Property(e => e.ToDate).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.IdAreaTypeNavigation).WithMany(p => p.ParkingRates)
                .HasForeignKey(d => d.IdAreaType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Rates_Area");

            entity.HasOne(d => d.IdIntervalNavigation).WithMany(p => p.ParkingRates)
                .HasForeignKey(d => d.IdInterval)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Rates_Interval");
        });

        modelBuilder.Entity<ParkingSubscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Subscriptions_pkey");

            entity.ToTable("parking.Subscriptions");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.AllDay).HasPrecision(18, 2);
            entity.Property(e => e.AllMonth).HasPrecision(18, 2);
            entity.Property(e => e.AllWeek).HasPrecision(18, 2);
            entity.Property(e => e.AllYear).HasPrecision(18, 2);
            entity.Property(e => e.CustomPrice).HasPrecision(18, 2);
            entity.Property(e => e.FromDate).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ToDate).HasColumnType("timestamp without time zone");
        });

        modelBuilder.Entity<UnitsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("units_view");

            entity.Property(e => e.Uniqueidentifier).HasMaxLength(200);
        });

        modelBuilder.Entity<UsersView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("users_view");

            entity.Property(e => e.UserName).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
