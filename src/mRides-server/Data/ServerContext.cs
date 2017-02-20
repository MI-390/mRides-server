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
        public DbSet<Ride> Riders { get; set; }
    }
}
