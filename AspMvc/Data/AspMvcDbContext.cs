using Pomelo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AspMvc.Models;

namespace AspMvc.Data
{

    public class AspMvcDbContext : DbContext
    {
        public AspMvcDbContext(DbContextOptions<AspMvcDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Person> People { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // primary keys
            modelBuilder.Entity<Person>().HasKey(p => p.PersonId);
            modelBuilder.Entity<City>().HasKey(c => c.CityId);
            modelBuilder.Entity<Country>().HasKey(co => co.CountryId);

            // relationships
            modelBuilder.Entity<City>()
                .HasOne(ci => ci.Country)
                .WithMany(co => co.Cities)
                .HasForeignKey(ci => ci.CountryId);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.City)
                .WithMany(ci => ci.People)
                .HasForeignKey(p => p.CityId);

            // seeding
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 1, Name = "Sverige" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 2, Name = "Norge" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 3, Name = "Danmark" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 4, Name = "Finland" });
            modelBuilder.Entity<Country>().HasData(new Country { CountryId = 5, Name = "Island" });

            modelBuilder.Entity<City>().HasData(new City { CityId = 1, CountryId = 1, Name = "Mellerud" });
            modelBuilder.Entity<City>().HasData(new City { CityId = 2, CountryId = 1, Name = "Göteborg" });
            modelBuilder.Entity<City>().HasData(new City { CityId = 3, CountryId = 1, Name = "Stockholm" });
            modelBuilder.Entity<City>().HasData(new City { CityId = 4, CountryId = 2, Name = "Oslo" });
            modelBuilder.Entity<City>().HasData(new City { CityId = 5, CountryId = 3, Name = "Köpenhamn" });
            modelBuilder.Entity<City>().HasData(new City { CityId = 6, CountryId = 4, Name = "Helsingfors" });
            modelBuilder.Entity<City>().HasData(new City { CityId = 7, CountryId = 5, Name = "Reykjavik" });

            modelBuilder.Entity<Person>().HasData(new Person { PersonId = 1, FirstName = "Andreas", LastName = "Berg", CityId = 1, Phone = "0000-000 000" });
            modelBuilder.Entity<Person>().HasData(new Person { PersonId = 2, FirstName = "Anders", LastName = "Andersson", CityId = 2, Phone = "1234-567 890" });
            modelBuilder.Entity<Person>().HasData(new Person { PersonId = 3, FirstName = "Maria", LastName = "Svensson", CityId = 3, Phone = "0987-654 321" });
        }
    }

}