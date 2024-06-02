using System;
using System.Collections.Generic;
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
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<FieldTrip> FieldTrips { get; set; } = null!;
        public virtual DbSet<FieldTripParticipant> FieldTripParticipants { get; set; } = null!;
        public virtual DbSet<FieldtripAdditionalDetail> FieldtripAdditionalDetails { get; set; } = null!;
        public virtual DbSet<FieldtripDaybyDay> FieldtripDaybyDays { get; set; } = null!;
        public virtual DbSet<FieldtripGettingThere> FieldtripGettingTheres { get; set; } = null!;
        public virtual DbSet<FieldtripInclusion> FieldtripInclusions { get; set; } = null!;
        public virtual DbSet<FieldtripMedia> FieldtripMedia { get; set; } = null!;
        public virtual DbSet<Gallery> Galleries { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Meeting> Meetings { get; set; } = null!;
        public virtual DbSet<MeetingMedia> MeetingMedia { get; set; } = null!;
        public virtual DbSet<MeetingParticipant> MeetingParticipants { get; set; } = null!;
        public virtual DbSet<Member> Members { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot configuration = builder.Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bird>(entity =>
            {
                entity.HasOne(d => d.MemberDetails)
                    .WithMany(p => p.MemberBirds)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bird_Member");
            });

            modelBuilder.Entity<BirdMedia>(entity =>
            {
                entity.HasKey(e => e.PictureId)
                    .HasName("PK__BirdMedi__8C2866D8E5255F8B");

                entity.HasOne(d => d.BirdDetails)
                    .WithMany(p => p.BirdPictures)
                    .HasForeignKey(d => d.BirdId)
                    .HasConstraintName("FK_BirdMedia_Bird");
            });

            modelBuilder.Entity<Blog>(entity =>
            {
                entity.HasOne(d => d.UserDetail)
                    .WithMany(p => p.Blogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Blog_Users");
            });

            modelBuilder.Entity<ClubInformation>(entity =>
            {
                entity.HasKey(e => e.ClubId)
                    .HasName("PK__ClubInfo__DF4AEAB20A84BC20");

                entity.HasOne(d => d.ClubLocation)
                    .WithMany(p => p.ClubInformations)
                    .HasForeignKey(d => d.ClubLocationId)
                    .HasConstraintName("FK_ClubInformation_ClubLocation");
            });

            modelBuilder.Entity<ClubLocation>(entity =>
            {
                entity.HasOne(d => d.Location)
                    .WithMany(p => p.ClubLocations)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_ClubLocation_Location");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(d => d.UserDetail)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Users");
            });

            modelBuilder.Entity<ContestMedia>(entity =>
            {
                entity.HasKey(e => e.PictureId)
                    .HasName("PK__ContestM__769A271AE03B7758");

                entity.HasOne(d => d.ContestDetail)
                    .WithMany(p => p.ContestPictures)
                    .HasForeignKey(d => d.ContestId)
                    .HasConstraintName("FK_Contest");
            });

            modelBuilder.Entity<ContestParticipant>(entity =>
            {
                entity.HasKey(e => new { e.ContestId, e.MemberId })
                    .HasName("PK_ContestParticipant");

                entity.HasOne(d => d.BirdDetails)
                    .WithMany(p => p.ContestParticipants)
                    .HasForeignKey(d => d.BirdId)
                    .HasConstraintName("FK__TournamentP__BID__0E6E26BF");

                entity.HasOne(d => d.ContestDetail)
                    .WithMany(p => p.ContestParticipants)
                    .HasForeignKey(d => d.ContestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TournamentP__TID__0D7A0286");

                entity.HasOne(d => d.MemberDetail)
                    .WithMany(p => p.ContestParticipants)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TournamentP__MID");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasOne(d => d.UserDetail)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedback_Users");
            });

            modelBuilder.Entity<FieldTrip>(entity =>
            {
                entity.HasKey(e => e.TripId)
                    .HasName("PK__FieldTri__C1BEA5A2CBA40722");
            });

            modelBuilder.Entity<FieldTripParticipant>(entity =>
            {
                entity.HasKey(e => new { e.TripId, e.MemberId });

                entity.HasOne(d => d.MemberDetail)
                    .WithMany(p => p.FieldTripParticipants)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldTripParticipants_Member");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.FieldTripParticipants)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldTripParticipants_FieldTrip");
            });

            modelBuilder.Entity<FieldtripAdditionalDetail>(entity =>
            {
                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.FieldtripAdditionalDetails)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldtripAdditionalDetails_Trip");
            });

            modelBuilder.Entity<FieldtripDaybyDay>(entity =>
            {
                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.FieldtripDaybyDays)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldtripDaybyDay_FieldTrip");
            });

            modelBuilder.Entity<FieldtripGettingThere>(entity =>
            {
                entity.HasOne(d => d.Trip)
                    .WithOne(p => p.FieldtripGettingTheres)
                    .HasForeignKey<FieldtripGettingThere>(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldtripGettingThere_FieldTrip");
            });

            modelBuilder.Entity<FieldtripInclusion>(entity =>
            {
                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.FieldtripInclusions)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FieldtripInclusions_FieldTrip");
            });

            modelBuilder.Entity<FieldtripMedia>(entity =>
            {
                entity.HasKey(e => e.PictureId)
                    .HasName("PK__Fieldtri__769A271AD61345EC");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.FieldtripPictures)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK_FieldtripMedia_FieldTrip");
            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.HasOne(d => d.UserDetail)
                    .WithMany(p => p.Galleries)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Gallery_Users");
            });

            modelBuilder.Entity<MeetingMedia>(entity =>
            {
                entity.HasKey(e => e.PictureId)
                    .HasName("PK__MeetingM__769A271AEF8C2935");

                entity.HasOne(d => d.MeetingDetail)
                    .WithMany(p => p.MeetingPictures)
                    .HasForeignKey(d => d.MeetingId)
                    .HasConstraintName("FK_Meeting");
            });

            modelBuilder.Entity<MeetingParticipant>(entity =>
            {
                entity.HasKey(e => new { e.MeetingId, e.MemberId });

                entity.HasOne(d => d.MeetingDetail)
                    .WithMany(p => p.MeetingParticipants)
                    .HasForeignKey(d => d.MeetingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MeetingPar__MeID__03F0984C");

                entity.HasOne(d => d.MemberDetail)
                    .WithMany(p => p.MeetingParticipants)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MeetingParticipant_Member");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasOne(d => d.UserDetail)
                    .WithMany(p => p.NewsDetail)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_News_Users");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(d => d.UserDetail)
                      .WithMany(p => p.Notifications)
                      .HasForeignKey(d => d.UserId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Notification_Users");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                /*entity.HasOne(d => d.UserDetail)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_Users");*/
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(d => d.MemberDetail)
                    .WithOne(p => p.UserDetail)
                    .HasForeignKey<User>(d => d.MemberId)
                    .HasConstraintName("FK_Users_Member");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
