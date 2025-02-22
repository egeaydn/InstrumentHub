using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InstrumentHub.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingToComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "CommentTable",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "CommentTable");
        }
    }
}
