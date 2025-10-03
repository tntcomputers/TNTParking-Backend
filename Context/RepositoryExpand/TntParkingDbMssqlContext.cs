using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context.Repository
{
    public partial class TntParkingDbMssqlContext
    {
        public virtual DbSet<Databasechangelog> Databasechangelogs { get; set; }

        public virtual DbSet<Databasechangeloglock> Databasechangeloglocks { get; set; }

        public virtual DbSet<ParkingArea> ParkingAreas { get; set; }

        public virtual DbSet<ParkingAreaInterval> ParkingAreaIntervals { get; set; }

        public virtual DbSet<ParkingInterval> ParkingIntervals { get; set; }

        public virtual DbSet<ParkingRate> ParkingRates { get; set; }

        public virtual DbSet<ParkingSubscription> ParkingSubscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<ParkingArea>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Areas_pkey");

                entity.ToTable("parking.Areas");

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<ParkingAreaInterval>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("AreaIntervals_pkey");

                entity.ToTable("parking.AreaIntervals");

                entity.HasOne(d => d.IdAreaTypeNavigation).WithMany(p => p.ParkingAreaIntervals)
                    .HasForeignKey(d => d.IdAreaType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_AreaIntervals_Area");

                entity.HasOne(d => d.IdIntervalNavigation).WithMany(p => p.ParkingAreaIntervals)
                    .HasForeignKey(d => d.IdInterval)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_AreaIntervals_Interval");
            });

            modelBuilder.Entity<ParkingInterval>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Intervals_pkey");

                entity.ToTable("parking.Intervals");

                entity.Property(e => e.DayOfWeek).HasMaxLength(500);
                entity.Property(e => e.FromHour).HasMaxLength(10);
                entity.Property(e => e.ToHour).HasMaxLength(10);
            });

            modelBuilder.Entity<ParkingRate>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Rates_pkey");

                entity.ToTable("parking.Rates");

                entity.Property(e => e.FromDate).HasColumnType("timestamp without time zone");
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e => e.ToDate).HasColumnType("timestamp without time zone");
            });

            modelBuilder.Entity<ParkingSubscription>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Subscriptions_pkey");

                entity.ToTable("parking.Subscriptions");

                entity.Property(e => e.AllDay).HasPrecision(18, 2);
                entity.Property(e => e.AllMonth).HasPrecision(18, 2);
                entity.Property(e => e.AllWeek).HasPrecision(18, 2);
                entity.Property(e => e.AllYear).HasPrecision(18, 2);
                entity.Property(e => e.CustomPrice).HasPrecision(18, 2);
                entity.Property(e => e.FromDate).HasColumnType("timestamp without time zone");
                entity.Property(e => e.ToDate).HasColumnType("timestamp without time zone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
