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
            
        public DbSet<User> Users { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        //public DbSet<UserRides> UserRides { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasDiscriminator<string>("Type")
                .HasValue<Driver>("Driver")
                .HasValue<Rider>("Rider")
                .HasValue<User>("User");

            builder.Entity<UserRides>()
                .HasOne(s=>s.Ride)
                .WithMany(p => p.UserRides)
                .HasForeignKey(pt => pt.ID);

            builder.Entity<UserRides>()
                .HasOne(pt => pt.Rider)
                .WithMany(t => t.UserRides)
                .HasForeignKey(pt => pt.ID);

        }
    }
    
}
