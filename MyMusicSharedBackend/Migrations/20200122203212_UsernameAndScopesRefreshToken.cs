using Microsoft.EntityFrameworkCore.Migrations;

namespace MyMusicSharedBackend.Migrations
{
    public partial class UsernameAndScopesRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Scopes",
                table: "RefreshTokens",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "RefreshTokens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scopes",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "RefreshTokens");
        }
    }
}
