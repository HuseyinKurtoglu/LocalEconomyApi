using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LocalEconomyApi.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBusinessNameUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Businesses_Name",
                table: "Businesses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Businesses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_Name",
                table: "Businesses",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Businesses_Name",
                table: "Businesses");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Businesses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_Name",
                table: "Businesses",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
