using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorForum.Data.Migrations
{
    public partial class AddSupportForumColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSupportForum",
                table: "Forums",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAnswer",
                table: "ForumPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSupportForum",
                table: "Forums");

            migrationBuilder.DropColumn(
                name: "IsAnswer",
                table: "ForumPosts");
        }
    }
}
