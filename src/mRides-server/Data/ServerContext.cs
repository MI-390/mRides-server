using Microsoft.EntityFrameworkCore;
using mRides_server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace mRides_server.Data
{
    public class ServerContext:DbContext
    {
        public ServerContext(DbContextOptions<ServerContext> options) : base(options)
        {
        }
            
        public DbSet<User> Users { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasDiscriminator<int>("Type")
                .HasValue<Driver>(2)
                .HasValue<Rider>(1)
                .HasValue<User>(0);
                
        }
    }
    
}
