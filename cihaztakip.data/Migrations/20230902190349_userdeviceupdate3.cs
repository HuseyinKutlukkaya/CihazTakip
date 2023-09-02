using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cihaztakip.data.Migrations
{
    /// <inheritdoc />
    public partial class userdeviceupdate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userDevices_AspNetUsers_UserId",
                table: "userDevices");

            migrationBuilder.DropForeignKey(
                name: "FK_userDevices_Devices_DeviceId",
                table: "userDevices");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_userDevices_UserId_DeviceId",
                table: "userDevices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userDevices",
                table: "userDevices");

            migrationBuilder.RenameTable(
                name: "userDevices",
                newName: "UserDevice");

            migrationBuilder.RenameIndex(
                name: "IX_userDevices_DeviceId",
                table: "UserDevice",
                newName: "IX_UserDevice_DeviceId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserDevice_UserId_DeviceId",
                table: "UserDevice",
                columns: new[] { "UserId", "DeviceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDevice",
                table: "UserDevice",
                column: "UserDeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDevice_AspNetUsers_UserId",
                table: "UserDevice",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDevice_Devices_DeviceId",
                table: "UserDevice",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDevice_AspNetUsers_UserId",
                table: "UserDevice");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDevice_Devices_DeviceId",
                table: "UserDevice");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserDevice_UserId_DeviceId",
                table: "UserDevice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDevice",
                table: "UserDevice");

            migrationBuilder.RenameTable(
                name: "UserDevice",
                newName: "userDevices");

            migrationBuilder.RenameIndex(
                name: "IX_UserDevice_DeviceId",
                table: "userDevices",
                newName: "IX_userDevices_DeviceId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_userDevices_UserId_DeviceId",
                table: "userDevices",
                columns: new[] { "UserId", "DeviceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_userDevices",
                table: "userDevices",
                column: "UserDeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_userDevices_AspNetUsers_UserId",
                table: "userDevices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_userDevices_Devices_DeviceId",
                table: "userDevices",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
