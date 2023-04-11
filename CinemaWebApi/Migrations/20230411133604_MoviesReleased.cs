using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaWebApi.Migrations
{
    /// <inheritdoc />
    public partial class MoviesReleased : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Released",
                table: "Movies",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Released",
                table: "Movies");
        }
    }
}
