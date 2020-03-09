using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorForum.Data.Migrations
{
    public partial class RemoveApplicationUserIdFromSubscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicSubscriptions_AspNetUsers_ApplicationUserId",
                table: "TopicSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_TopicSubscriptions_ApplicationUserId",
                table: "TopicSubscriptions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "TopicSubscriptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "TopicSubscriptions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopicSubscriptions_ApplicationUserId",
                table: "TopicSubscriptions",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicSubscriptions_AspNetUsers_ApplicationUserId",
                table: "TopicSubscriptions",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
