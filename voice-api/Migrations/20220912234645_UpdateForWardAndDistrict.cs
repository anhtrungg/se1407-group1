using Microsoft.EntityFrameworkCore.Migrations;

namespace VoiceAPI.Migrations
{
    public partial class UpdateForWardAndDistrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ward_Name",
                table: "Ward");

            migrationBuilder.DropIndex(
                name: "IX_District_Name",
                table: "District");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Ward_Name",
                table: "Ward",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_District_Name",
                table: "District",
                column: "Name",
                unique: true);
        }
    }
}
