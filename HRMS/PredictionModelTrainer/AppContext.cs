using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace PredictionModelTrainer
{
    public class AppContext : DbContext
    {
        public DbSet<RoomRates> RoomRates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=HRMS;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
