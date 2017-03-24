using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mRidesserver.Migrations
{
    public partial class updatedDestCoord2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DestinationCoordinate",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    RequestID = table.Column<int>(nullable: false),
                    coordinate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationCoordinate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DestinationCoordinate_Request_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Request",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DestinationCoordinate_RequestID",
                table: "DestinationCoordinate",
                column: "RequestID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DestinationCoordinate");
        }
    }
}
