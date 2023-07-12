using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalexBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class OrderChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Orders",
                newName: "Phone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Orders",
                newName: "PhoneNumber");
        }
    }
}
