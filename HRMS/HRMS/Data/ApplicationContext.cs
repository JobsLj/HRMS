using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HRMS.Models;
using HRMS.EntityModels;

namespace HRMS.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<DailyPredictionModel> Predictions { get; set; }
        public DbSet<DailyOccupancy> Occupancy { get; set; }
        public DbSet<DailyRoomRates> RoomRates { get; set; }
        public DbSet<DailyOccupancyRoomType> RoomTypeOccupancy { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyPredictionModel>().ToTable("Predictions");
            modelBuilder.Entity<DailyRoomRates>().ToTable("RoomRates");
            modelBuilder.Entity<DailyOccupancy>().ToTable("Occupancy");
            modelBuilder.Entity<DailyOccupancyRoomType>().ToTable("RoomTypeOccupancy");
        }
    }
}
