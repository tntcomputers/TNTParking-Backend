
using Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Context.Repository
{
    public partial class TntParkingDbMssqlContext : DbContext, ITntParkingApplicationDbContext
    {
        public TntParkingDbMssqlContext()
        {

        }
        public DbContext Instance => this;

        public TntParkingDbMssqlContext(DbContextOptions<TntParkingDbMssqlContext> options)
             : base(options)
        {

            var dbCreater = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (dbCreater != null)
            {
                // Create Database 
                if (!dbCreater.CanConnect())
                {
                    try
                    {
                        dbCreater.Create();
                    }
                    catch (Exception e)
                    {

                    }
                }
                // Create Tables
                if (!dbCreater.HasTables())
                {
                    dbCreater.CreateTables();
                }
            }
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            ModelCreatingPartial.Insert(modelBuilder);
        }
    }
}
