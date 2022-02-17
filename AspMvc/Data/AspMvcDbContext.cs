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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(new Person {PersonId = 1, FirstName = "Andreas", LastName = "Berg", City = "Mellerud", Phone = "0000-000 000"});
            modelBuilder.Entity<Person>().HasData(new Person {PersonId = 2, FirstName = "Anders", LastName = "Andersson", City = "Göteborg", Phone = "1234-567 890"});
            modelBuilder.Entity<Person>().HasData(new Person {PersonId = 3, FirstName = "Maria", LastName = "Svensson", City = "Stockholm", Phone = "0987-654 321"});
        }
    }

}