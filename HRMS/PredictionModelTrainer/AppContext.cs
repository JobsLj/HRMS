using Microsoft.EntityFrameworkCore;
using PredictionModelTrainer.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PredictionModelTrainer
{
    public class AppContext : DbContext
    {
        public DbSet<RoomRates> RoomRates { get; set; }
        public DbSet<Occupancy> Occupancy { get; set; }
        public DbSet<OccupancyRoomType> RoomTypeOccupancy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=HRMS;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
