using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QWIIK.ProjectTest.Migrations
{
    /// <inheritdoc />
    public partial class remove_unused_item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "UserAppoinments");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAppoinments_Users_UserId",
                table: "UserAppoinments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAppoinments_Users_UserId",
                table: "UserAppoinments");

            migrationBuilder.AddColumn<Guid>(
                name: "AppointmentId",
                table: "UserAppoinments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
