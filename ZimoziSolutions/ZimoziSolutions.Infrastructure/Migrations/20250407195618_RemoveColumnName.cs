using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZimoziSolutions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangeByUserId",
                table: "TaskHistory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChangeByUserId",
                table: "TaskHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
