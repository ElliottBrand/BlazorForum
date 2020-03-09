using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorForum.Data.Migrations
{
    public partial class RemoveConstraintForSubscriptionsFromTopics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicSubscriptions_ForumTopics_ForumTopicId",
                table: "TopicSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_TopicSubscriptions_ForumTopicId",
                table: "TopicSubscriptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TopicSubscriptions_ForumTopicId",
                table: "TopicSubscriptions",
                column: "ForumTopicId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicSubscriptions_ForumTopics_ForumTopicId",
                table: "TopicSubscriptions",
                column: "ForumTopicId",
                principalTable: "ForumTopics",
                principalColumn: "ForumTopicId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
