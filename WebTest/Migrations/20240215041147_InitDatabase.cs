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
                    id = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.UniqueConstraint("uk_users_login", x => x.name);
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
