using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QWIIK.ProjectTest.Migrations
{
    /// <inheritdoc />
    public partial class add_update_ef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAppoinments_Appointments_AppointmentId",
                table: "UserAppoinments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAppoinments_Users_UserId",
                table: "UserAppoinments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAppoinments",
                table: "UserAppoinments");

            migrationBuilder.DropIndex(
                name: "IX_UserAppoinments_AppointmentId",
                table: "UserAppoinments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAppoinments",
                table: "UserAppoinments",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAppoinments",
                table: "UserAppoinments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAppoinments",
                table: "UserAppoinments",
                columns: new[] { "UserId", "AppointmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAppoinments_AppointmentId",
                table: "UserAppoinments",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAppoinments_Appointments_AppointmentId",
                table: "UserAppoinments",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAppoinments_Users_UserId",
                table: "UserAppoinments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
