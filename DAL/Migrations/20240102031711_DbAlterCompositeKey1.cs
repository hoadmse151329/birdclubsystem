using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class DbAlterCompositeKey1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__MeetingPar__MeID__03F0984C",
                table: "MeetingParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingParticipant_Member",
                table: "MeetingParticipant");

            migrationBuilder.AlterColumn<int>(
                name: "memberId",
                table: "MeetingParticipant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "meetingId",
                table: "MeetingParticipant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__MeetingPar__MeID__03F0984C",
                table: "MeetingParticipant",
                column: "meetingId",
                principalTable: "Meeting",
                principalColumn: "meetingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingParticipant_Member",
                table: "MeetingParticipant",
                column: "memberId",
                principalTable: "Member",
                principalColumn: "memberId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddPrimaryKey(
                name: "PK_Member_Meeting",
                table: "MeetingParticipant",
                columns: new string[]{ "memberId","meetingId"});
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__MeetingPar__MeID__03F0984C",
                table: "MeetingParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingParticipant_Member",
                table: "MeetingParticipant");

            migrationBuilder.AlterColumn<int>(
                name: "memberId",
                table: "MeetingParticipant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "meetingId",
                table: "MeetingParticipant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK__MeetingPar__MeID__03F0984C",
                table: "MeetingParticipant",
                column: "meetingId",
                principalTable: "Meeting",
                principalColumn: "meetingId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingParticipant_Member",
                table: "MeetingParticipant",
                column: "memberId",
                principalTable: "Member",
                principalColumn: "memberId");
            migrationBuilder.DropPrimaryKey(
                name: "PK_Member_Meeting",
                table: "MeetingParticipant");
        }
    }
}
