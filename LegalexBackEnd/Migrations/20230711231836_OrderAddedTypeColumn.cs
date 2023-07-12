using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalexBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class OrderAddedTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Orders");
        }
    }
}
