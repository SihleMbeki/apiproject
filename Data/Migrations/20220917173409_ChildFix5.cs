using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class ChildFix5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_child_users_parentId",
                table: "child");

            migrationBuilder.DropIndex(
                name: "IX_child_parentId",
                table: "child");

            migrationBuilder.DropColumn(
                name: "parentId",
                table: "child");

            migrationBuilder.AddColumn<int>(
                name: "parent",
                table: "child",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "parent",
                table: "child");

            migrationBuilder.AddColumn<int>(
                name: "parentId",
                table: "child",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_child_parentId",
                table: "child",
                column: "parentId");

            migrationBuilder.AddForeignKey(
                name: "FK_child_users_parentId",
                table: "child",
                column: "parentId",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
