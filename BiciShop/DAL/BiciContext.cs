using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models
{
    public class BiciContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Bicicleta> Bicicletas { get; set; }
        public DbSet<BiciType> BiciTypes { get; set; }
        public DbSet<OrderUnit> OrderUnits { get; set; }
        public DbSet<HandlePhase> HandlePhases { get; set; }
        public BiciContext(DbContextOptions<BiciContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderUnit>()
                .HasKey(o => new { o.OrderId, o.BicicletaId });

            modelBuilder.Entity<OrderUnit>().HasOne(u => u.Order).WithMany(o => o.OrderUnits)
                        .HasForeignKey(p => p.OrderId)
                        .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
