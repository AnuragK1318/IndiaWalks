using IndiaWalks.APi.Domain;
using Microsoft.EntityFrameworkCore;

namespace IndiaWalks.APi.Context
{
    public class IndiaWalksDbContext: DbContext
    {
        public IndiaWalksDbContext(DbContextOptions<IndiaWalksDbContext> dbContextOptions):base(dbContextOptions)
        {
            
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Walks> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Data for Difficulty
            var Difficulties=new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = 1,
                    Code = "Easy"
                },
                new Difficulty()
                {
                    Id = 2,
                    Code = "Medium"
                },
                new Difficulty()
                {
                    Id = 3,
                    Code = "Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(Difficulties);

            // Seed Data for Regions
            var Regions = new List<Region>()
            {

                new Region()
                {
                    Id = 1,
                    Code = "AP",
                    Name = "Andhra Pradesh",
                    Area = 162968,
                    Lat = 15.9129,
                    Long = 79.7400,
                    Population = 49577103
                }
            };
            modelBuilder.Entity<Region>().HasData(Regions);

            //seed data for Walks
            var Walks = new List<Walks>()
            {
                new Walks()
                {
                    Id = 1,
                    Name = "Charminar Walk",
                    Length = 2.5,
                    RegionId = 1,
                    DifficultyId = 1
                }
            };

            modelBuilder.Entity<Walks>().HasData(Walks);

        }

    }
}
