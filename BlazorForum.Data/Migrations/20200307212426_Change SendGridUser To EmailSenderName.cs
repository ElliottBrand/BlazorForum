using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorForum.Data.Migrations
{
    public partial class ChangeSendGridUserToEmailSenderName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendGridUser",
                table: "Configuration");

            migrationBuilder.AddColumn<string>(
                name: "EmailSenderName",
                table: "Configuration",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailSenderName",
                table: "Configuration");

            migrationBuilder.AddColumn<string>(
                name: "SendGridUser",
                table: "Configuration",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
