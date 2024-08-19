using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebPortal.Migrations.Portal
{
    /// <inheritdoc />
    public partial class UpdateNonNullableFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
       name: "LastUpdatedBy",
       table: "Certificate",
       nullable: false,
       oldClrType: typeof(string),
       oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdatedOn",
                table: "Certificate",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
