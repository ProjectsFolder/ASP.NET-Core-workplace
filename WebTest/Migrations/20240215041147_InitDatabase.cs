using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTest.Migrations
{
    /// <inheritdoc />
    public partial class InitDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.Id);
                });

            migrationBuilder.Sql("INSERT INTO users (id, name, password) VALUES (1, \"Alex\", \"abC\"), (2, \"Bob\", \"bcd\"), (3, \"Sam\", \"CDE\")");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "users");
        }
    }
}
