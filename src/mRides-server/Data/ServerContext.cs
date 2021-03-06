﻿using Microsoft.EntityFrameworkCore;
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
        public ServerContext()
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public DbSet<RiderRequest> RiderRequests { get; set; }
        public DbSet<DestinationCoordinate> DestinationCoordinates { get; set; }
        //public DbSet<UserRides> UserRides { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<UserRides>()
                .HasOne(s=>s.Ride)
                .WithMany(p => p.UserRides)
                .HasForeignKey(pt => pt.RideId);

            builder.Entity<UserRides>()
                .HasOne(pt => pt.Rider)
                .WithMany(t => t.RidesAsRider)
                .HasForeignKey(pt => pt.RiderId);

            builder.Entity<RiderRequest>()
               .HasOne(r=>r.Request)
               .WithMany(p => p.RiderRequests)
               .HasForeignKey(pt => pt.RequestID);

            builder.Entity<RiderRequest>()
                .HasOne(pt => pt.Rider)
                .WithMany(t => t.RequestAsRider)
                .HasForeignKey(pt => pt.RiderID);

            

        }
    }
    
}
