using Microsoft.EntityFrameworkCore.Migrations;
using WebTest.Services;

#nullable disable

namespace WebTest.Migrations
{
    /// <inheritdoc />
    public partial class CreateUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var password = AuthService.HashPassword(new Models.OrgStructure.User(), "password");

            migrationBuilder.Sql($"INSERT INTO users (id, login, password) VALUES (1, 'admin', '{password}')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM users");
        }
    }
}
