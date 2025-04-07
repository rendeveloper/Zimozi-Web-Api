using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZimoziSolutions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ManyToOneDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignedUserId",
                table: "OTask",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "OTask",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OTask",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_OTask_AssignedUserId",
                table: "OTask",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OTask_User_AssignedUserId",
                table: "OTask",
                column: "AssignedUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OTask_User_AssignedUserId",
                table: "OTask");

            migrationBuilder.DropIndex(
                name: "IX_OTask_AssignedUserId",
                table: "OTask");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "OTask");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "OTask");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "OTask");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "OTask");
        }
    }
}
