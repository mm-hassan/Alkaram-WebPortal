using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations.Portal
{
    /// <inheritdoc />
    public partial class addcols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
               name: "LastUpdatedBy",
               table: "Procedure",
               type: "nvarchar(max)",
               defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Procedure",
                type: "datetime2",
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "Format",
                type: "nvarchar(max)",
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Format",
                type: "datetime2",
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "Certificate",
                type: "nvarchar(max)",
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Certificate",
                type: "datetime2",
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Procedure");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Procedure");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Format");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Format");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Certificate");

            migrationBuilder.DropColumn(
                name: "LastUpdatedOn",
                table: "Certificate");
        }
    }
}
