using Microsoft.EntityFrameworkCore;
using RespayMLS.Core.Models;

namespace RespayMLS.Data
{
    public class RespayDbContext : DbContext
    {
        public RespayDbContext(DbContextOptions<RespayDbContext> options) : base(options)
        {

        }

        public DbSet<Area> Areas { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Estate> Estates { get; set; }

        public DbSet<Street> Streets { get; set; }

        public DbSet<State> States { get; set; }

        public DbSet<Country> Countries { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    new CityMap(builder.Entity<City>());

        //    base.OnModelCreating(builder);
        //}
    }
   
}
