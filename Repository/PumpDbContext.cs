using Microsoft.EntityFrameworkCore;
using Models;
using Models.PumpTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PumpDbContext : DbContext
    {
        public DbSet<Pump> pumps { get; set; }
        public DbSet<CompactPump> compactPumps { get; set; }
        public DbSet<CompactPlusPump> compactPlusPumps { get; set; }
        public DbSet<SpacePump> spacePumps { get; set; }
        public DbSet<SpacePlusPump> spacePlusPumps { get; set; }

        public PumpDbContext(DbContextOptions<PumpDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder
                    .UseInMemoryDatabase("myDB")
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpacePlusPump>().HasData(new SpacePlusPump("1111", Room.Room202, Models.Type.Volumetric));
            modelBuilder.Entity<CompactPump>().HasData(new CompactPump("1234", Room.Room201, Models.Type.Volumetric));
            modelBuilder.Entity<CompactPlusPump>().HasData(new CompactPlusPump("1112", Room.Room201, Models.Type.Syringe));
            modelBuilder.Entity<SpacePump>().HasData(new SpacePump("5555", Room.Room201, Models.Type.Syringe));
        }
    }
}
