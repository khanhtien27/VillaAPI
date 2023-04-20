using Microsoft.EntityFrameworkCore;
using VillaAPI.Models;

namespace VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {           
        }
        public DbSet<LocalUser> LocalUsers { get; set; }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Test",
                    Details = "nguyen khanh tien dep trai",
                    ImageUrl = "",
                    Occupancy = 143,
                    Rate = 344,
                    Sqft = 3444,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa
                {
                    Id = 2,
                    Name = "hihi",
                    Details = "nguyen khanh tien dep traiasdasdas afas asfasas asdfssad as asdf",
                    ImageUrl = "",
                    Occupancy = 312,
                    Rate = 4545,
                    Sqft = 34523,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                },
                new Villa
                {
                    Id = 3,
                    Name = "hhhhhhhh",
                    Details = "nguyen khanh tien dep trai asas asdassdfag fd dfdfg df ",
                    ImageUrl = "",
                    Occupancy = 4234,
                    Rate = 655,
                    Sqft = 775,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                }
                );
        }
    }
}
