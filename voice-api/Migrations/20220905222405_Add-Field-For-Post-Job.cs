using Microsoft.EntityFrameworkCore.Migrations;
using VoiceAPI.Entities.Enums;

namespace VoiceAPI.Migrations
{
    public partial class AddFieldForPostJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:Accent", "north,mid,south,other")
                .Annotation("Npgsql:Enum:AccountStatus", "inactive,active,blocked,deleted")
                .Annotation("Npgsql:Enum:Gender", "male,female,other")
                .Annotation("Npgsql:Enum:JobInvitationStatus", "pending,not_accepted,accepted")
                .Annotation("Npgsql:Enum:JobStatus", "pending,processing,finished,deleted")
                .Annotation("Npgsql:Enum:Language", "vietnam,english,other")
                .Annotation("Npgsql:Enum:OrderStatus", "pending,processing,finished,rejected")
                .Annotation("Npgsql:Enum:Role", "candidate,enterprise")
                .Annotation("Npgsql:Enum:TransactionType", "receive,send,deposit,refunded,unlock")
                .Annotation("Npgsql:Enum:WorkingStatus", "available,unavailable")
                .OldAnnotation("Npgsql:Enum:Accent", "north,mid,south,other")
                .OldAnnotation("Npgsql:Enum:AccountStatus", "inactive,active,blocked,deleted")
                .OldAnnotation("Npgsql:Enum:Gender", "male,female,other")
                .OldAnnotation("Npgsql:Enum:JobInvitationStatus", "pending,not_accepted,accepted")
                .OldAnnotation("Npgsql:Enum:JobStatus", "pending,processing,finished,deleted")
                .OldAnnotation("Npgsql:Enum:OrderStatus", "pending,processing,finished,rejected")
                .OldAnnotation("Npgsql:Enum:Role", "candidate,enterprise")
                .OldAnnotation("Npgsql:Enum:TransactionType", "receive,send,deposit,refunded,unlock")
                .OldAnnotation("Npgsql:Enum:WorkingStatus", "available,unavailable");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Job",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<GenderEnum>(
                name: "Gender",
                table: "Job",
                type: "\"Gender\"",
                nullable: true,
                defaultValue: GenderEnum.OTHER);

            migrationBuilder.AddColumn<LanguageEnum>(
                name: "Language",
                table: "Job",
                type: "\"Language\"",
                nullable: false,
                defaultValue: LanguageEnum.VIETNAM);

            migrationBuilder.CreateIndex(
                name: "IX_Ward_Name",
                table: "Ward",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Province_Name",
                table: "Province",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_District_Name",
                table: "District",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Ward_Name",
                table: "Ward");

            migrationBuilder.DropIndex(
                name: "IX_Province_Name",
                table: "Province");

            migrationBuilder.DropIndex(
                name: "IX_District_Name",
                table: "District");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Job");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:Accent", "north,mid,south,other")
                .Annotation("Npgsql:Enum:AccountStatus", "inactive,active,blocked,deleted")
                .Annotation("Npgsql:Enum:Gender", "male,female,other")
                .Annotation("Npgsql:Enum:JobInvitationStatus", "pending,not_accepted,accepted")
                .Annotation("Npgsql:Enum:JobStatus", "pending,processing,finished,deleted")
                .Annotation("Npgsql:Enum:OrderStatus", "pending,processing,finished,rejected")
                .Annotation("Npgsql:Enum:Role", "candidate,enterprise")
                .Annotation("Npgsql:Enum:TransactionType", "receive,send,deposit,refunded,unlock")
                .Annotation("Npgsql:Enum:WorkingStatus", "available,unavailable")
                .OldAnnotation("Npgsql:Enum:Accent", "north,mid,south,other")
                .OldAnnotation("Npgsql:Enum:AccountStatus", "inactive,active,blocked,deleted")
                .OldAnnotation("Npgsql:Enum:Gender", "male,female,other")
                .OldAnnotation("Npgsql:Enum:JobInvitationStatus", "pending,not_accepted,accepted")
                .OldAnnotation("Npgsql:Enum:JobStatus", "pending,processing,finished,deleted")
                .OldAnnotation("Npgsql:Enum:Language", "vietnam,english,other")
                .OldAnnotation("Npgsql:Enum:OrderStatus", "pending,processing,finished,rejected")
                .OldAnnotation("Npgsql:Enum:Role", "candidate,enterprise")
                .OldAnnotation("Npgsql:Enum:TransactionType", "receive,send,deposit,refunded,unlock")
                .OldAnnotation("Npgsql:Enum:WorkingStatus", "available,unavailable");
        }
    }
}
