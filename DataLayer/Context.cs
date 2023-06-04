using DataLayer.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataLayer
{
    public class Context : DbContext
    {
        public DbSet<Role> Roles {get;set;}
        public DbSet<Airline> Airlines {get;set;}
        public DbSet<Airplane> Airplanes {get;set;}
        public DbSet<Airline_Route> Airline_Routes { get;set;}
        public DbSet<Airport> Airports {get;set;}
        public DbSet<Booking> Bookings {get;set;}
        public DbSet<City> Cities {get;set; }
        public DbSet<Flight> Flights {get;set; }
        public DbSet<Klass> Klasses {get;set;}
        public DbSet<Route> Routes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
       .AddJsonFile("appsettings.json")
       .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            ServerVersion vesrion = ServerVersion.AutoDetect(connectionString);

            optionsBuilder.UseMySql(connectionString, vesrion);
        }

        public Context(DbContextOptions<Context> options) : base(options) { }

    }
    //public class ContextFactory:IDesignTimeDbContextFactory<Context>
    //{
    //    public Context CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<Context>();
    //        string connection = "server=localhost;user=root;database=usersdb5;";
    //        ServerVersion vesrion = ServerVersion.AutoDetect(connection);
    //        optionsBuilder.UseMySql(connection, vesrion,b=>b.MigrationsAssembly("DataLayer"));
    //        return new Context(optionsBuilder.Options);
    //    }
    //}
}
