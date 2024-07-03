using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    public partial class CreateRoomTableandEducationId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Groups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatCount = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoftDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_EducationId",
                table: "Groups",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_RoomId",
                table: "Groups",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Educations_EducationId",
                table: "Groups",
                column: "EducationId",
                principalTable: "Educations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Rooms_RoomId",
                table: "Groups",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Educations_EducationId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Rooms_RoomId",
                table: "Groups");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Groups_EducationId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_RoomId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Groups");
        }
    }
}
