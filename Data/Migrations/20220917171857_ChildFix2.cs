using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class ChildFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_child_users_parentId",
                table: "child");

            migrationBuilder.RenameColumn(
                name: "parentId",
                table: "child",
                newName: "parentidId");

            migrationBuilder.RenameIndex(
                name: "IX_child_parentId",
                table: "child",
                newName: "IX_child_parentidId");

            migrationBuilder.AddColumn<string>(
                name: "gender",
                table: "child",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_child_users_parentidId",
                table: "child",
                column: "parentidId",
                principalTable: "users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_child_users_parentidId",
                table: "child");

            migrationBuilder.DropColumn(
                name: "gender",
                table: "child");

            migrationBuilder.RenameColumn(
                name: "parentidId",
                table: "child",
                newName: "parentId");

            migrationBuilder.RenameIndex(
                name: "IX_child_parentidId",
                table: "child",
                newName: "IX_child_parentId");

            migrationBuilder.AddForeignKey(
                name: "FK_child_users_parentId",
                table: "child",
                column: "parentId",
                principalTable: "users",
                principalColumn: "Id");
        }
    }
}
