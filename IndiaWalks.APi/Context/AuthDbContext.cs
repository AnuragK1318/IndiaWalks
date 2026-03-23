using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IndiaWalks.APi.Context
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerId = "98be9ad3 - 7d28 - 49d7 - a5e9 - e74879a65a0a";
            var writerId = "5c8be676-2425-49c5-b3a0-d98fdc986371";
            var roles = new List<IdentityRole>() 
            {
                new IdentityRole
                {
                    Id = readerId,
                    ConcurrencyStamp=readerId,
                    Name="Reader",
                    NormalizedName="READER"
                },
                new IdentityRole
                {
                    Id = writerId,
                    ConcurrencyStamp = writerId,
                    Name="Writer",
                    NormalizedName="WRITER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
