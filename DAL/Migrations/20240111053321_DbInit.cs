using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class DbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    blogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    category = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    uploadDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    vote = table.Column<int>(type: "int", nullable: false),
                    image = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.blogId);
                });

            migrationBuilder.CreateTable(
                name: "ClubInformation",
                columns: table => new
                {
                    clubId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clubLocationId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ClubInfo__DF4AEAB2467C2F83", x => x.clubId);
                });

            migrationBuilder.CreateTable(
                name: "ClubLocation",
                columns: table => new
                {
                    clubLocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    clubName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    locationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubLocation", x => x.clubLocationId);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    commentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    blogId = table.Column<int>(type: "int", nullable: true),
                    vote = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "date", nullable: true),
                    userId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.commentId);
                });

            migrationBuilder.CreateTable(
                name: "Contest",
                columns: table => new
                {
                    contestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contestName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    registrationDeadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    locationId = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    startDate = table.Column<DateTime>(type: "date", nullable: true),
                    endDate = table.Column<DateTime>(type: "date", nullable: true),
                    beforeScore = table.Column<int>(type: "int", nullable: true),
                    afterScore = table.Column<int>(type: "int", nullable: true),
                    fee = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    prize = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    host = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    incharge = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    review = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numberOfParticipants = table.Column<int>(type: "int", nullable: true),
                    clubId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contest", x => x.contestId);
                });

            migrationBuilder.CreateTable(
                name: "ContestScore",
                columns: table => new
                {
                    scoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contestId = table.Column<int>(type: "int", nullable: true),
                    birdId = table.Column<int>(type: "int", nullable: true),
                    memberId = table.Column<int>(type: "int", nullable: true),
                    score = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    scoreDate = table.Column<DateTime>(type: "date", nullable: true),
                    comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ContestS__B56A0C8D230083CD", x => x.scoreId);
                });

            migrationBuilder.CreateTable(
                name: "FieldTrip",
                columns: table => new
                {
                    tripId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tripName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    registrationDeadline = table.Column<DateTime>(type: "date", nullable: true),
                    startDate = table.Column<DateTime>(type: "date", nullable: true),
                    endDate = table.Column<DateTime>(type: "date", nullable: true),
                    locationId = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    fee = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    numberOfParticipants = table.Column<int>(type: "int", nullable: true),
                    host = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    inCharge = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    review = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FieldTri__C1BEA5A2CBA40722", x => x.tripId);
                });

            migrationBuilder.CreateTable(
                name: "Gallery",
                columns: table => new
                {
                    pictureId = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userId = table.Column<int>(type: "int", nullable: true),
                    image = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    locationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    locationName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.locationId);
                });

            migrationBuilder.CreateTable(
                name: "Meeting",
                columns: table => new
                {
                    meetingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    meetingName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    registrationDeadline = table.Column<DateTime>(type: "datetime", nullable: true),
                    startDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    endDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    numberOfParticipants = table.Column<int>(type: "int", nullable: true),
                    host = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    incharge = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    locationId = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meeting", x => x.meetingId);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    memberId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    fullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    userName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    clubId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.memberId);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    newsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    newsContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    uploadDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    picture = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    filepdf = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.newsId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    transactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: true),
                    transactionType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    value = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    paymentDate = table.Column<DateTime>(type: "date", nullable: true),
                    transactionDate = table.Column<DateTime>(type: "date", nullable: true),
                    status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    docNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.transactionId);
                });

            migrationBuilder.CreateTable(
                name: "ContestMedia",
                columns: table => new
                {
                    pictureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contestId = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ContestM__769A271A9B2DCFE0", x => x.pictureId);
                    table.ForeignKey(
                        name: "FK_Contest",
                        column: x => x.contestId,
                        principalTable: "Contest",
                        principalColumn: "contestId");
                });

            migrationBuilder.CreateTable(
                name: "FieldtripDaybyDay",
                columns: table => new
                {
                    tripId = table.Column<int>(type: "int", nullable: false),
                    daybyDayID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    day = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pictureId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_FieldtripDaybyDay_FieldTrip",
                        column: x => x.tripId,
                        principalTable: "FieldTrip",
                        principalColumn: "tripId");
                });

            migrationBuilder.CreateTable(
                name: "FieldtripGettingThere",
                columns: table => new
                {
                    tripId = table.Column<int>(type: "int", nullable: false),
                    gettingThereId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gettingThereText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_FieldtripGettingThere_FieldTrip",
                        column: x => x.tripId,
                        principalTable: "FieldTrip",
                        principalColumn: "tripId");
                });

            migrationBuilder.CreateTable(
                name: "FieldtripInclusions",
                columns: table => new
                {
                    tripId = table.Column<int>(type: "int", nullable: false),
                    inclusionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    inclusionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_FieldtripInclusions_FieldTrip",
                        column: x => x.tripId,
                        principalTable: "FieldTrip",
                        principalColumn: "tripId");
                });

            migrationBuilder.CreateTable(
                name: "FieldtripMedia",
                columns: table => new
                {
                    pictureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tripId = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Fieldtri__769A271A14AF4791", x => x.pictureId);
                    table.ForeignKey(
                        name: "FK_FieldTrip",
                        column: x => x.tripId,
                        principalTable: "FieldTrip",
                        principalColumn: "tripId");
                });

            migrationBuilder.CreateTable(
                name: "FieldTripOverview",
                columns: table => new
                {
                    tripId = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    destination = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    registrationDeadline = table.Column<DateTime>(type: "date", nullable: false),
                    startDate = table.Column<DateTime>(type: "date", nullable: false),
                    endDate = table.Column<DateTime>(type: "date", nullable: false),
                    pictureId = table.Column<int>(type: "int", nullable: true),
                    userReview = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_FieldTripOverview_FieldTrip",
                        column: x => x.tripId,
                        principalTable: "FieldTrip",
                        principalColumn: "tripId");
                });

            migrationBuilder.CreateTable(
                name: "FieldtripRates",
                columns: table => new
                {
                    tripId = table.Column<int>(type: "int", nullable: false),
                    rateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rateType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_FieldtripRates_FieldTrip",
                        column: x => x.tripId,
                        principalTable: "FieldTrip",
                        principalColumn: "tripId");
                });

            migrationBuilder.CreateTable(
                name: "MeetingMedia",
                columns: table => new
                {
                    pictureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    meetingId = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MeetingM__769A271A1B6856EF", x => x.pictureId);
                    table.ForeignKey(
                        name: "FK_Meeting",
                        column: x => x.meetingId,
                        principalTable: "Meeting",
                        principalColumn: "meetingId");
                });

            migrationBuilder.CreateTable(
                name: "Bird",
                columns: table => new
                {
                    birdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    memberId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    birdName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ELO = table.Column<int>(type: "int", nullable: false),
                    age = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addDate = table.Column<DateTime>(type: "date", nullable: true),
                    profilePic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    origin = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bird", x => x.birdId);
                    table.ForeignKey(
                        name: "FK_Bird_Member",
                        column: x => x.memberId,
                        principalTable: "Member",
                        principalColumn: "memberId");
                });

            migrationBuilder.CreateTable(
                name: "FieldTripParticipants",
                columns: table => new
                {
                    tripId = table.Column<int>(type: "int", nullable: false),
                    memberId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    participantNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__FieldTripPa__FID__1332DBDC",
                        column: x => x.tripId,
                        principalTable: "FieldTrip",
                        principalColumn: "tripId");
                    table.ForeignKey(
                        name: "FK_FieldTripParticipants_Member",
                        column: x => x.memberId,
                        principalTable: "Member",
                        principalColumn: "memberId");
                });

            migrationBuilder.CreateTable(
                name: "MeetingParticipant",
                columns: table => new
                {
                    meetingId = table.Column<int>(type: "int", nullable: false),
                    memberId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    participantNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MeetingP__2BA312F592ED3821", x => new { x.meetingId, x.memberId });
                    table.ForeignKey(
                        name: "FK__MeetingPar__MeID__03F0984C",
                        column: x => x.meetingId,
                        principalTable: "Meeting",
                        principalColumn: "meetingId");
                    table.ForeignKey(
                        name: "FK_MeetingParticipant_Member",
                        column: x => x.memberId,
                        principalTable: "Member",
                        principalColumn: "memberId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false),
                    clubId = table.Column<int>(type: "int", nullable: true),
                    memberId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    userName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userId);
                    table.ForeignKey(
                        name: "FK_Users_Member",
                        column: x => x.memberId,
                        principalTable: "Member",
                        principalColumn: "memberId");
                });

            migrationBuilder.CreateTable(
                name: "BirdMedia",
                columns: table => new
                {
                    pictureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    birdId = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BirdMedi__8C2866D8E5255F8B", x => x.pictureId);
                    table.ForeignKey(
                        name: "FK_BirdMedia_Bird",
                        column: x => x.birdId,
                        principalTable: "Bird",
                        principalColumn: "birdId");
                });

            migrationBuilder.CreateTable(
                name: "ContestParticipants",
                columns: table => new
                {
                    contestId = table.Column<int>(type: "int", nullable: true),
                    birdId = table.Column<int>(type: "int", nullable: true),
                    ELO = table.Column<int>(type: "int", nullable: false),
                    participantNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    checkInStatus = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__TournamentP__BID__0E6E26BF",
                        column: x => x.birdId,
                        principalTable: "Bird",
                        principalColumn: "birdId");
                    table.ForeignKey(
                        name: "FK__TournamentP__TID__0D7A0286",
                        column: x => x.contestId,
                        principalTable: "Contest",
                        principalColumn: "contestId");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    feedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date = table.Column<DateTime>(type: "datetime", nullable: true),
                    category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback", x => x.feedbackId);
                    table.ForeignKey(
                        name: "FK_Feedback_Users",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "userId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bird_memberId",
                table: "Bird",
                column: "memberId");

            migrationBuilder.CreateIndex(
                name: "IX_BirdMedia_birdId",
                table: "BirdMedia",
                column: "birdId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestMedia_contestId",
                table: "ContestMedia",
                column: "contestId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestParticipants_birdId",
                table: "ContestParticipants",
                column: "birdId");

            migrationBuilder.CreateIndex(
                name: "IX_ContestParticipants_contestId",
                table: "ContestParticipants",
                column: "contestId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_userId",
                table: "Feedback",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldtripDaybyDay_tripId",
                table: "FieldtripDaybyDay",
                column: "tripId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldtripGettingThere_tripId",
                table: "FieldtripGettingThere",
                column: "tripId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldtripInclusions_tripId",
                table: "FieldtripInclusions",
                column: "tripId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldtripMedia_tripId",
                table: "FieldtripMedia",
                column: "tripId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldTripOverview_tripId",
                table: "FieldTripOverview",
                column: "tripId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldTripParticipants_memberId",
                table: "FieldTripParticipants",
                column: "memberId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldTripParticipants_tripId",
                table: "FieldTripParticipants",
                column: "tripId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldtripRates_tripId",
                table: "FieldtripRates",
                column: "tripId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingMedia_meetingId",
                table: "MeetingMedia",
                column: "meetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingParticipant_memberId",
                table: "MeetingParticipant",
                column: "memberId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_memberId",
                table: "Users",
                column: "memberId",
                unique: true,
                filter: "[memberId] IS NOT NULL");*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropTable(
                name: "BirdMedia");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "ClubInformation");

            migrationBuilder.DropTable(
                name: "ClubLocation");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "ContestMedia");

            migrationBuilder.DropTable(
                name: "ContestParticipants");

            migrationBuilder.DropTable(
                name: "ContestScore");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "FieldtripDaybyDay");

            migrationBuilder.DropTable(
                name: "FieldtripGettingThere");

            migrationBuilder.DropTable(
                name: "FieldtripInclusions");

            migrationBuilder.DropTable(
                name: "FieldtripMedia");

            migrationBuilder.DropTable(
                name: "FieldTripOverview");

            migrationBuilder.DropTable(
                name: "FieldTripParticipants");

            migrationBuilder.DropTable(
                name: "FieldtripRates");

            migrationBuilder.DropTable(
                name: "Gallery");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "MeetingMedia");

            migrationBuilder.DropTable(
                name: "MeetingParticipant");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Bird");

            migrationBuilder.DropTable(
                name: "Contest");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "FieldTrip");

            migrationBuilder.DropTable(
                name: "Meeting");

            migrationBuilder.DropTable(
                name: "Member");*/
        }
    }
}
