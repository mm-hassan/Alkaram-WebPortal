using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations.Portal
{
    /// <inheritdoc />
    public partial class dropcols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "LastUpdatedBy",
            table: "Certificate");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Certificate");

            migrationBuilder.DropColumn(
           name: "LastUpdatedBy",
           table: "Format");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Format");

            migrationBuilder.DropColumn(
          name: "LastUpdatedBy",
          table: "Procedure");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Procedure");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "LastUpdatedBy",
            table: "Certificate",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Certificate",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<string>(
            name: "LastUpdatedBy",
            table: "Format",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Format",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<string>(
            name: "LastUpdatedBy",
            table: "Procedure",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Procedure",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);
        }
    }
}
