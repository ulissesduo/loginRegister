using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace baltaIOCrud.Migrations
{
    /// <inheritdoc />
    public partial class addfieldssaleslead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "SalesLead",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SalesLead",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "SalesLead");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "SalesLead");
        }
    }
}
