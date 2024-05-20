using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogAPI.Database.Migrations
{
    /// <inheritdoc />
    public partial class AmountColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "BookModel",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "BookModel");
        }
    }
}
