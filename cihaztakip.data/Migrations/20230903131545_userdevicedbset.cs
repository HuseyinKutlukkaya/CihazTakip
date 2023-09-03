using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cihaztakip.data.Migrations
{
    /// <inheritdoc />
    public partial class userdevicedbset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "UserDevices");

            migrationBuilder.RenameIndex(
                name: "IX_UserDevice_DeviceId",
                table: "UserDevices",
                newName: "IX_UserDevices_DeviceId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserDevices_UserId_DeviceId",
                table: "UserDevices",
                columns: new[] { "UserId", "DeviceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDevices",
                table: "UserDevices",
                column: "UserDeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDevices_AspNetUsers_UserId",
                table: "UserDevices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDevices_Devices_DeviceId",
                table: "UserDevices",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "DeviceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDevices_AspNetUsers_UserId",
                table: "UserDevices");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDevices_Devices_DeviceId",
                table: "UserDevices");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserDevices_UserId_DeviceId",
                table: "UserDevices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDevices",
                table: "UserDevices");

            migrationBuilder.RenameTable(
                name: "UserDevices",
                newName: "UserDevice");

            migrationBuilder.RenameIndex(
                name: "IX_UserDevices_DeviceId",
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
    }
}
