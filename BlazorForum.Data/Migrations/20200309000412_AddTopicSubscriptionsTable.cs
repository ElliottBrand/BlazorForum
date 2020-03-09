using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorForum.Data.Migrations
{
    public partial class AddTopicSubscriptionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopicSubscriptions",
                columns: table => new
                {
                    TopicSubscriptionsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<string>(maxLength: 256, nullable: true),
                    ForumTopicId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicSubscriptions", x => x.TopicSubscriptionsId);
                    table.ForeignKey(
                        name: "FK_TopicSubscriptions_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopicSubscriptions_ForumTopics_ForumTopicId",
                        column: x => x.ForumTopicId,
                        principalTable: "ForumTopics",
                        principalColumn: "ForumTopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicSubscriptions_ApplicationUserId",
                table: "TopicSubscriptions",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicSubscriptions_ForumTopicId",
                table: "TopicSubscriptions",
                column: "ForumTopicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopicSubscriptions");
        }
    }
}
