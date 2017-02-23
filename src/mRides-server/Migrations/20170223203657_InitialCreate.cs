using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mRidesserver.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    FirstName = table.Column<string>(nullable: true),
                    GSD = table.Column<long>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    hasLuggage = table.Column<bool>(nullable: false),
                    isHandicap = table.Column<bool>(nullable: false),
                    isSmoker = table.Column<bool>(nullable: false),
                    prefferedLanguage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DriverID = table.Column<int>(nullable: true),
                    dateTime = table.Column<DateTime>(nullable: false),
                    destination = table.Column<string>(nullable: true),
                    isWeekly = table.Column<bool>(nullable: false),
                    location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Request_User_DriverID",
                        column: x => x.DriverID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ride",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    DriverID = table.Column<int>(nullable: true),
                    dateTime = table.Column<DateTime>(nullable: false),
                    destination = table.Column<string>(nullable: true),
                    isWeekly = table.Column<bool>(nullable: false),
                    location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ride", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ride_User_DriverID",
                        column: x => x.DriverID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RiderRequest",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    RequestID = table.Column<int>(nullable: false),
                    RiderID = table.Column<int>(nullable: false),
                    location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiderRequest", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RiderRequest_Request_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Request",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RiderRequest_User_RiderID",
                        column: x => x.RiderID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRides",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    RideId = table.Column<int>(nullable: false),
                    RiderId = table.Column<int>(nullable: false),
                    driverFeedback = table.Column<string>(nullable: true),
                    location = table.Column<string>(nullable: true),
                    riderFeedback = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRides", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRides_Ride_RideId",
                        column: x => x.RideId,
                        principalTable: "Ride",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRides_User_RiderId",
                        column: x => x.RiderId,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Request_DriverID",
                table: "Request",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Ride_DriverID",
                table: "Ride",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_RiderRequest_RequestID",
                table: "RiderRequest",
                column: "RequestID");

            migrationBuilder.CreateIndex(
                name: "IX_RiderRequest_RiderID",
                table: "RiderRequest",
                column: "RiderID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRides_RideId",
                table: "UserRides",
                column: "RideId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRides_RiderId",
                table: "UserRides",
                column: "RiderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiderRequest");

            migrationBuilder.DropTable(
                name: "UserRides");

            migrationBuilder.DropTable(
                name: "Request");

            migrationBuilder.DropTable(
                name: "Ride");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
