using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSYCO.Ranpod.DataCollector.Migrations
{
    public partial class newLogModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GetPMDLProStatusString0",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GetPMDLProStatusString1",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GetPMDLProStatusString2",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPMDLProRunning",
                table: "Logs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MyProperty",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegistryAppID",
                table: "Logs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RegistryDefaultProtectionMode",
                table: "Logs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistryExpireDate",
                table: "Logs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RegistryLicenseData",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegistryOrgID",
                table: "Logs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RegistryPassword",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistryRestrictedPath",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegistryServerID",
                table: "Logs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RegistryTrustedPath",
                table: "Logs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GetPMDLProStatusString0",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "GetPMDLProStatusString1",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "GetPMDLProStatusString2",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "IsPMDLProRunning",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RegistryAppID",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RegistryDefaultProtectionMode",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RegistryExpireDate",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RegistryLicenseData",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RegistryOrgID",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RegistryPassword",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RegistryRestrictedPath",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RegistryServerID",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "RegistryTrustedPath",
                table: "Logs");
        }
    }
}
