using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogAPI.Database.Migrations
{
    /// <inheritdoc />
    public partial class PriceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BookModel",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "BookModel");
        }
    }
}
