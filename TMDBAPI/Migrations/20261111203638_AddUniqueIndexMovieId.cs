using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TMDBAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexMovieId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
            name: "IX_FavoriteMovies_MovieId",
            table: "FavoriteMovies",
            column: "MovieId",
            unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
            name: "IX_FavoriteMovies_MovieId",
            table: "FavoriteMovies");
        }
    }
}
