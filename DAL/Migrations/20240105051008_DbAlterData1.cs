using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class DbAlterData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClubInformation",
                keyColumn: "clubId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClubInformation",
                keyColumn: "clubId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClubInformation",
                keyColumn: "clubId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "locationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "locationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "locationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "locationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "locationId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Meeting",
                keyColumn: "meetingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Meeting",
                keyColumn: "meetingId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Meeting",
                keyColumn: "meetingId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Meeting",
                keyColumn: "meetingId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 8);
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 8);

            migrationBuilder.InsertData(
                table: "ClubInformation",
                columns: new[] { "clubId", "clubLocationId", "Description" },
                values: new object[,]
                {
                    { 1, 3, "The ThunderBird Roars!!!, ThunderBird Bird No 1 Fan Club in Da Nang city" },
                    { 2, 1, "ChaoMao Club Main headquarter" },
                    { 3, 4, "WindStrike Bird Fan Club from Ho Chi Minh city" }
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "locationId", "description", "locationName" },
                values: new object[,]
                {
                    { 1, "This is a big city, CS 1", "H22/183,Hoang Van Thai,Thanh Xuan,Hanoi" },
                    { 2, "This is a big city", "42/6,Ha Huy Tap,P3,Da Lat" },
                    { 3, "This is a big city", "7,Quang Trung,Hai Chau,Da Nang" },
                    { 4, "This is a big city, CS 2", "224,Le Van Viet,9,Ho Chi Minh" },
                    { 5, "This is a big city", "23,Nguyen Dinh Chieu,9,Ho Chi Minh" }
                });

            migrationBuilder.InsertData(
                table: "Meeting",
                columns: new[] { "meetingId", "description", "endDate", "host", "incharge", "LocationId", "meetingName", "note", "numberOfParticipants", "registrationDeadline", "startDate" },
                values: new object[,]
                {
                    { 1, "Meet up with new members, exchanging experiences and ideas...Fusce dui est, pellentesque a dolor eu, The main focus of the meetings is birding, thus the meeting location is rotated among good birding locations.", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Connor", "James Howard", 1, "ChaoMaoClub First Anual Meeting", "Everyone interested in birds of the Ha Noi is welcome at our general meetings, whether members of the Chao Mao Club or not.Specifics about upcoming meetings are provided via the Newsletter sent to all members,and are also provided on this web site.", 30, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Meet up with new members, exchanging experiences and ideas...Fusce dui est, pellentesque a dolor eu, The main focus of the meetings is birding, thus the meeting location is rotated among good birding locations.", new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adam Anderson", "Adele Holmes", 2, "ChaoMaoClub Second Anual Meeting", "Everyone interested in birds of the Da Lat City is welcome at our general meetings, whether members of the Chao Mao Club or not.Specifics about upcoming meetings are provided via the Newsletter sent to all members,and are also provided on this web site.", 20, new DateTime(2024, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Meet up with new members, exchanging experiences and ideas...Fusce dui est, pellentesque a dolor eu, The main focus of the meetings is birding, thus the meeting location is rotated among good birding locations.", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyen Van A", "Vuong Cam Tu", 3, "ThunderBird Club First Meeting", "Everyone interested in birds of the Da Nang is welcome at our general meetings, whether members of the Chao Mao Club or not.Specifics about upcoming meetings are provided via the Newsletter sent to all members,and are also provided on this web site.", 10, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Meet up with new members, exchanging experiences and ideas...Fusce dui est, pellentesque a dolor eu, The main focus of the meetings is birding, thus the meeting location is rotated among good birding locations.", new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nguyen Tri Thien", "Ngo Ho Quan", 4, "WindStrike Club First Meeting", "Everyone interested in birds of the HCM city is welcome at our general meetings, whether members of the Chao Mao Club or not.Specifics about upcoming meetings are provided via the Newsletter sent to all members,and are also provided on this web site.", 20, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Member",
                columns: new[] { "memberId", "address", "clubId", "description", "email", "fullName", "gender", "phone", "role", "status", "userName" },
                values: new object[,]
                {
                    { 1, "154/20 Nguyen Kim Street, Ward 6, Dist.10, Ho Chi Minh City", 1, "yo adsjsdajndsjadna", "anguyenvan@gmail.com.vn", "Nguyen Van A", "male", "0838548850", "Manager", "Active", "Nguyen Van A" },
                    { 2, "334 Huynh Tan Phat Street, District 7, Ho Chi Minh City", null, "yo adsjsdajndsjadna", "danecho@gmail.com", "Daniel Echocraft", "male", "0838726767", "Member", "Active", "DanEchocraft" },
                    { 3, "23 Nguyen Bieu Street, Ba Dinh District, Ha Noi", 2, "yo adsjsdajndsjadna", "adminquan@chaomaoclub.com.vn", "Vuong Hai Quan", "male", "0938329397", "Admin", "Active", "Admin" },
                    { 4, "111-E1 Phuong Mai Street, Dong Da District, Ha Noi", 2, "yo adsjsdajndsjadna", "johncon@gmail.com", "John Connor", "male", "0938523649", "Manager", "Active", "JohnCon" },
                    { 5, "14 Nguyen Bieu Street, Ba Dinh District, Ha Noi", 1, "yo adsjsdajndsjadna", "tuvc@gmail.com", "Vuong Cam Tu", "female", "0838548850", "Staff", "Active", "Vuong Cam Tu" },
                    { 6, "28/12, Phan Dinh Giot Street, Ward 2, Dist.Tan Binh, Ho Chi Minh City", 3, "yo adsjsdajndsjadna", "thiennguyen132@gmail.com", "Nguyen Tri Thien", "male", "0938478766", "Manager", "Active", "Nguyen Tri Thien" },
                    { 7, "478 Nguyen Thi Minh Khai Street, Ward 2, District 3, Ho Chi Minh City", 3, "yo adsjsdajndsjadna", "quanNHo145@gmail.com", "Ngo Ho Quan", "male", "0938353577", "Staff", "Active", "Ngo Ho Quan" },
                    { 8, null, null, null, "justym@gmail.com", "Michael Jordan", "male", "01241242141", "Member", "Active", "Michael Jordan" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "userId", "clubId", "memberId", "password", "userName" },
                values: new object[,]
                {
                    { 1, 1, 1, "123", "ANV2024" },
                    { 2, null, 2, "123", "DanEcho" },
                    { 3, 2, 3, "theadmin", "Admin" },
                    { 4, 2, 4, "123", "JConnor" },
                    { 5, 1, 5, "123", "TuVC1010" },
                    { 6, 3, 6, "123", "ThienNTBirdio" },
                    { 7, 3, 7, "123", "QuanNH2024" },
                    { 8, null, 8, "123", "JustYourMan" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClubInformation",
                keyColumn: "clubId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClubInformation",
                keyColumn: "clubId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClubInformation",
                keyColumn: "clubId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "locationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "locationId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "locationId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "locationId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Location",
                keyColumn: "locationId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Meeting",
                keyColumn: "meetingId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Meeting",
                keyColumn: "meetingId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Meeting",
                keyColumn: "meetingId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Meeting",
                keyColumn: "meetingId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "userId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Member",
                keyColumn: "memberId",
                keyValue: 8);
        }
    }
}
