﻿using System;
using System.Collections.Generic;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DAL.Models
{
    public partial class BirdClubContext : DbContext
    {
        public BirdClubContext()
        {
        }

        public BirdClubContext(DbContextOptions<BirdClubContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bird> Birds { get; set; } = null!;
        public virtual DbSet<BirdMedia> BirdMedia { get; set; } = null!;
        public virtual DbSet<Blog> Blogs { get; set; } = null!;
        public virtual DbSet<ClubInformation> ClubInformations { get; set; } = null!;
        public virtual DbSet<ClubLocation> ClubLocations { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Contest> Contests { get; set; } = null!;
        public virtual DbSet<ContestMedia> ContestMedia { get; set; } = null!;
        public virtual DbSet<ContestParticipant> ContestParticipants { get; set; } = null!;
        public virtual DbSet<ContestScore> ContestScores { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<FieldTrip> FieldTrips { get; set; } = null!;
        public virtual DbSet<FieldTripOverview> FieldTripOverviews { get; set; } = null!;
        public virtual DbSet<FieldTripParticipant> FieldTripParticipants { get; set; } = null!;
        public virtual DbSet<FieldtripDaybyDay> FieldtripDaybyDays { get; set; } = null!;
        public virtual DbSet<FieldtripGettingThere> FieldtripGettingTheres { get; set; } = null!;
        public virtual DbSet<FieldtripInclusion> FieldtripInclusions { get; set; } = null!;
        public virtual DbSet<FieldtripMedia> FieldtripMedia { get; set; } = null!;
        public virtual DbSet<FieldtripRate> FieldtripRates { get; set; } = null!;
        public virtual DbSet<Gallery> Galleries { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Meeting> Meetings { get; set; } = null!;
        public virtual DbSet<MeetingMedia> MeetingMedia { get; set; } = null!;
        public virtual DbSet<MeetingParticipant> MeetingParticipants { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config["ConnectionStrings:DefaultConnection"]);
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bird>(entity =>
            {
                entity.ToTable("Bird");

                entity.HasIndex(e => e.MemberId, "IX_Bird_memberId");

                entity.Property(e => e.BirdId).HasColumnName("birdId");

                entity.Property(e => e.AddDate)
                    .HasColumnType("date")
                    .HasColumnName("addDate");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.BirdName).HasColumnName("birdName");

                entity.Property(e => e.Color).HasColumnName("color");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Elo).HasColumnName("ELO");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.Origin).HasColumnName("origin");

                entity.Property(e => e.ProfilePic).HasColumnName("profilePic");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Birds)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bird_Member");
            });

            modelBuilder.Entity<BirdMedia>(entity =>
            {
                entity.HasKey(e => e.PictureId)
                    .HasName("PK__BirdMedi__8C2866D8E5255F8B");

                entity.HasIndex(e => e.BirdId, "IX_BirdMedia_birdId");

                entity.Property(e => e.PictureId).HasColumnName("pictureId");

                entity.Property(e => e.BirdId).HasColumnName("birdId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.HasOne(d => d.Bird)
                    .WithMany(p => p.BirdMedia)
                    .HasForeignKey(d => d.BirdId)
                    .HasConstraintName("FK_BirdMedia_Bird");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.ToTable("Blog");

                entity.HasIndex(e => e.UserId, "IX_Blog_userId");

                entity.Property(e => e.BlogId).HasColumnName("blogId");

                entity.Property(e => e.Category)
                    .HasMaxLength(255)
                    .HasColumnName("category");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.Property(e => e.UploadDate)
                    .HasColumnType("datetime")
                    .HasColumnName("uploadDate");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Vote).HasColumnName("vote");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Blog_User");
            });

            modelBuilder.Entity<ClubInformation>(entity =>
            {
                entity.HasKey(e => e.ClubId)
                    .HasName("PK__ClubInfo__DF4AEAB2B80BD850");

                entity.ToTable("ClubInformation");

                entity.Property(e => e.ClubId).HasColumnName("clubId");

                entity.Property(e => e.ClubLocationId).HasColumnName("clubLocationId");
            });

            modelBuilder.Entity<ClubLocation>(entity =>
            {
                entity.ToTable("ClubLocation");

                entity.Property(e => e.ClubLocationId).HasColumnName("clubLocationId");

                entity.Property(e => e.ClubName)
                    .HasMaxLength(255)
                    .HasColumnName("clubName");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.LocationId).HasColumnName("locationId");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.HasIndex(e => e.BlogId, "IX_Comment_blogId");

                entity.HasIndex(e => e.UserId, "IX_Comment_userId");

                entity.Property(e => e.CommentId).HasColumnName("commentId");

                entity.Property(e => e.BlogId).HasColumnName("blogId");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("date");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Vote).HasColumnName("vote");

                entity.HasOne(d => d.Blog)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.BlogId)
                    .HasConstraintName("FK_Blog_Comments");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_User_Comments");
            });

            modelBuilder.Entity<Contest>(entity =>
            {
                entity.ToTable("Contest");

                entity.Property(e => e.ContestId).HasColumnName("contestId");

                entity.Property(e => e.AfterScore).HasColumnName("afterScore");

                entity.Property(e => e.BeforeScore).HasColumnName("beforeScore");

                entity.Property(e => e.ClubId).HasColumnName("clubId");

                entity.Property(e => e.ContestName)
                    .HasMaxLength(255)
                    .HasColumnName("contestName");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.Fee)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("fee");

                entity.Property(e => e.Host)
                    .HasMaxLength(100)
                    .HasColumnName("host");

                entity.Property(e => e.Incharge)
                    .HasMaxLength(100)
                    .HasColumnName("incharge");

                entity.Property(e => e.LocationId).HasColumnName("locationId");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.NumberOfParticipants).HasColumnName("numberOfParticipants");

                entity.Property(e => e.Prize)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("prize");

                entity.Property(e => e.RegistrationDeadline)
                    .HasColumnType("datetime")
                    .HasColumnName("registrationDeadline");

                entity.Property(e => e.Review).HasColumnName("review");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<ContestMedia>(entity =>
            {
                entity.HasKey(e => e.PictureId)
                    .HasName("PK__ContestM__769A271A7CF0320F");

                entity.HasIndex(e => e.ContestId, "IX_ContestMedia_contestId");

                entity.Property(e => e.PictureId).HasColumnName("pictureId");

                entity.Property(e => e.ContestId).HasColumnName("contestId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.HasOne(d => d.Contest)
                    .WithMany(p => p.ContestMedia)
                    .HasForeignKey(d => d.ContestId)
                    .HasConstraintName("FK_Contest");
            });

            modelBuilder.Entity<ContestParticipant>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.BirdId, "IX_ContestParticipants_birdId");

                entity.HasIndex(e => e.ContestId, "IX_ContestParticipants_contestId");

                entity.Property(e => e.BirdId).HasColumnName("birdId");

                entity.Property(e => e.CheckInStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("checkInStatus");

                entity.Property(e => e.ContestId).HasColumnName("contestId");

                entity.Property(e => e.Elo).HasColumnName("ELO");

                entity.Property(e => e.ParticipantNo)
                    .HasMaxLength(50)
                    .HasColumnName("participantNo");

                entity.HasOne(d => d.Bird)
                    .WithMany()
                    .HasForeignKey(d => d.BirdId)
                    .HasConstraintName("FK__TournamentP__BID__0E6E26BF");

                entity.HasOne(d => d.Contest)
                    .WithMany()
                    .HasForeignKey(d => d.ContestId)
                    .HasConstraintName("FK__TournamentP__TID__0D7A0286");
            });

            modelBuilder.Entity<ContestScore>(entity =>
            {
                entity.HasKey(e => e.ScoreId)
                    .HasName("PK__ContestS__B56A0C8DD44B44C9");

                entity.ToTable("ContestScore");

                entity.Property(e => e.ScoreId).HasColumnName("scoreId");

                entity.Property(e => e.BirdId).HasColumnName("birdId");

                entity.Property(e => e.Comment).HasColumnName("comment");

                entity.Property(e => e.ContestId).HasColumnName("contestId");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.Score)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("score");

                entity.Property(e => e.ScoreDate)
                    .HasColumnType("date")
                    .HasColumnName("scoreDate");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.FeedbackId).HasColumnName("feedbackId");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .HasColumnName("category");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Detail).HasColumnName("detail");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("userId");
            });

            modelBuilder.Entity<FieldTrip>(entity =>
            {
                entity.HasKey(e => e.TripId)
                    .HasName("PK__FieldTri__C1BEA5A2CBA40722");

                entity.ToTable("FieldTrip");

                entity.Property(e => e.TripId).HasColumnName("tripId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.Fee)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("fee");

                entity.Property(e => e.Host)
                    .HasMaxLength(100)
                    .HasColumnName("host");

                entity.Property(e => e.InCharge)
                    .HasMaxLength(100)
                    .HasColumnName("inCharge");

                entity.Property(e => e.LocationId).HasColumnName("locationId");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.NumberOfParticipants).HasColumnName("numberOfParticipants");

                entity.Property(e => e.RegistrationDeadline)
                    .HasColumnType("date")
                    .HasColumnName("registrationDeadline");

                entity.Property(e => e.Review).HasColumnName("review");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.Property(e => e.TripName)
                    .HasMaxLength(255)
                    .HasColumnName("tripName");
            });

            modelBuilder.Entity<FieldTripOverview>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("FieldTripOverview");

                entity.HasIndex(e => e.TripId, "IX_FieldTripOverview_tripId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Destination)
                    .HasMaxLength(255)
                    .HasColumnName("destination");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("endDate");

                entity.Property(e => e.PictureId).HasColumnName("pictureId");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.RegistrationDeadline)
                    .HasColumnType("date")
                    .HasColumnName("registrationDeadline");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("startDate");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.Property(e => e.TripId).HasColumnName("tripId");

                entity.Property(e => e.UserReview).HasColumnName("userReview");

                entity.HasOne(d => d.Trip)
                    .WithMany()
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldTripOverview_FieldTrip");
            });

            modelBuilder.Entity<FieldTripParticipant>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.MemberId, "IX_FieldTripParticipants_memberId");

                entity.HasIndex(e => e.TripId, "IX_FieldTripParticipants_tripId");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.ParticipantNo)
                    .HasMaxLength(50)
                    .HasColumnName("participantNo");

                entity.Property(e => e.TripId).HasColumnName("tripId");

                entity.HasOne(d => d.Member)
                    .WithMany()
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldTripParticipants_Member");

                entity.HasOne(d => d.Trip)
                    .WithMany()
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FieldTripPa__FID__1332DBDC");
            });

            modelBuilder.Entity<FieldtripDaybyDay>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("FieldtripDaybyDay");

                entity.HasIndex(e => e.TripId, "IX_FieldtripDaybyDay_tripId");

                entity.Property(e => e.Day).HasColumnName("day");

                entity.Property(e => e.DaybyDayId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("daybyDayID");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.PictureId).HasColumnName("pictureId");

                entity.Property(e => e.TripId).HasColumnName("tripId");

                entity.HasOne(d => d.Trip)
                    .WithMany()
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldtripDaybyDay_FieldTrip");
            });

            modelBuilder.Entity<FieldtripGettingThere>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("FieldtripGettingThere");

                entity.HasIndex(e => e.TripId, "IX_FieldtripGettingThere_tripId");

                entity.Property(e => e.GettingThereId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("gettingThereId");

                entity.Property(e => e.GettingThereText).HasColumnName("gettingThereText");

                entity.Property(e => e.TripId).HasColumnName("tripId");

                entity.HasOne(d => d.Trip)
                    .WithMany()
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldtripGettingThere_FieldTrip");
            });

            modelBuilder.Entity<FieldtripInclusion>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.TripId, "IX_FieldtripInclusions_tripId");

                entity.Property(e => e.InclusionId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("inclusionId");

                entity.Property(e => e.InclusionText).HasColumnName("inclusionText");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .HasColumnName("title");

                entity.Property(e => e.TripId).HasColumnName("tripId");

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .HasColumnName("type");

                entity.HasOne(d => d.Trip)
                    .WithMany()
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldtripInclusions_FieldTrip");
            });

            modelBuilder.Entity<FieldtripMedia>(entity =>
            {
                entity.HasKey(e => e.PictureId)
                    .HasName("PK__Fieldtri__769A271A7C84EFD2");

                entity.HasIndex(e => e.TripId, "IX_FieldtripMedia_tripId");

                entity.Property(e => e.PictureId).HasColumnName("pictureId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.TripId).HasColumnName("tripId");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.FieldtripMedia)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK_FieldTrip");
            });

            modelBuilder.Entity<FieldtripRate>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.TripId, "IX_FieldtripRates_tripId");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");

                entity.Property(e => e.RateId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("rateId");

                entity.Property(e => e.RateType)
                    .HasMaxLength(50)
                    .HasColumnName("rateType");

                entity.Property(e => e.TripId).HasColumnName("tripId");

                entity.HasOne(d => d.Trip)
                    .WithMany()
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldtripRates_FieldTrip");
            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Gallery");

                entity.HasIndex(e => e.UserId, "IX_Gallery_userId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.PictureId).HasColumnName("pictureId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Gallery_User");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.LocationId).HasColumnName("locationId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.LocationName)
                    .HasMaxLength(255)
                    .HasColumnName("locationName");
            });

            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.ToTable("Meeting");

                entity.Property(e => e.MeetingId).HasColumnName("meetingId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("endDate");

                entity.Property(e => e.Host)
                    .HasMaxLength(100)
                    .HasColumnName("host");

                entity.Property(e => e.Incharge)
                    .HasMaxLength(100)
                    .HasColumnName("incharge");

                entity.Property(e => e.MeetingName)
                    .HasMaxLength(255)
                    .HasColumnName("meetingName");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.NumberOfParticipants).HasColumnName("numberOfParticipants");

                entity.Property(e => e.RegistrationDeadline)
                    .HasColumnType("datetime")
                    .HasColumnName("registrationDeadline");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("startDate");
            });

            modelBuilder.Entity<MeetingMedia>(entity =>
            {
                entity.HasKey(e => e.PictureId)
                    .HasName("PK__MeetingM__769A271A8E424EAF");

                entity.HasIndex(e => e.MeetingId, "IX_MeetingMedia_meetingId");

                entity.Property(e => e.PictureId).HasColumnName("pictureId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.MeetingId).HasColumnName("meetingId");

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.MeetingMedia)
                    .HasForeignKey(d => d.MeetingId)
                    .HasConstraintName("FK_Meeting");
            });

            modelBuilder.Entity<MeetingParticipant>(entity =>
            {

                entity.ToTable("MeetingParticipant");

                entity.HasIndex(e => e.MeetingId, "IX_MeetingParticipant_meetingId");

                entity.HasIndex(e => e.MemberId, "IX_MeetingParticipant_memberId");

                entity.Property(e => e.MeetingId).HasColumnName("meetingId");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.ParticipantNo)
                    .HasMaxLength(50)
                    .HasColumnName("participantNo");

                entity.HasOne(d => d.Meeting)
                    .WithMany()
                    .HasForeignKey(d => d.MeetingId)
                    .HasConstraintName("FK__MeetingPar__MeID__03F0984C");

                entity.HasOne(d => d.Member)
                    .WithMany()
                    .HasForeignKey(d => d.MemberId)
                    .HasConstraintName("FK_MeetingParticipant_Member");
                entity.HasKey(e => new { e.MeetingId, e.MemberId});
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.ToTable("Member");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.ClubId).HasColumnName("clubId");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .HasColumnName("fullName");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .HasColumnName("gender");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_News_userId");

                entity.Property(e => e.NewsId).HasColumnName("newsId");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .HasColumnName("category");

                entity.Property(e => e.Filepdf)
                    .IsUnicode(false)
                    .HasColumnName("filepdf");

                entity.Property(e => e.NewsContent).HasColumnName("newsContent");

                entity.Property(e => e.Picture)
                    .IsUnicode(false)
                    .HasColumnName("picture");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.UploadDate)
                    .HasColumnType("datetime")
                    .HasColumnName("uploadDate");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_News_User");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.TransactionId).HasColumnName("transactionId");

                entity.Property(e => e.DocNo)
                    .HasMaxLength(255)
                    .HasColumnName("docNo");

                entity.Property(e => e.PaymentDate)
                    .HasColumnType("date")
                    .HasColumnName("paymentDate");

                entity.Property(e => e.Status)
                    .HasMaxLength(255)
                    .HasColumnName("status");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("date")
                    .HasColumnName("transactionDate");

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(255)
                    .HasColumnName("transactionType");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("value");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.MemberId, "IX_User_memberId")
                    .IsUnique()
                    .HasFilter("([memberId] IS NOT NULL)");

                entity.HasIndex(e => e.UserName, "UQ__User__66DCF95CC4AB3072")
                    .IsUnique()
                    .HasFilter("([userName] IS NOT NULL)");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.ClubId).HasColumnName("clubId");

                entity.Property(e => e.MemberId).HasColumnName("memberId");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("userName");

                entity.HasOne(d => d.Member)
                    .WithOne(p => p.User)
                    .HasForeignKey<User>(d => d.MemberId)
                    .HasConstraintName("FK_User_Member");
            });

            #region DataSeederCall
            modelBuilder.SeedLocations();
            modelBuilder.SeedClubInfos();
            modelBuilder.SeedMeetings();
            modelBuilder.SeedMembers();
            modelBuilder.SeedUsers();
            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
