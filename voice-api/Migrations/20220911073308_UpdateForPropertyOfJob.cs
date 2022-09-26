using Microsoft.EntityFrameworkCore.Migrations;

namespace VoiceAPI.Migrations
{
    public partial class UpdateForPropertyOfJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Job",
                newName: "minAge");

            migrationBuilder.AddColumn<int>(
                name: "maxAge",
                table: "Job",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "maxAge",
                table: "Job");

            migrationBuilder.RenameColumn(
                name: "minAge",
                table: "Job",
                newName: "Age");
        }
    }
}
