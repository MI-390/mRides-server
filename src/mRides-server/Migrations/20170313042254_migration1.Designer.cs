using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using mRides_server.Data;

namespace mRidesserver.Migrations
{
    [DbContext(typeof(ServerContext))]
    [Migration("20170313042254_migration1")]
    partial class migration1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("mRides_server.Models.Request", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DriverID");

                    b.Property<DateTime>("dateTime");

                    b.Property<string>("destination");

                    b.Property<bool>("isWeekly");

                    b.Property<string>("location");

                    b.HasKey("ID");

                    b.HasIndex("DriverID");

                    b.ToTable("Request");
                });

            modelBuilder.Entity("mRides_server.Models.Ride", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DriverID");

                    b.Property<DateTime>("dateTime");

                    b.Property<string>("destination");

                    b.Property<bool>("isWeekly");

                    b.Property<string>("location");

                    b.HasKey("ID");

                    b.HasIndex("DriverID");

                    b.ToTable("Ride");
                });

            modelBuilder.Entity("mRides_server.Models.RiderRequest", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RequestID");

                    b.Property<int>("RiderID");

                    b.Property<string>("destination");

                    b.Property<string>("location");

                    b.HasKey("ID");

                    b.HasIndex("RequestID");

                    b.HasIndex("RiderID");

                    b.ToTable("RiderRequest");
                });

            modelBuilder.Entity("mRides_server.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<long>("GSD");

                    b.Property<string>("LastName");

                    b.Property<long>("facebookID");

                    b.Property<string>("genderPreference");

                    b.Property<bool>("hasLuggage");

                    b.Property<bool>("hasPet");

                    b.Property<bool>("isHandicap");

                    b.Property<bool>("isSmoker");

                    b.Property<string>("prefferedLanguage");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("mRides_server.Models.UserRides", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RideId");

                    b.Property<int>("RiderId");

                    b.Property<string>("destinaion");

                    b.Property<string>("driverFeedback");

                    b.Property<int>("driverStars");

                    b.Property<string>("location");

                    b.Property<string>("riderFeedback");

                    b.Property<int>("riderStars");

                    b.HasKey("ID");

                    b.HasIndex("RideId");

                    b.HasIndex("RiderId");

                    b.ToTable("UserRides");
                });

            modelBuilder.Entity("mRides_server.Models.Request", b =>
                {
                    b.HasOne("mRides_server.Models.User", "Driver")
                        .WithMany("RequestsAsDriver")
                        .HasForeignKey("DriverID");
                });

            modelBuilder.Entity("mRides_server.Models.Ride", b =>
                {
                    b.HasOne("mRides_server.Models.User", "Driver")
                        .WithMany("RidesAsDriver")
                        .HasForeignKey("DriverID");
                });

            modelBuilder.Entity("mRides_server.Models.RiderRequest", b =>
                {
                    b.HasOne("mRides_server.Models.Request", "Request")
                        .WithMany("RiderRequests")
                        .HasForeignKey("RequestID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("mRides_server.Models.User", "Rider")
                        .WithMany("RequestAsRider")
                        .HasForeignKey("RiderID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("mRides_server.Models.UserRides", b =>
                {
                    b.HasOne("mRides_server.Models.Ride", "Ride")
                        .WithMany("UserRides")
                        .HasForeignKey("RideId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("mRides_server.Models.User", "Rider")
                        .WithMany("RidesAsRider")
                        .HasForeignKey("RiderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
