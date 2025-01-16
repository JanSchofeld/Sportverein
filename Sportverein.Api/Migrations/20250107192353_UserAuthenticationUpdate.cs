using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sportverein.Api.Migrations
{
    /// <inheritdoc />
    public partial class UserAuthenticationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isadmin",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "passwordhash",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isadmin",
                table: "users");

            migrationBuilder.DropColumn(
                name: "passwordhash",
                table: "users");
        }
    }
}
