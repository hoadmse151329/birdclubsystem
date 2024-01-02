using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class DbAlterCompositeKey3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingParticipant",
                table: "MeetingParticipant",
                columns: new[] { "meetingId", "memberId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MeetingParticipant",
                table: "MeetingParticipant");
        }
    }
}
