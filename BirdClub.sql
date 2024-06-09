USE [master]
GO
/****** Object:  Database [BirdClub]    Script Date: 6/9/2024 5:52:46 AM ******/
DROP DATABASE [BirdClub]
GO
/****** Object:  Database [BirdClub]    Script Date: 6/9/2024 5:52:46 AM ******/
CREATE DATABASE [BirdClub]
GO

USE [BirdClub]
GO
/****** Object:  User [duy]    Script Date: 6/9/2024 5:52:46 AM ******/
CREATE TABLE [dbo].[Bird](
	[birdId] [int] IDENTITY(1,1) NOT NULL,
	[memberId] [nvarchar](255) NULL,
	[birdName] [nvarchar](max) NULL,
	[ELO] [int] NULL,
	[age] [int] NULL,
	[description] [nvarchar](max) NULL,
	[color] [nvarchar](max) NULL,
	[addDate] [datetime] NULL,
	[profilePic] [nvarchar](max) NULL,
	[status] [nvarchar](50) NULL,
	[origin] [nvarchar](max) NULL,
 CONSTRAINT [PK__Bird__C6DE0D21564B20FB] PRIMARY KEY CLUSTERED 
(
	[birdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BirdMedia]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BirdMedia](
	[pictureId] [int] IDENTITY(1,1) NOT NULL,
	[birdId] [int] NULL,
	[description] [nvarchar](max) NULL,
	[image] [nvarchar](max) NULL,
	[type] [nvarchar](50) NULL,
 CONSTRAINT [PK__BirdMedi__8C2866D8E5255F8B] PRIMARY KEY CLUSTERED 
(
	[pictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[blogId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[description] [nvarchar](max) NULL,
	[category] [nvarchar](255) NULL,
	[uploadDate] [datetime] NULL,
	[vote] [int] NULL,
	[image] [varchar](max) NULL,
	[status] [nvarchar](20) NULL,
 CONSTRAINT [PK__Blog__3B7E5742B9C74200] PRIMARY KEY CLUSTERED 
(
	[blogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClubInformation]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClubInformation](
	[clubId] [int] IDENTITY(1,1) NOT NULL,
	[clubLocationId] [int] NULL,
	[description] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[clubId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClubLocation]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClubLocation](
	[clubLocationId] [int] IDENTITY(1,1) NOT NULL,
	[clubName] [nvarchar](255) NULL,
	[description] [nvarchar](max) NULL,
	[locationId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[clubLocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[commentId] [int] IDENTITY(1,1) NOT NULL,
	[blogId] [int] NULL,
	[vote] [int] NULL,
	[description] [nvarchar](max) NULL,
	[date] [datetime] NULL,
	[userId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[commentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contest]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contest](
	[contestId] [int] IDENTITY(1,1) NOT NULL,
	[contestName] [nvarchar](255) NULL,
	[description] [nvarchar](max) NULL,
	[openRegistration] [datetime] NULL,
	[registrationDeadline] [datetime] NULL,
	[locationId] [int] NULL,
	[status] [nvarchar](20) NULL,
	[startDate] [datetime] NULL,
	[endDate] [datetime] NULL,
	[reqMinELO] [int] NULL,
	[reqMaxELO] [int] NULL,
	[afterELO] [int] NULL,
	[fee] [int] NULL,
	[prize] [int] NULL,
	[host] [nvarchar](100) NULL,
	[incharge] [nvarchar](100) NULL,
	[note] [nvarchar](max) NULL,
	[review] [nvarchar](max) NULL,
	[numberOfParticipants] [int] NULL,
	[clubId] [int] NULL,
	[numberOfParticipantsMinReq] [int] NULL,
	[numberOfParticipantsLimit] [int] NULL,
 CONSTRAINT [PK__Tourname__C456D729169E357B] PRIMARY KEY CLUSTERED 
(
	[contestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContestMedia]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContestMedia](
	[pictureId] [int] IDENTITY(1,1) NOT NULL,
	[contestId] [int] NOT NULL,
	[description] [nvarchar](max) NULL,
	[image] [nvarchar](max) NULL,
	[type] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[pictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContestParticipants]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContestParticipants](
	[contestId] [int] NOT NULL,
	[memberId] [nvarchar](255) NOT NULL,
	[birdId] [int] NULL,
	[ELO] [int] NULL,
	[score] [int] NULL,
	[participantNo] [int] NULL,
	[checkInStatus] [nvarchar](50) NULL,
 CONSTRAINT [PK_ContestParticipant] PRIMARY KEY CLUSTERED 
(
	[contestId] ASC,
	[memberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[feedbackId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[eventId] [nvarchar](50) NULL,
	[title] [nvarchar](255) NULL,
	[details] [nvarchar](max) NULL,
	[date] [datetime] NULL,
	[category] [nvarchar](50) NULL,
	[rating] [decimal](5, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[feedbackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldTrip]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldTrip](
	[tripId] [int] IDENTITY(1,1) NOT NULL,
	[tripName] [nvarchar](255) NULL,
	[description] [nvarchar](max) NULL,
	[details] [nvarchar](max) NULL,
	[openRegistration] [datetime] NULL,
	[registrationDeadline] [datetime] NULL,
	[startDate] [datetime] NULL,
	[endDate] [datetime] NULL,
	[locationId] [int] NULL,
	[status] [nvarchar](50) NULL,
	[numberOfParticipants] [int] NULL,
	[numberOfParticipantsMinReq] [int] NULL,
	[numberOfParticipantsLimit] [int] NULL,
	[fee] [int] NULL,
	[host] [nvarchar](100) NULL,
	[inCharge] [nvarchar](100) NULL,
	[note] [nvarchar](max) NULL,
 CONSTRAINT [PK__FieldTri__C1BEA5A2CBA40722] PRIMARY KEY CLUSTERED 
(
	[tripId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldTripAdditionalDetails]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldTripAdditionalDetails](
	[tripDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[tripId] [int] NULL,
	[title] [nvarchar](50) NULL,
	[description] [nvarchar](max) NULL,
	[type] [nvarchar](50) NULL,
 CONSTRAINT [PK_FieldTripAdditionalDetails] PRIMARY KEY CLUSTERED 
(
	[tripDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldTripDaybyDay]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldTripDaybyDay](
	[tripId] [int] NULL,
	[dayByDayId] [int] IDENTITY(1,1) NOT NULL,
	[day] [int] NULL,
	[description] [nvarchar](max) NULL,
	[mainDestination] [nvarchar](100) NULL,
	[accommodation] [nvarchar](255) NULL,
	[mealsAndDrinks] [nvarchar](255) NULL,
 CONSTRAINT [PK_FieldTripDaybyDay] PRIMARY KEY CLUSTERED 
(
	[dayByDayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldTripGettingThere]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldTripGettingThere](
	[tripId] [int] NULL,
	[gettingThereId] [int] IDENTITY(1,1) NOT NULL,
	[gettingThereStartEnd] [nvarchar](max) NULL,
	[gettingThereFlight] [nvarchar](max) NULL,
	[gettingThereTransportation] [nvarchar](max) NULL,
	[gettingThereAccommodation] [nvarchar](max) NULL,
 CONSTRAINT [PK_FieldTripGettingThere] PRIMARY KEY CLUSTERED 
(
	[gettingThereId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldTripInclusions]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldTripInclusions](
	[tripId] [int] NULL,
	[inclusionId] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NULL,
	[inclusionText] [nvarchar](max) NULL,
	[type] [nvarchar](50) NULL,
	[inclusiontype] [nvarchar](50) NULL,
 CONSTRAINT [PK_FieldTripInclusions] PRIMARY KEY CLUSTERED 
(
	[inclusionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldTripMedia]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldTripMedia](
	[pictureId] [int] IDENTITY(1,1) NOT NULL,
	[tripId] [int] NULL,
	[description] [nvarchar](max) NULL,
	[image] [nvarchar](max) NULL,
	[type] [nvarchar](50) NULL,
	[dayByDayId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[pictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldTripParticipants]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldTripParticipants](
	[tripId] [int] NOT NULL,
	[memberId] [nvarchar](255) NOT NULL,
	[participantNo] [int] NULL,
	[checkInStatus] [nvarchar](50) NULL,
 CONSTRAINT [PK_FieldTripParticipants] PRIMARY KEY CLUSTERED 
(
	[tripId] ASC,
	[memberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gallery]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gallery](
	[pictureId] [int] IDENTITY(1,1) NOT NULL,
	[description] [nvarchar](max) NULL,
	[userId] [int] NULL,
	[image] [varchar](max) NULL,
 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
(
	[pictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[locationId] [int] IDENTITY(1,1) NOT NULL,
	[locationName] [nvarchar](255) NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK__Location__C6555721B7AAE234] PRIMARY KEY CLUSTERED 
(
	[locationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meeting]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meeting](
	[meetingId] [int] IDENTITY(1,1) NOT NULL,
	[meetingName] [nvarchar](255) NULL,
	[description] [nvarchar](max) NULL,
	[openRegistration] [datetime] NULL,
	[registrationDeadline] [datetime] NULL,
	[startDate] [datetime] NULL,
	[endDate] [datetime] NULL,
	[numberOfParticipants] [int] NULL,
	[host] [nvarchar](100) NULL,
	[incharge] [nvarchar](100) NULL,
	[locationId] [int] NULL,
	[status] [nvarchar](20) NULL,
	[note] [nvarchar](max) NULL,
	[numberOfParticipantsMinReq] [int] NULL,
	[numberOfParticipantsLimit] [int] NULL,
 CONSTRAINT [PK__Meeting__1234DA4418E1FF0D] PRIMARY KEY CLUSTERED 
(
	[meetingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingMedia]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingMedia](
	[pictureId] [int] IDENTITY(1,1) NOT NULL,
	[meetingId] [int] NULL,
	[description] [nvarchar](max) NULL,
	[image] [nvarchar](max) NULL,
	[type] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[pictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingParticipant]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingParticipant](
	[meetingId] [int] NOT NULL,
	[memberId] [nvarchar](255) NOT NULL,
	[participantNo] [int] NULL,
	[checkInStatus] [nvarchar](50) NULL,
 CONSTRAINT [PK_MeetingParticipant] PRIMARY KEY CLUSTERED 
(
	[meetingId] ASC,
	[memberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[memberId] [nvarchar](255) NOT NULL,
	[fullName] [nvarchar](255) NULL,
	[userName] [nvarchar](50) NULL,
	[email] [nvarchar](255) NULL,
	[gender] [nvarchar](10) NULL,
	[role] [nvarchar](50) NULL,
	[address] [nvarchar](max) NULL,
	[phone] [nvarchar](20) NULL,
	[description] [nvarchar](max) NULL,
	[status] [nvarchar](50) NULL,
	[registerDate] [datetime] NULL,
	[joinDate] [datetime] NULL,
	[expiryDate] [datetime] NULL,
	[clubId] [int] NULL,
 CONSTRAINT [PK__Member__0CF04B18EBA8FD9D] PRIMARY KEY CLUSTERED 
(
	[memberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[newsId] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NULL,
	[category] [nvarchar](50) NULL,
	[newsContent] [nvarchar](max) NULL,
	[uploadDate] [datetime] NULL,
	[status] [nvarchar](20) NULL,
	[picture] [varchar](max) NULL,
	[userId] [int] NULL,
 CONSTRAINT [PK__News__5218041EBB60A1A2] PRIMARY KEY CLUSTERED 
(
	[newsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[notificationId] [nvarchar](255) NOT NULL,
	[title] [nvarchar](255) NULL,
	[description] [nvarchar](max) NULL,
	[date] [datetime] NULL,
	[userId] [int] NULL,
	[status] [nvarchar](50) NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[notificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[transactionId] [int] IDENTITY(1,1) NOT NULL,
	[vnPayId] [nvarchar](255) NULL,
	[userId] [int] NULL,
	[transactionType] [nvarchar](255) NULL,
	[value] [int] NULL,
	[transactionDate] [datetime] NULL,
	[paymentDate] [datetime] NULL,
	[status] [nvarchar](255) NULL,
	[docNo] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[transactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/9/2024 5:52:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[clubId] [int] NULL,
	[imagePath] [nvarchar](255) NULL,
	[memberId] [nvarchar](255) NULL,
	[userName] [nvarchar](255) NULL,
	[password] [nvarchar](255) NULL,
	[role] [nvarchar](50) NULL,
 CONSTRAINT [PK__Users__CB9A1CFFCCE22DCC] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bird] ON 

INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (1, N'1', N'Meo', 1600, 2, N'A colorful and melodious bird with a distinctive red patch on its cheeks.', N'Red, Black, and White', CAST(N'2024-01-15T10:23:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Northern India')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (2, N'2', N'Chip', 1500, 1, N'A young Red-whiskered Bulbul, still acquiring its adult plumage.', N'Brown and White', CAST(N'2024-02-15T11:45:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Nepal')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (3, N'3', N'Lua', 1750, 4, N'A mature Red-whiskered Bulbul singing melodiously in a garden.', N'Red, Black, and White', CAST(N'2024-03-15T09:12:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Nepal')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (4, N'4', N'Gao', 1550, 3, N'A Red-whiskered Bulbul perched on a branch with a backdrop of lush green foliage.', N'Red, Black, and White', CAST(N'2024-04-15T13:34:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'South-Eastern China')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (5, N'5', N'Soc', 1650, 2, N'A Red-whiskered Bulbul pair in a courtship display.', N'Red, Black, and White', CAST(N'2024-05-15T14:56:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'South-Western Thailand')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (6, N'6', N'Bac', 1700, 3, N'A Red-whiskered Bulbul feeding on fruits in a tree.', N'Red, Black, and White', CAST(N'2024-05-15T08:21:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Northern India')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (7, N'7', N'Mo', 1600, 2, N'A Red-whiskered Bulbul bathing in a bird bath.', N'Red, Black, and White', CAST(N'2024-04-15T16:45:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Northern Myanmar')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (8, N'8', N'Đau', 1550, 1, N'A juvenile Red-whiskered Bulbul exploring its surroundings.', N'Brown and White', CAST(N'2024-05-15T09:23:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'South-Eastern China')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (9, N'9', N'Bay', 1800, 5, N'A Red-whiskered Bulbul building a nest in a tree.', N'Red, Black, and White', CAST(N'2024-03-15T10:34:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'South-Western Thailand')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (10, N'10', N'Tao', 1650, 3, N'A Red-whiskered Bulbul perched on a wire with a backdrop of clear blue sky.', N'Red, Black, and White', CAST(N'2024-02-15T11:45:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'South-Eastern China')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (11, N'1', N'Coconut', 1600, 2, N'A Coconut Bulbul perched on a palm tree, enjoying the tropical breeze.', N'White and Brown', CAST(N'2024-01-18T09:12:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (12, N'1', N'Rocky', 1550, 3, N'A Rocky Bulbul exploring the rocky terrain of a mountain slope.', N'Gray and Brown', CAST(N'2024-01-20T14:56:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (13, N'2', N'Sparky', 1650, 4, N'A Sparky Bulbul perched on a wire, watching over its nest in a telephone pole.', N'Yellow and Black', CAST(N'2024-02-18T08:21:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (14, N'2', N'Lucky', 1700, 2, N'A Lucky Bulbul foraging for food amidst fallen leaves in a forest.', N'Green and Brown', CAST(N'2024-02-20T16:45:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (15, N'3', N'Buddy', 1750, 3, N'A Buddy Bulbul perched on a fence, greeting passersby with its cheerful chirps.', N'Gray and White', CAST(N'2024-03-18T11:34:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (16, N'3', N'Lily', 1680, 2, N'A Bulbul fluttering among blossoming flowers in a garden.', N'Yellow and White', CAST(N'2024-03-20T09:12:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (17, N'4', N'Bella', 1720, 4, N'A Bella Bulbul perched atop a wooden birdhouse, surveying its territory.', N'Brown and White', CAST(N'2024-04-18T16:34:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (18, N'4', N'Vivi', 1630, 3, N'A Rocky Bulbul chirping melodiously from a branch, filling the air with sweet music.', N'Gray and Brown', CAST(N'2024-04-20T10:21:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (19, N'5', N'Charlie', 1700, 5, N'A Charlie Bulbul perched on a signpost, announcing its presence to the world.', N'Black and White', CAST(N'2024-05-18T11:12:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (20, N'5', N'Max', 1650, 2, N'A Max Bulbul preening its feathers with meticulous care on a sunny day.', N'Black and Yellow', CAST(N'2024-05-20T14:21:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (21, N'6', N'Khoi', 1680, 2, N'A small Khoi Bulbul perched on a branch, singing in the forest.', N'Yellow and White', CAST(N'2024-05-18T13:45:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (22, N'6', N'Ho', 1720, 4, N'A Ho Bulbul enjoying water from a pouring jug.', N'Yellow and White', CAST(N'2024-05-18T17:45:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (23, N'7', N'Muop', 1630, 3, N'A tiny Muop Bulbul sleeping in a nest on an oak tree.', N'Green and White', CAST(N'2024-04-16T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (24, N'7', N'Tao', 1700, 5, N'A tao Bulbul searching for food under the sunlight.', N'Orange and Black', CAST(N'2024-04-18T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (25, N'8', N'Tom', 1750, 2, N'A Tom Bulbul building a nest on a tall tree.', N'Purple and White', CAST(N'2024-04-20T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (26, N'8', N'Daisy', 1670, 3, N'A Daisy Bulbul singing loudly amidst a vibrant natural backdrop.', N'Black and White', CAST(N'2024-04-22T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (27, N'9', N'Ngao', 1710, 4, N'A Rocky Bulbul resting on a branch of bright red flowers.', N'Pink and White', CAST(N'2024-04-24T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (28, N'9', N'Whiskers', 1650, 2, N'A Whiskers Bulbul flying gracefully among white clouds in the sky.', N'Gray and White', CAST(N'2024-04-26T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (29, N'10', N'Smudge', 1690, 3, N'A Smudge Bulbul enjoying the feeling of freedom on a tree branch.', N'Green and White', CAST(N'2024-04-28T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (30, N'10', N'Biscuit', 1730, 4, N'A Biscuit Bulbul caring for its young in the nest.', N'Deep Pink and White', CAST(N'2024-04-30T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (31, N'11', N'Pepper', 1680, 2, N'A small Pepper Bulbul perched on a branch, singing in the forest.', N'Yellow and White', CAST(N'2024-04-12T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao1.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (32, N'11', N'Cocoa', 1720, 4, N'A Cocoa Bulbul enjoying water from a pouring jug.', N'Yellow and White', CAST(N'2024-04-14T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao6.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (33, N'12', N'Hazel', 1630, 3, N'A tiny Hazel Bulbul sleeping in a nest on an oak tree.', N'Green and White', CAST(N'2024-04-16T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao4.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (34, N'12', N'Oreo', 1700, 5, N'A Oreo Bulbul searching for food under the sunlight.', N'Orange and Black', CAST(N'2024-04-18T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/d306d5e1-7e3e-41ea-877e-8fddb7906f70-chaomao2.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (35, N'13', N'Felix', 1750, 2, N'A Felix Bulbul building a nest on a tall tree.', N'Purple and White', CAST(N'2024-04-20T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao7.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (36, N'13', N'Ginger', 1670, 3, N'A Ginger Bulbul singing loudly amidst a vibrant natural backdrop.', N'Black and White', CAST(N'2024-04-22T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (37, N'14', N'Mittens', 1710, 4, N'A Mittens Bulbul resting on a branch of bright red flowers.', N'Pink and White', CAST(N'2024-04-24T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao6.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (38, N'14', N'Freckles', 1650, 2, N'A Freckles Bulbul flying gracefully among white clouds in the sky.', N'Gray and White', CAST(N'2024-04-26T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao5.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (39, N'14', N'Bella', 1680, 2, N'A Bella Bulbul perched on a branch, singing in the forest.', N'Yellow and White', CAST(N'2024-04-12T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao1.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (40, N'14', N'Rosie', 1720, 4, N'A Rosie Bulbul enjoying water from a pouring jug.', N'Yellow and White', CAST(N'2024-04-14T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao3.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (41, N'13', N'Socks', 1630, 3, N'A tiny Socks Bulbul sleeping in a nest on an oak tree.', N'Green and White', CAST(N'2024-04-16T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/d306d5e1-7e3e-41ea-877e-8fddb7906f70-chaomao2.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (42, N'12', N'Cooper', 1700, 5, N'A Cooper Bulbul searching for food under the sunlight.', N'Orange and Black', CAST(N'2024-04-18T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao7.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (43, N'15', N'Boots', 1750, 2, N'A Boots Bulbul building a nest on a tall tree.', N'Purple and White', CAST(N'2024-04-20T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao7.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (44, N'15', N'Luna', 1670, 3, N'A Luna Bulbul singing loudly amidst a vibrant natural backdrop.', N'Black and White', CAST(N'2024-04-22T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao1.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (45, N'16', N'Milo', 1710, 4, N'A Milo Bulbul resting on a branch of bright red flowers.', N'Pink and White', CAST(N'2024-04-24T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao6.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (46, N'17', N'Tiger', 1650, 2, N'A Tiger Bulbul flying gracefully among white clouds in the sky.', N'Gray and White', CAST(N'2024-04-26T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao5.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (47, N'18', N'Chloe', 1690, 3, N'A Chloe Bulbul enjoying the feeling of freedom on a tree branch.', N'Green and White', CAST(N'2024-04-28T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (48, N'16', N'Mia', 1730, 4, N'A Buddy Bulbul caring for its young in the nest.', N'Deep Pink and White', CAST(N'2024-04-30T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao4.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (49, N'17', N'Toby', 1680, 2, N'A Toby Bulbul perched on a branch, singing in the forest.', N'Yellow and White', CAST(N'2024-04-12T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao6.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (50, N'18', N'Molly', 1720, 4, N'A Molly Bulbul enjoying water from a pouring jug.', N'Yellow and White', CAST(N'2024-04-14T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/d306d5e1-7e3e-41ea-877e-8fddb7906f70-chaomao2.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (51, N'14', N'Max', 1630, 3, N'A tiny Max Bulbul sleeping in a nest on an oak tree.', N'Green and White', CAST(N'2024-04-16T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao7.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (52, N'19', N'Chuot', 1600, 2, N'A Chuot Bulbul chirping happily in the early morning sun.', N'Yellow and Brown', CAST(N'2024-05-01T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao1.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (53, N'19', N'Tim', 1580, 4, N'Tim Bulbul flying over a serene lake at sunset.', N'Blue and White', CAST(N'2024-05-02T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao5.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (54, N'19', N'Leo', 1620, 3, N'Leo Bulbul perched on a blossoming cherry tree.', N'Pink and Grey', CAST(N'2024-05-03T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (55, N'19', N'Nhim', 1610, 5, N'Nhim Bulbul singing from a bamboo stalk.', N'Green and Yellow', CAST(N'2024-05-04T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao4.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (56, N'19', N'Cam', 1590, 2, N'Cam Bulbul nesting in a palm tree.', N'Orange and Brown', CAST(N'2024-05-05T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao3.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (57, N'18', N'Rong', 1635, 4, N'Rong Bulbul watching over its territory from a high branch.', N'Red and Black', CAST(N'2024-05-06T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao7.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (58, N'18', N'Soi', 1625, 3, N'Soi Bulbul catching insects mid-air.', N'Grey and White', CAST(N'2024-05-07T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/d306d5e1-7e3e-41ea-877e-8fddb7906f70-chaomao2.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (59, N'18', N'De', 1615, 2, N'De Bulbul playing with other birds in the garden.', N'Yellow and Green', CAST(N'2024-05-08T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao6.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (60, N'17', N'Ga', 1605, 5, N'Ga Bulbul bathing in a stream.', N'Blue and Brown', CAST(N'2024-05-09T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao3.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (61, N'17', N'Voi', 1595, 3, N'Voi Bulbul building its nest.', N'Black and White', CAST(N'2024-05-10T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao7.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (62, N'17', N'Tiger', 1640, 4, N'Tiger Bulbul hunting for food in the forest.', N'Red and Yellow', CAST(N'2024-05-11T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/d306d5e1-7e3e-41ea-877e-8fddb7906f70-chaomao2.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (63, N'16', N'Map', 1630, 3, N'Map Bulbul calling out to its mate.', N'Green and White', CAST(N'2024-05-12T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao5.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (64, N'16', N'Lon', 1620, 2, N'Lon Bulbul foraging in the underbrush.', N'Brown and Yellow', CAST(N'2024-05-13T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/d306d5e1-7e3e-41ea-877e-8fddb7906f70-chaomao2.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (65, N'16', N'Beo', 1610, 5, N'Beo Bulbul resting in the shade.', N'Grey and Black', CAST(N'2024-05-14T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao6.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (66, N'15', N'Bao', 1600, 3, N'Bao Bulbul enjoying a fruit.', N'Green and Red', CAST(N'2024-05-15T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao1.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (67, N'15', N'Quy', 1650, 4, N'Quy Bulbul perched on a lotus flower.', N'White and Pink', CAST(N'2024-05-16T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao5.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (68, N'15', N'Tram', 1640, 3, N'Tram Bulbul singing at dawn.', N'Yellow and White', CAST(N'2024-05-17T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (69, N'14', N'Hoa', 1630, 2, N'Hoa Bulbul hopping between branches.', N'Red and Brown', CAST(N'2024-05-18T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao4.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (70, N'14', N'Thanh', 1620, 5, N'Thanh Bulbul nesting in a bamboo grove.', N'Green and Yellow', CAST(N'2024-05-19T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao3.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (71, N'14', N'Tuyen', 1610, 3, N'Tuyen Bulbul perched on a rock.', N'Black and Grey', CAST(N'2024-05-20T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao6.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (72, N'13', N'Kien', 1600, 4, N'Kien Bulbul singing from a tall tree.', N'Yellow and Black', CAST(N'2024-05-21T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/d306d5e1-7e3e-41ea-877e-8fddb7906f70-chaomao2.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (73, N'12', N'Lam', 1590, 2, N'Lam Bulbul preening its feathers.', N'Brown and White', CAST(N'2024-05-22T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao6.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (74, N'11', N'Duc', 1580, 3, N'Duc Bulbul hopping on the ground.', N'Green and Grey', CAST(N'2024-05-23T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao3.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (75, N'13', N'Binh', 1570, 4, N'Binh Bulbul observing its surroundings.', N'Red and Black', CAST(N'2024-05-24T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao7.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (76, N'13', N'Phong', 1560, 5, N'Phong Bulbul nesting in a pine tree.', N'Brown and Green', CAST(N'2024-05-25T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (77, N'12', N'Ngoc', 1550, 2, N'Ngoc Bulbul singing from a roof.', N'White and Grey', CAST(N'2024-05-26T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao4.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (78, N'12', N'Bao', 1540, 3, N'Bao Bulbul flying across a field.', N'Green and Yellow', CAST(N'2024-05-27T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao1.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (79, N'11', N'Hung', 1530, 4, N'Hung Bulbul chirping on a fence.', N'Red and Brown', CAST(N'2024-05-28T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/d306d5e1-7e3e-41ea-877e-8fddb7906f70-chaomao2.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (80, N'11', N'Quang', 1520, 5, N'Quang Bulbul playing in a puddle.', N'Yellow and Grey', CAST(N'2024-05-29T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao5.jpg', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (81, N'10', N'Son', 1510, 2, N'Son Bulbul flying through a forest.', N'Green and Brown', CAST(N'2024-05-30T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (82, N'10', N'Tuan', 1500, 4, N'Tuan Bulbul perched on a telephone wire.', N'Grey and Yellow', CAST(N'2024-05-31T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (83, N'9', N'Thai', 1490, 3, N'Thai Bulbul hopping between leaves.', N'Green and White', CAST(N'2024-04-01T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (84, N'9', N'Trung', 1480, 5, N'Trung Bulbul bathing in a birdbath.', N'Yellow and Blue', CAST(N'2024-04-02T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (85, N'8', N'Khoa', 1470, 2, N'Khoa Bulbul chirping on a branch.', N'Brown and White', CAST(N'2024-04-03T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (86, N'8', N'Hoang', 1460, 4, N'Hoang Bulbul observing a flower.', N'Green and Yellow', CAST(N'2024-04-04T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (87, N'7', N'Dong', 1450, 3, N'Dong Bulbul preening its wings.', N'Red and Black', CAST(N'2024-01-05T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (88, N'7', N'Thanh', 1440, 5, N'Thanh Bulbul perched on a garden fence.', N'Grey and Brown', CAST(N'2024-01-06T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (89, N'7', N'Phu', 1430, 2, N'Phu Bulbul chirping at dawn.', N'Green and Yellow', CAST(N'2024-01-07T00:00:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (90, N'6', N'Hieu', 1420, 3, N'Hieu Bulbul flying over a pond.', N'Brown and Grey', CAST(N'2024-05-19T12:13:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (91, N'6', N'Thanh', 1410, 4, N'Thanh Bulbul resting on a rock.', N'Yellow and Black', CAST(N'2024-05-20T15:42:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (92, N'5', N'Trinh', 1400, 5, N'Trinh Bulbul hopping on a lawn.', N'Green and White', CAST(N'2024-05-18T15:36:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (93, N'5', N'Loc', 1390, 2, N'Loc Bulbul singing from a fence post.', N'Brown and Yellow', CAST(N'2024-05-16T18:52:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (94, N'4', N'Hai', 1380, 3, N'Hai Bulbul watching the sunset.', N'Red and Grey', CAST(N'2024-04-16T12:32:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (95, N'4', N'Phat', 1370, 4, N'Phat Bulbul preening its feathers.', N'Yellow and Brown', CAST(N'2024-04-17T12:54:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (96, N'3', N'Toan', 1360, 5, N'Toan Bulbul chirping on a branch.', N'Green and Black', CAST(N'2024-04-18T16:34:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (97, N'3', N'Viet', 1350, 2, N'Viet Bulbul hopping on a roof.', N'White and Grey', CAST(N'2024-04-20T14:34:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (98, N'2', N'Dat', 1340, 3, N'Dat Bulbul singing at noon.', N'Green and Yellow', CAST(N'2024-02-18T11:45:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (99, N'2', N'Trung', 1330, 4, N'Trung Bulbul chirping from a high branch.', N'Brown and Black', CAST(N'2024-02-19T11:45:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
GO
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (100, N'1', N'Nha', 1320, 5, N'Quy Bulbul hopping on the ground.', N'Red and Yellow', CAST(N'2024-01-25T10:23:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (101, N'1', N'Hoan', 1310, 3, N'Khai Bulbul flying over a field.', N'Green and Grey', CAST(N'2024-01-27T10:23:00.000' AS DateTime), N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/21ace224-4acb-4324-bac3-fa80d94714e3-bird.png', N'Active', N'Vietnam')
SET IDENTITY_INSERT [dbo].[Bird] OFF
GO
SET IDENTITY_INSERT [dbo].[BirdMedia] ON 

INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image], [type]) VALUES (1, 1, N'Beautiful Red Bulbul in flight', N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao11.jpg', N'Additional')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image], [type]) VALUES (2, 1, N'Close-up of Red Bulbul feeding', N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao10.jpg', N'Additional')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image], [type]) VALUES (3, 2, N'Red Bulbul pair on a branch', N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao12.jpg', N'Additional')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image], [type]) VALUES (4, 2, N'Red Bulbul bathing in a pond', N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao9.jpg', N'Additional')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image], [type]) VALUES (5, 3, N'Juvenile Red Bulbul exploring', N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao11.jpg', N'Additional')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image], [type]) VALUES (6, 3, N'Red Bulbul with nesting material', N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao14.jpg', N'Additional')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image], [type]) VALUES (7, 4, N'Red Bulbul singing on a tree', N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao8.jpg', N'Additional')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image], [type]) VALUES (8, 4, N'Red Bulbul in its natural habitat', N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao7.jpg', N'Additional')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image], [type]) VALUES (9, 5, N'Red Bulbul family in the morning', N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao12.jpg', N'Additional')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image], [type]) VALUES (10, 5, N'Red Bulbul perched on a fence', N'https://edwinbirdclubstorage.blob.core.windows.net/images/bird/chaomao13.jpg', N'Additional')
SET IDENTITY_INSERT [dbo].[BirdMedia] OFF
GO
SET IDENTITY_INSERT [dbo].[Blog] ON 

INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (1, 1, N'Observations of Red-Whiskered Bulbuls in Central Park', N'Announcement', CAST(N'2024-03-01T09:43:00.000' AS DateTime), 15, N'/images/central_park_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (2, 2, N'Tips for Attracting Bulbuls to Your Garden', N'Discussion', CAST(N'2024-03-05T16:24:00.000' AS DateTime), 20, N'/images/garden_tips_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (3, 3, N'The Beauty of Bulbul Nests: A Photo Journey', N'Discussion', CAST(N'2024-03-10T21:20:00.000' AS DateTime), 25, N'/images/nest_photo_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (4, 4, N'Red Bulbul Conservation Efforts in Everglades National Park', N'Others', CAST(N'2024-03-15T09:10:00.000' AS DateTime), 18, N'/images/conservation_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (5, 5, N'Spotting Rare Bulbul Varieties in Griffith Park', N'Information', CAST(N'2024-03-20T10:55:00.000' AS DateTime), 22, N'/images/rare_bulbuls_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (6, 6, N'A Day of Birdwatching at Discovery Park', N'Announcement', CAST(N'2024-03-25T20:02:00.000' AS DateTime), 17, N'/images/birdwatching_day_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (7, 7, N'Exploring Stanford University Campus: Birdwatcher’s Paradise', N'Announcement', CAST(N'2024-03-30T16:16:00.000' AS DateTime), 23, N'/images/stanford_campus_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (8, 8, N'Winter Bulbul Migration Patterns at South Mountain Park', N'Information', CAST(N'2024-04-05T10:20:00.000' AS DateTime), 19, N'/images/migration_patterns_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (9, 9, N'Piedmont Park: A Haven for Red Bulbuls', N'Information', CAST(N'2024-04-10T08:32:00.000' AS DateTime), 21, N'/images/piedmont_park_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (10, 10, N'Fairmount Park Bird Festival: Highlights and Discoveries', N'Discussion', CAST(N'2024-04-15T20:00:12.000' AS DateTime), 28, N'/images/park_festival_blog.jpg', N'Active')
SET IDENTITY_INSERT [dbo].[Blog] OFF
GO
SET IDENTITY_INSERT [dbo].[ClubInformation] ON 

INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [description]) VALUES (1, 1, N'Situated in the bustling metropolis of Ho Chi Minh City, this club is at the heart of Vietnam''''s economic and cultural hub. Formerly known as Saigon, the city offers a dynamic blend of modern skyscrapers, historic landmarks, and bustling markets. Members can enjoy access to a wide range of amenities, entertainment venues, and urban parks for birdwatching activities.')
SET IDENTITY_INSERT [dbo].[ClubInformation] OFF
GO
SET IDENTITY_INSERT [dbo].[ClubLocation] ON 

INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (1, N'Ho Chi Minh City Bulbul Bird Club', N'Being one of the oldest and most developed clubs in Ho Chi Minh City, where many bird enthusiasts gather and host large events and competitions about Bulbul birds.', 4)
SET IDENTITY_INSERT [dbo].[ClubLocation] OFF
GO
SET IDENTITY_INSERT [dbo].[Comment] ON 

INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (2, 1, 5, N'Great insights into the behaviors of Red-Whiskered Bulbuls!', CAST(N'2024-03-02T09:21:00.000' AS DateTime), 1)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (3, 1, 4, N'I love observing birds in Central Park. Thanks for sharing!', CAST(N'2024-03-03T13:23:00.000' AS DateTime), 2)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (4, 2, 3, N'Your gardening tips worked wonders in attracting bulbuls to my garden.', CAST(N'2024-03-06T17:50:00.000' AS DateTime), 3)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (5, 2, 5, N'Beautiful photos! Bulbuls are truly fascinating creatures.', CAST(N'2024-03-07T10:20:00.000' AS DateTime), 4)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (6, 3, 4, N'The photo journey of bulbul nests is heartwarming.', CAST(N'2024-03-11T15:26:00.000' AS DateTime), 5)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (7, 3, 5, N'Nature''s wonders captured in these pictures. Amazing!', CAST(N'2024-03-12T23:59:00.000' AS DateTime), 6)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (8, 4, 4, N'Conservation efforts are crucial. Thank you for raising awareness!', CAST(N'2024-03-16T21:55:00.000' AS DateTime), 7)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (9, 4, 3, N'Shy or not, every bird plays a vital role in the ecosystem.', CAST(N'2024-03-17T17:15:00.000' AS DateTime), 8)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (10, 5, 5, N'Rare varieties are always a joy to spot. Thanks for the guide!', CAST(N'2024-03-21T22:08:00.000' AS DateTime), 9)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (11, 5, 4, N'The festival was a blast! Looking forward to more events.', CAST(N'2024-03-22T08:59:00.000' AS DateTime), 10)
SET IDENTITY_INSERT [dbo].[Comment] OFF
GO
SET IDENTITY_INSERT [dbo].[Contest] ON 

INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (1, N'Sublime Melody Challenge', N'The competition focuses on the delicacy and uniqueness of the melodious chirps of Bulbul birds, encouraging contestants to demonstrate creativity and individual style.', CAST(N'2024-10-05T00:00:00.000' AS DateTime), CAST(N'2024-10-15T00:00:00.000' AS DateTime), 13, N'OnHold', CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), 1000, 1200, 20, 200000, 500000, N'AnhTuan', N'TuanKiet', N'Ensure that the competition environment does not induce excessive stress or pressure on the contestants and respect the diversity of singing styles.', N'Good', 0, 1, 0, 10)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (2, N'Plumage Beauty Pageant', N'Birds participating in the competition are evaluated based on the beauty of their feathers, overall shape, and coloration.', CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), 14, N'Postponed', CAST(N'2024-11-25T00:00:00.000' AS DateTime), CAST(N'2024-12-05T00:00:00.000' AS DateTime), 1000, 1200, 20, 150000, 1000000, N'emily_white', N'ThanhPhat', N'Ensure that the evaluation process does not compromise the health and natural state of the birds, and respect the diversity of plumage types.', N'Good', 0, 1, 0, 10)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (3, N'Eye-Catching Cage Battle', N'The competition focuses on the agility and interaction of Bulbul birds in the cage environment, assessing their movement skills and natural behaviors.', CAST(N'2024-10-10T00:00:00.000' AS DateTime), CAST(N'2024-10-20T00:00:00.000' AS DateTime), 15, N'Cancelled', CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'2024-11-20T00:00:00.000' AS DateTime), 1200, 1400, 20, 100000, 750000, N'AnhTuan', N'QuangHuy', N'Ensure that the cages provide a safe and comfortable environment for the participating birds and do not induce stress or tension.', N'Good', 0, 1, 0, 10)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (4, N'North-South Singing Showdown', N'The competition creates an opportunity for Bulbul birds from different regions to compete in showcasing the distinctive singing styles of the North or South.', CAST(N'2024-05-01T00:00:00.000' AS DateTime), CAST(N'2024-06-15T00:00:00.000' AS DateTime), 16, N'OpenRegistration', CAST(N'2024-06-25T00:00:00.000' AS DateTime), CAST(N'2024-07-10T00:00:00.000' AS DateTime), 900, 1100, 20, 250000, 1200000, N'HieuMinh', N'TuanKiet', N'Encourage respect and appreciation for the cultural diversity of different regions and avoid bias or favoritism towards any particular region.', N'Good', 4, 1, 0, 10)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (5, N'Peak Performance Pinnacle', N'The competition challenges contestants to demonstrate their ability to attract and interact with partners or the surrounding environment.', CAST(N'2024-11-03T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), 14, N'ClosedRegistration', CAST(N'2024-12-01T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), 900, 1100, 20, 180000, 800000, N'AnhTuan', N'QuangHuy', N'Ensure that participating birds are trained and nurtured responsibly and do not experience excessive stress or pressure.', N'Good', 3, 1, 0, 10)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (6, N'Stylish Singing Showcase', N'The competition focuses on evaluating and encouraging diversity and creativity in the Bulbul birds'' singing styles.', CAST(N'2024-10-10T00:00:00.000' AS DateTime), CAST(N'2024-10-25T00:00:00.000' AS DateTime), 17, N'CheckingIn', CAST(N'2024-11-15T00:00:00.000' AS DateTime), CAST(N'2024-11-30T00:00:00.000' AS DateTime), 1400, 1600, 20, 100000, 600000, N'AnhTuan', N'QuangHuy', N'Encourage active participation and respect for the creative and unique aspects of each singing style.', N'Good', 2, 1, 0, 10)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (7, N'Young Talent Tournament', N'The competition aims to encourage and develop the skills of young Bulbul birds through various challenges and qualifiers.', CAST(N'2024-10-10T00:00:00.000' AS DateTime), CAST(N'2024-10-20T00:00:00.000' AS DateTime), 13, N'Ongoing', CAST(N'2024-11-05T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), 1200, 1400, 20, 50000, 1500000, N'emily_white', N'TuanKiet', N'Ensure that participating in the competition does not compromise the health and natural development of young birds and encourage education and support from bird owners.', N'Good', 1, 1, 0, 10)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (8, N'Grand Bulbul Gala', N'The competition serves as a festive event, providing an opportunity for the Bulbul bird community to meet, exchange experiences, and enjoy entertainment activities.', CAST(N'2024-10-23T00:00:00.000' AS DateTime), CAST(N'2024-10-30T00:00:00.000' AS DateTime), 14, N'Ended', CAST(N'2024-11-15T00:00:00.000' AS DateTime), CAST(N'2024-11-25T00:00:00.000' AS DateTime), 1100, 1300, 20, 120000, 900000, N'emily_white', N'AnhTuan', N'Create a joyful and friendly atmosphere and encourage active participation from everyone, including those not directly involved in the competition.', N'Good', 8, 1, 0, 10)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (9, N'Unique Mimicry Match: Marvelous Mimicry Masters', N'The competition focuses on evaluating and encouraging the mimicry and imitation skills of Bulbul birds, showcasing intelligence and flexibility.', CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), 13, N'OnHold', CAST(N'2024-11-25T00:00:00.000' AS DateTime), CAST(N'2024-12-10T00:00:00.000' AS DateTime), 1000, 1200, 20, 200000, 1200000, N'AnhTuan', N'QuangHuy', N'Ensure that participating birds are trained and nurtured responsibly, and do not experience stress or excessive pressure.', N'Good', 8, 1, 0, 10)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (10, N'High Note Harmony', N'The competition provides an opportunity for contestants to demonstrate talent and creativity by performing unique and refined singing styles.', CAST(N'2024-10-13T00:00:00.000' AS DateTime), CAST(N'2024-10-25T00:00:00.000' AS DateTime), 15, N'OnHold', CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'2024-11-20T00:00:00.000' AS DateTime), 1350, 1550, 20, 80000, 400000, N'sophia_d', N'AnhTuan', N'Encourage active participation and diversity in the singing styles performed, and respect the balance between competition and social interaction.', N'Good', 8, 1, 0, 10)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (11, N'Bulbul Beauty Pageant', N'<p>A showcase of the most beautiful and elegant Bulbul birds.</p>
', CAST(N'2024-06-09T00:00:00.000' AS DateTime), CAST(N'2024-07-15T00:00:00.000' AS DateTime), 15, N'OpenRegistration', CAST(N'2024-08-25T00:00:00.000' AS DateTime), CAST(N'2024-08-27T00:00:00.000' AS DateTime), 1400, 1600, 30, 100000, 500000, N'LanHuong', N'Nguyen Quang Huy', N'Contestants will be judged on plumage, posture, and overall appearance.', N'No Feedback', 0, 1, 10, 15)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (12, N'Bulbul Songwriting Contest', N'A competition for composing original songs inspired by Bulbul birds.', CAST(N'2024-04-05T00:00:00.000' AS DateTime), CAST(N'2024-06-25T00:00:00.000' AS DateTime), 10, N'OpenRegistration', CAST(N'2024-07-30T00:00:00.000' AS DateTime), CAST(N'2024-08-05T00:00:00.000' AS DateTime), 1400, 1600, 40, 120000, 600000, N'AnhTuan', N'ThanhPhat', N'Originality, melody, and lyrics will be key criteria for judging.', N'Excellent', 0, 1, 0, 20)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (13, N'Bulbul Photography Contest', N'A photography contest capturing the beauty of Bulbul birds in their natural habitat.', CAST(N'2024-04-10T00:00:00.000' AS DateTime), CAST(N'2024-06-15T00:00:00.000' AS DateTime), 18, N'OpenRegistration', CAST(N'2024-06-20T00:00:00.000' AS DateTime), CAST(N'2024-07-01T00:00:00.000' AS DateTime), 1500, 1700, 25, 80000, 450000, N'AnhTuan', N'ThanhPhat', N'Judging criteria include composition, lighting, and creativity.', N'Good', 0, 1, 0, 12)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (14, N'Bulbul Conservation Symposium', N'A symposium discussing conservation efforts and challenges related to Bulbul birds.', CAST(N'2024-05-15T00:00:00.000' AS DateTime), CAST(N'2024-06-18T00:00:00.000' AS DateTime), 7, N'OpenRegistration', CAST(N'2024-06-25T00:00:00.000' AS DateTime), CAST(N'2024-07-05T00:00:00.000' AS DateTime), 1500, 1700, 35, 150000, 700000, N'sophia_d', N'ThanhPhat', N'Experts and researchers will present findings and discuss strategies for Bulbul conservation.', N'Excellent', 0, 1, 0, 25)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (15, N'Bulbul Vocal Talent Showdown', N'A competition showcasing the vocal talents of Bulbul birds.', CAST(N'2024-02-20T00:00:00.000' AS DateTime), CAST(N'2024-06-30T00:00:00.000' AS DateTime), 12, N'OpenRegistration', CAST(N'2024-07-12T00:00:00.000' AS DateTime), CAST(N'2024-07-25T00:00:00.000' AS DateTime), 1250, 1450, 28, 110000, 550000, N'NgocCuong', N'ThanhPhat', N'Judging criteria include pitch, rhythm, and vocal range.', N'Good', 0, 1, 0, 18)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (16, N'Bulbul Art Exhibition', N'An exhibition showcasing artwork inspired by Bulbul birds.', CAST(N'2024-03-25T00:00:00.000' AS DateTime), CAST(N'2024-07-05T00:00:00.000' AS DateTime), 9, N'OpenRegistration', CAST(N'2024-08-15T00:00:00.000' AS DateTime), CAST(N'2024-08-30T00:00:00.000' AS DateTime), 1550, 1750, 32, 100000, 600000, N'LanHuong', N'ThanhPhat', N'Artwork will be judged based on creativity, technique, and interpretation of Bulbul birds.', N'Excellent', 0, 1, 0, 20)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (17, N'Bulbul Birding Marathon', N'A marathon birdwatching event to spot as many Bulbul species as possible.', CAST(N'2024-06-05T00:00:00.000' AS DateTime), CAST(N'2024-07-10T00:00:00.000' AS DateTime), 13, N'OpenRegistration', CAST(N'2024-08-20T00:00:00.000' AS DateTime), CAST(N'2025-09-05T00:00:00.000' AS DateTime), 1650, 1850, 38, 120000, 650000, N'michael_g', N'ThanhPhat', N'Participants will compete to identify the most Bulbul species within the designated time frame.', N'Good', 0, 1, 0, 10)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (18, N'Bulbul Birdhouse Design Contest', N'A contest to design innovative and functional birdhouses for Bulbul birds.', CAST(N'2024-06-05T00:00:00.000' AS DateTime), CAST(N'2024-08-15T00:00:00.000' AS DateTime), 11, N'OpenRegistration', CAST(N'2024-08-25T00:00:00.000' AS DateTime), CAST(N'2025-09-10T00:00:00.000' AS DateTime), 1600, 1800, 30, 100000, 550000, N'LanHuong', N'ThanhPhat', N'Judging criteria include design creativity, functionality, and suitability for Bulbul nesting.', N'Good', 0, 1, 0, 18)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (19, N'Bulbul Birding Photo Contest', N'A photo contest capturing the beauty and behavior of Bulbul birds.', CAST(N'2024-02-10T00:00:00.000' AS DateTime), CAST(N'2024-06-20T00:00:00.000' AS DateTime), 16, N'OpenRegistration', CAST(N'2024-07-01T00:00:00.000' AS DateTime), CAST(N'2024-07-15T00:00:00.000' AS DateTime), 1500, 1700, 26, 80000, 450000, N'LanHuong', N'QuangHuy', N'Photos will be judged based on composition, clarity, and capturing unique Bulbul behavior.', N'Good', 0, 1, 0, 15)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (20, N'Bulbul Birding Quiz Challenge', N'A quiz challenge testing participants’ knowledge of Bulbul birds and birdwatching.', CAST(N'2024-04-15T00:00:00.000' AS DateTime), CAST(N'2024-06-25T00:00:00.000' AS DateTime), 14, N'OpenRegistration', CAST(N'2024-09-05T00:00:00.000' AS DateTime), CAST(N'2024-09-20T00:00:00.000' AS DateTime), 1250, 1450, 22, 110000, 550000, N'BichNgoc', N'QuangHuy', N'Quiz questions will cover Bulbul species identification, habitat, and behavior.', N'Good', 0, 1, 0, 20)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (21, N'Bulbul Birdwatching Workshop', N'A workshop on birdwatching techniques and Bulbul bird identification.', CAST(N'2024-05-20T00:00:00.000' AS DateTime), CAST(N'2024-07-30T00:00:00.000' AS DateTime), 19, N'OpenRegistration', CAST(N'2024-10-10T00:00:00.000' AS DateTime), CAST(N'2024-10-25T00:00:00.000' AS DateTime), 1200, 1400, 20, 100000, 500000, N'sophia_d', N'TuanKiet', N'Participants will learn about Bulbul bird habitats, behavior, and vocalizations.', N'Good', 0, 1, 0, 18)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (22, N'Bulbul Bird Conservation Expo', N'An exposition showcasing conservation efforts for Bulbul birds and their habitats.', CAST(N'2024-04-19T00:00:00.000' AS DateTime), CAST(N'2024-06-26T00:00:00.000' AS DateTime), 20, N'OpenRegistration', CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-08-05T00:00:00.000' AS DateTime), 1400, 1600, 28, 120000, 600000, N'AnhTuan', N'TuanKiet', N'The expo will feature exhibits, presentations, and discussions on Bulbul bird conservation.', N'Good', 9, 1, 0, 10)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (23, N'Bulbul Birding Adventure Tour', N'A guided adventure tour to explore Bulbul bird habitats and observe diverse wildlife.', CAST(N'2024-01-10T00:00:00.000' AS DateTime), CAST(N'2024-01-20T00:00:00.000' AS DateTime), 17, N'ClosedRegistration', CAST(N'2024-11-30T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), 1300, 1500, 24, 100000, 550000, N'BichNgoc', N'ThanhPhat', N'Participants will embark on guided tours to Bulbul bird hotspots and learn about local biodiversity.', N'Good', 0, 1, 0, 18)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (24, N'Bulbul Birding Documentary Contest', N'A documentary film contest showcasing the beauty and conservation challenges of Bulbul birds.', CAST(N'2024-01-15T00:00:00.000' AS DateTime), CAST(N'2024-06-25T00:00:00.000' AS DateTime), 12, N'OpenRegistration', CAST(N'2024-10-05T00:00:00.000' AS DateTime), CAST(N'2024-10-20T00:00:00.000' AS DateTime), 1500, 1700, 32, 150000, 700000, N'michael_g', N'TuanKiet', N'Documentaries will highlight Bulbul bird behavior, habitats, and conservation initiatives.', N'Excellent', 0, 1, 0, 20)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (25, N'Bulbul Birding Lecture Series', N'A series of lectures covering various aspects of Bulbul bird biology, behavior, and conservation.', CAST(N'2024-01-20T00:00:00.000' AS DateTime), CAST(N'2024-07-30T00:00:00.000' AS DateTime), 14, N'OpenRegistration', CAST(N'2024-10-10T00:00:00.000' AS DateTime), CAST(N'2024-10-25T00:00:00.000' AS DateTime), 1200, 1400, 18, 100000, 500000, N'AnhTuan', N'ThanhPhat', N'Experts will deliver presentations on Bulbul bird ecology, conservation challenges, and research findings.', N'Good', 0, 1, 0, 18)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (26, N'Bulbul Birding Workshop for Beginners', N'A workshop designed to introduce beginners to the basics of birdwatching, with a focus on Bulbul birds.', CAST(N'2024-01-25T00:00:00.000' AS DateTime), CAST(N'2024-07-05T00:00:00.000' AS DateTime), 10, N'OpenRegistration', CAST(N'2024-07-15T00:00:00.000' AS DateTime), CAST(N'2024-08-01T00:00:00.000' AS DateTime), 1100, 1300, 15, 80000, 450000, N'LanHuong', N'QuangHuy', N'Participants will learn bird identification techniques, use of field guides, and observation skills.', N'Good', 0, 1, 0, 15)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (27, N'Bulbul Birdwatching Field Trip', N'A guided field trip to Bulbul bird habitats for birdwatching and photography.', CAST(N'2024-01-31T00:00:00.000' AS DateTime), CAST(N'2024-06-12T00:00:00.000' AS DateTime), 16, N'OpenRegistration', CAST(N'2024-06-25T00:00:00.000' AS DateTime), CAST(N'2024-06-30T00:00:00.000' AS DateTime), 1250, 1450, 20, 100000, 550000, N'AnhTuan', N'TuanKiet', N'Participants will explore Bulbul bird habitats and learn birdwatching techniques from experienced guides.', N'Good', 0, 1, 0, 20)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (28, N'Bulbul Birding Scavenger Hunt', N'A fun and educational scavenger hunt to search for Bulbul birds and identify various species.', CAST(N'2024-02-05T00:00:00.000' AS DateTime), CAST(N'2024-06-15T00:00:00.000' AS DateTime), 8, N'OpenRegistration', CAST(N'2024-07-25T00:00:00.000' AS DateTime), CAST(N'2024-08-10T00:00:00.000' AS DateTime), 1300, 1500, 24, 100000, 550000, N'michael_g', N'ThanhPhat', N'Participants will search for Bulbul birds and other wildlife while completing challenges and tasks.', N'Good', 0, 1, 0, 18)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (29, N'Bulbul Birdwatching Photo Walk', N'A guided photo walk through Bulbul bird habitats to capture stunning images of birds and nature.', CAST(N'2024-02-10T00:00:00.000' AS DateTime), CAST(N'2024-06-10T00:00:00.000' AS DateTime), 11, N'OpenRegistration', CAST(N'2024-06-21T00:00:00.000' AS DateTime), CAST(N'2024-07-07T00:00:00.000' AS DateTime), 1200, 1400, 18, 80000, 450000, N'AnhTuan', N'QuangHuy', N'Participants will receive photography tips and guidance while exploring Bulbul bird habitats.', N'Good', 0, 1, 0, 16)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [openRegistration], [registrationDeadline], [locationId], [status], [startDate], [endDate], [reqMinELO], [reqMaxELO], [afterELO], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (30, N'Bulbul Birding Lecture: Bulbul Behaviors', N'A lecture focusing on the behavior and social interactions of Bulbul birds in their natural habitat.', CAST(N'2024-02-15T00:00:00.000' AS DateTime), CAST(N'2024-07-25T00:00:00.000' AS DateTime), 15, N'OpenRegistration', CAST(N'2024-08-05T00:00:00.000' AS DateTime), CAST(N'2024-08-20T00:00:00.000' AS DateTime), 1300, 1500, 22, 100000, 500000, N'AnhTuan', N'QuangHuy', N'The lecture will cover Bulbul bird behaviors such as feeding, nesting, and communication.', N'Good', 0, 1, 0, 20)
SET IDENTITY_INSERT [dbo].[Contest] OFF
GO
SET IDENTITY_INSERT [dbo].[ContestMedia] ON 

INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (1, 4, N'Captivating moments from the Red Bulbul Contest', N'https://i.ytimg.com/vi/jA4mpm93cHQ/maxresdefault.jpg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (2, 11, N'The winning bird in action', N'https://edwinbirdclubstorage.blob.core.windows.net/images/contest/1420889c-5217-409b-ab9f-262a97f54bb0-contest_image_8.jpg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (3, 12, N'Contest participants in their natural habitat', N'https://thethaovanhoa.mediacdn.vn/2011/09/26/13/40/IMG2427.jpg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (4, 13, N'Admiring the beauty of Red-Whiskered Bulbuls', N'https://baodongnai.com.vn/file/e7837c02876411cd0187645a2551379f/dataimages/201802/original/images2106977__nh_2.jpg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (5, 14, N'Close-up of the winning bird', N'https://baoapbac.vn/dataimages/202311/original/images1904886_H_i_thi_chim_h_t_m_t_5_m_u_b_ng_v_ng__2_.jpg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (6, 15, N'Judges evaluating the contestants', N'https://media.techcity.cloud/binhdinh/2024/04/Soi-noi-Hoi-thi-tieng-hot-chim-chao-mao.Jpeg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (7, 16, N'Diverse bird species showcased in the contest', N'https://btnmt.1cdn.vn/2018/02/24/1-9.jpg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (8, 17, N'Bird enthusiasts enjoying the contest', N'https://hoisvcvn.org.vn/Uploads/images/ChimCanh_CaMau02.JPG', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (9, 18, N'Birds in flight during the contest', N'https://i.ex-cdn.com/nongnghiep.vn/files/f1/Article/thanhnb/2018/12/30/anh-2-1.JPG', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (10, 19, N'Celebrating the successful conclusion of the contest', N'https://danviet.mediacdn.vn/296231569849192448/2023/3/8/thu-choi-chim-chao-mao-31-144950-1678291586936-1678291587089404027413.jpg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (11, 20, N'Captivating moments from the Red Bulbul Contest', N'https://farm7.static.flickr.com/6055/6312964758_062f4854a9.jpg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (12, 21, N'The winning bird in action', N'https://bbt.1cdn.vn/2023/10/30/202310301140531.jpeg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (13, 22, N'Contest participants in their natural habitat', N'https://baobariavungtau.com.vn/dataimages/202402/original/images1931758_chim.jpg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (14, 23, N'Admiring the beauty of Red-Whiskered Bulbuls', N'https://images.baoangiang.com.vn/image/fckeditor/upload/2020/20200614/images/IMG_1306.JPG', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (15, 24, N'Close-up of the winning bird', N'https://baodongnai.com.vn/file/e7837c02876411cd0187645a2551379f/dataimages/202001/original/images2263205_Quang_c_nh_H_i_thi_chim_ch_ch_ch_e_l_a_h_t_ng_y_29_1__2_.jpg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (16, 25, N'Judges evaluating the contestants', N'https://thuanan.binhduong.gov.vn/documents/tinbai//a91d9846-d1fa-49e5-8f70-17e344e0062c.png', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (17, 26, N'Diverse bird species showcased in the contest', N'https://media.baobinhphuoc.com.vn/Content/UploadFiles//EditorFiles/images/2014/Quy4/hoithichim.jpg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (18, 27, N'Bird enthusiasts enjoying the contest', N'https://media.baothaibinh.com.vn/upload/news/5_2019/81286_chich_choe_than.jpg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (19, 28, N'Birds in flight during the contest', N'https://danviet.mediacdn.vn/zoom/700_438//upload/4-2018/images/2018-11-19/15426180924020-thumbnail.jpg', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (20, 29, N'Celebrating the successful conclusion of the contest', N'https://bizweb.dktcdn.net/100/163/312/articles/dia-diem-tap-ket-chim-sai-gon-1.jpg?v=1654680346013', N'Spotlight')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image], [type]) VALUES (21, 30, N'Celebrating the successful conclusion of the contest', N'https://dienbientv.vn/dataimages/201304/original/images836504_Hoa_Mi_1.jpg', N'Spotlight')
SET IDENTITY_INSERT [dbo].[ContestMedia] OFF
GO
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (3, N'17', 7, 1180, 55, 1, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (3, N'5', 5, 1650, 70, 2, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (3, N'6', 6, 1700, 90, 3, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (3, N'7', 7, 1600, 70, 4, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (4, N'18', 8, 1320, 65, 1, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (4, N'19', 9, 1150, 0, 2, N'Not Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (4, N'9', 9, 1800, 90, 3, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (5, N'10', 10, 1650, 70, 1, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (5, N'13', 3, 1450, 0, 2, N'Not Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (6, N'15', 5, 1300, 0, 1, N'Not Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (22, N'1', 1, 1600, 80, 1, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (22, N'11', 1, 1400, 40, 2, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (22, N'12', 2, 1450, 50, 3, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (22, N'14', 4, 1520, 70, 4, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (22, N'16', 6, 1400, 65, 5, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (22, N'2', 2, 1500, 90, 6, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (22, N'3', 3, 1550, 80, 7, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (22, N'4', 4, 1550, 55, 8, N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [memberId], [birdId], [ELO], [score], [participantNo], [checkInStatus]) VALUES (22, N'8', 8, 1550, 70, 9, N'Checked-In')
GO
SET IDENTITY_INSERT [dbo].[Feedback] ON 

INSERT [dbo].[Feedback] ([feedbackId], [userId], [eventId], [title], [details], [date], [category], [rating]) VALUES (1, 1, N'contest1', N'Fantastic Bulbul Singing Contest', N'The singing contest was amazing. The Red-whiskered Bulbuls performances were outstanding.', CAST(N'2024-02-05T00:00:00.000' AS DateTime), N'Contest', CAST(3.50 AS Decimal(5, 2)))
INSERT [dbo].[Feedback] ([feedbackId], [userId], [eventId], [title], [details], [date], [category], [rating]) VALUES (2, 2, N'meeting1', N'Great Meeting with Bird Enthusiasts', N'Had a wonderful time at the meeting. Met some passionate bird lovers.', CAST(N'2024-03-15T00:00:00.000' AS DateTime), N'Meeting', CAST(4.50 AS Decimal(5, 2)))
INSERT [dbo].[Feedback] ([feedbackId], [userId], [eventId], [title], [details], [date], [category], [rating]) VALUES (3, 3, N'contest2', N'Bulbul Dance-Off Feedback', N'The dance competition was so much fun. The Bulbuls really know how to dance!', CAST(N'2024-01-20T00:00:00.000' AS DateTime), N'Contest', CAST(4.00 AS Decimal(5, 2)))
INSERT [dbo].[Feedback] ([feedbackId], [userId], [eventId], [title], [details], [date], [category], [rating]) VALUES (4, 4, N'meeting2', N'Informative Birdwatching Meeting', N'The meeting provided valuable insights into birdwatching. Learned a lot!', CAST(N'2024-02-29T00:00:00.000' AS DateTime), N'Meeting', CAST(4.50 AS Decimal(5, 2)))
INSERT [dbo].[Feedback] ([feedbackId], [userId], [eventId], [title], [details], [date], [category], [rating]) VALUES (5, 5, N'contest3', N'Nesting Championship', N'The nest-building competition was a bit challenging, but so rewarding!', CAST(N'2024-11-10T00:00:00.000' AS DateTime), N'Contest', CAST(4.00 AS Decimal(5, 2)))
INSERT [dbo].[Feedback] ([feedbackId], [userId], [eventId], [title], [details], [date], [category], [rating]) VALUES (6, 6, N'meeting3', N'Meeting Feedback', N'The meeting was informative, but it could be improved with more interactive sessions.', CAST(N'2024-02-25T00:00:00.000' AS DateTime), N'Meeting', CAST(4.50 AS Decimal(5, 2)))
INSERT [dbo].[Feedback] ([feedbackId], [userId], [eventId], [title], [details], [date], [category], [rating]) VALUES (7, 7, N'contest4', N'Bulbul Art Contest', N'Enjoyed the art contest. So many creative Bulbul-themed artworks!', CAST(N'2024-03-15T00:00:00.000' AS DateTime), N'Contest', CAST(4.50 AS Decimal(5, 2)))
INSERT [dbo].[Feedback] ([feedbackId], [userId], [eventId], [title], [details], [date], [category], [rating]) VALUES (8, 8, N'contest5', N'Singing Contest Disappointment', N'The singing contest didn''t meet my expectations. Some performances were off-key.', CAST(N'2024-05-05T00:00:00.000' AS DateTime), N'Contest', CAST(4.00 AS Decimal(5, 2)))
INSERT [dbo].[Feedback] ([feedbackId], [userId], [eventId], [title], [details], [date], [category], [rating]) VALUES (9, 9, N'meeting4', N'Birdwatching Meeting Suggestion', N'The meeting was good, but it would be better if it had more practical birdwatching tips.', CAST(N'2024-03-15T00:00:00.000' AS DateTime), N'Meeting', CAST(4.50 AS Decimal(5, 2)))
INSERT [dbo].[Feedback] ([feedbackId], [userId], [eventId], [title], [details], [date], [category], [rating]) VALUES (10, 10, N'contest6', N'Plumage Parade Contest', N'The colorful plumage contest was a visual treat. The Bulbuls looked stunning!', CAST(N'2024-04-30T00:00:00.000' AS DateTime), N'Contest', CAST(4.50 AS Decimal(5, 2)))
SET IDENTITY_INSERT [dbo].[Feedback] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldTrip] ON 

INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (22, N'Deep Jungle Bulbul Bird Exploration', N'Explore deep into the jungle to observe the elusive Bulbul birds in their natural habitat, amidst lush foliage and diverse wildlife.', N'Participants will embark on a guided trek led by experienced naturalists, venturing into remote jungle areas known for Bulbul bird sightings.', CAST(N'2024-06-10T00:00:00.000' AS DateTime), CAST(N'2024-07-15T00:00:00.000' AS DateTime), CAST(N'2024-08-01T00:00:00.000' AS DateTime), CAST(N'2024-08-03T00:00:00.000' AS DateTime), 3, N'OpenRegistration', 9, 10, 10, 2500000, N'AnhTuan', N'Nguyen Quang Huy', N'Be prepared for rugged terrain and humid conditions, and respect the natural environment by minimizing disturbance to wildlife and vegetation.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (23, N'Wild Bulbul Bird Watching Expedition', N'Embark on an expedition to observe Bulbul birds in their wild habitats, including forests, grasslands, and wetlands.', N'The trip includes visits to various natural habitats favored by Bulbul birds, with opportunities for birdwatching and photography.', CAST(N'2024-07-10T00:00:00.000' AS DateTime), CAST(N'2024-07-28T00:00:00.000' AS DateTime), CAST(N'2024-08-15T00:00:00.000' AS DateTime), CAST(N'2024-03-17T00:00:00.000' AS DateTime), 3, N'Postponed', 0, 0, 10, 2200000, N'AnhTuan', N'QuangHuy', N'Bring binoculars, cameras, and field guides for bird identification, and follow ethical guidelines for wildlife observation to avoid disturbing the birds.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (24, N'Highland Bulbul Bird Survey', N'Conduct a survey of Bulbul birds in highland regions, exploring mountainous terrain and montane forests.', N'Participants will hike to elevated areas known for Bulbul bird populations, conducting observations and collecting data on their distribution and behavior.', CAST(N'2024-07-10T00:00:00.000' AS DateTime), CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-08-10T00:00:00.000' AS DateTime), CAST(N'2024-04-12T00:00:00.000' AS DateTime), 3, N'Cancelled', 0, 0, 10, 3100000, N'AnhTuan', N'QuangHuy', N'Prepare for altitude and weather changes, and adhere to safety guidelines for hiking in mountainous areas.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (25, N'Bulbul Bird Stroll in Pristine Forests', N'Take a leisurely stroll through pristine forests to observe Bulbul birds amidst towering trees and rich biodiversity.', N'Take a leisurely stroll through pristine forests to observe Bulbul birds amidst towering trees and rich biodiversity.', CAST(N'2024-05-15T00:00:00.000' AS DateTime), CAST(N'2024-06-25T00:00:00.000' AS DateTime), CAST(N'2024-09-09T00:00:00.000' AS DateTime), CAST(N'2024-09-10T00:00:00.000' AS DateTime), 4, N'OpenRegistration', 0, 0, 10, 2000000, N'AnhTuan', N'QuangHuy', N'Stay on designated trails to minimize impact on fragile ecosystems, and be respectful of wildlife and plant life encountered along the way.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (26, N'Exploring Bulbul Birds in Nature Reserves', N'Explore nature reserves dedicated to the conservation of Bulbul birds and their habitats, learning about ongoing conservation efforts.', N' Participants will visit protected areas known for Bulbul bird populations, accompanied by park rangers or conservationists who will provide insights into habitat management and conservation practices.', CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-08-30T00:00:00.000' AS DateTime), CAST(N'2024-09-15T00:00:00.000' AS DateTime), CAST(N'2024-06-17T00:00:00.000' AS DateTime), 5, N'ClosedRegistration', 1, 0, 10, 2400000, N'AnhTuan', N'QuangHuy', N'Follow park regulations and guidelines, and learn about the threats facing Bulbul birds and the importance of their conservation.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (27, N'Following the Footsteps of Bulbul Birds', N'Trace the footsteps of Bulbul birds through diverse habitats, learning about their ecological roles and behaviors.', N'The journey will take participants through various habitats frequented by Bulbul birds, with opportunities to observe feeding, nesting, and social interactions.', CAST(N'2024-06-15T00:00:00.000' AS DateTime), CAST(N'2024-06-25T00:00:00.000' AS DateTime), CAST(N'2024-07-10T00:00:00.000' AS DateTime), CAST(N'2024-07-12T00:00:00.000' AS DateTime), 6, N'CheckingIn', 0, 0, 10, 2000000, N'AnhTuan', N'QuangHuy', N'Take notes on bird sightings and behaviors to contribute to citizen science projects or local conservation efforts.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (28, N'Bulbul Bird and Green Forest Nature Experience', N'Immerse yourself in the beauty of green forests while observing Bulbul birds and experiencing the tranquility of nature.', N'The trip includes guided walks through forested areas, birdwatching sessions, and relaxation amidst the sights and sounds of the forest.', CAST(N'2024-07-10T00:00:00.000' AS DateTime), CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-08-05T00:00:00.000' AS DateTime), CAST(N'2024-08-07T00:00:00.000' AS DateTime), 7, N'Ongoing', 0, 0, 10, 3000000, N'LanHuong', N'TuanKiet', N'Practice mindfulness and appreciation for the natural world, and take time to observe and reflect on the interconnectedness of all living things.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (29, N'Exploring Bulbul Birds in Vast Wetland Habitats', N'Venture into expansive wetland habitats to study Bulbul birds and their adaptations to aquatic environments.', N'Participants will travel by boat or kayak to explore wetland areas known for Bulbul bird populations, observing their foraging behaviors and interactions with other wetland species.', CAST(N'2024-08-15T00:00:00.000' AS DateTime), CAST(N'2024-08-25T00:00:00.000' AS DateTime), CAST(N'2024-09-10T00:00:00.000' AS DateTime), CAST(N'2024-09-12T00:00:00.000' AS DateTime), 8, N'Ended', 0, 0, 10, 2700000, N'AnhTuan', N'QuangHuy', N'Respect the delicate balance of wetland ecosystems and avoid disturbing nesting sites or disrupting natural processes.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (30, N'Exploring Scenery and Bulbul Birds in open Landscapes', N'Discover vast open landscapes and observe Bulbul birds thriving in grasslands, savannas, and agricultural areas.', N'The trip offers opportunities to explore diverse habitats outside of forests, with a focus on observing Bulbul birds in open environments.', CAST(N'2024-09-10T00:00:00.000' AS DateTime), CAST(N'2024-09-20T00:00:00.000' AS DateTime), CAST(N'2024-10-05T00:00:00.000' AS DateTime), CAST(N'2024-10-07T00:00:00.000' AS DateTime), 9, N'OnHold', 0, 0, 10, 3000000, N'AnhTuan', N'QuangHuy', N'Appreciate the beauty and importance of open landscapes for bird diversity and ecosystem health, and learn about sustainable land management practices.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (31, N'Bulbul Bird Excursion in National Parks', N'Enjoy birdwatching and nature exploration in national parks known for their biodiversity and Bulbul bird populations.', N'Participants will visit designated birdwatching areas within national parks, guided by park staff or experienced birdwatchers.', CAST(N'2024-10-05T00:00:00.000' AS DateTime), CAST(N'2024-10-15T00:00:00.000' AS DateTime), CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-11-03T00:00:00.000' AS DateTime), 10, N'OnHold', 0, 0, 10, 2500000, N'LanHuong', N'TuanKiet', N'Support conservation efforts by visiting national parks responsibly, respecting park rules, and raising awareness about the importance of protecting natural habitats.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (32, N'Bulbul Exploration Expedition', N'Explore the fascinating world of Bulbuls in Vietnam.', N'A comprehensive exploration of Bulbul habitats, behavior, and conservation efforts in Vietnam. Participants will engage in birdwatching excursions, expert-led discussions, and habitat conservation activities.', CAST(N'2024-04-20T00:00:00.000' AS DateTime), CAST(N'2024-06-30T00:00:00.000' AS DateTime), CAST(N'2024-09-15T00:00:00.000' AS DateTime), CAST(N'2024-09-17T00:00:00.000' AS DateTime), 1, N'OpenRegistration', 0, 0, 10, 1500000, N'AnhTuan', N'QuangHuy', NULL)
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (33, N'Bulbul Photography Workshop', N'Enhance your bird photography skills with Bulbuls as your subjects.', N'A specialized workshop focusing on bird photography techniques with Bulbuls as the primary subjects. Participants will receive guidance from experienced wildlife photographers and have the opportunity to capture stunning images of Bulbuls in their natural habitats.', CAST(N'2024-09-01T00:00:00.000' AS DateTime), CAST(N'2024-09-15T00:00:00.000' AS DateTime), CAST(N'2024-09-25T00:00:00.000' AS DateTime), CAST(N'2024-09-30T00:00:00.000' AS DateTime), 2, N'CheckingIn', 0, 0, 15, 2000000, N'AnhTuan', N'QuangHuy', NULL)
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (34, N'Bulbul Conservation Conference', N'Join experts in discussions on Bulbul conservation strategies.', N'A conference dedicated to the conservation of Bulbul species in Vietnam. Participants will engage in keynote speeches, panel discussions, and workshops aimed at formulating effective conservation measures for Bulbuls and their habitats.', CAST(N'2024-03-10T00:00:00.000' AS DateTime), CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-10-05T00:00:00.000' AS DateTime), CAST(N'2024-10-09T00:00:00.000' AS DateTime), 3, N'OpenRegistration', 0, 0, 30, 300000, N'LanHuong', N'TuanKiet', NULL)
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (35, N'Bulbul Habitat Restoration Project', N'Contribute to the restoration of Bulbul habitats in Vietnam.', N'A volunteer project focused on habitat restoration activities in Bulbul habitats. Participants will plant native vegetation, remove invasive species, and assist in habitat rehabilitation efforts to support Bulbul populations.', CAST(N'2024-09-20T00:00:00.000' AS DateTime), CAST(N'2024-10-01T00:00:00.000' AS DateTime), CAST(N'2024-10-20T00:00:00.000' AS DateTime), CAST(N'2024-10-25T00:00:00.000' AS DateTime), 4, N'Postponed', 0, 0, 25, 2000000, N'AnhTuan', N'QuangHuy', NULL)
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (36, N'Bulbul Species Survey Expedition', N'Conduct a survey to study Bulbul species distribution.', N'A scientific expedition to conduct a comprehensive survey of Bulbul species distribution and abundance in various regions of Vietnam. Participants will collect data, analyze habitat conditions, and contribute to scientific research on Bulbuls.', CAST(N'2024-10-05T00:00:00.000' AS DateTime), CAST(N'2024-10-15T00:00:00.000' AS DateTime), CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-11-05T00:00:00.000' AS DateTime), 5, N'Ongoing', 0, 0, 20, 200000, N'AnhTuan', N'QuangHuy', NULL)
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (37, N'Bulbul Birding Tour', N'Embark on a guided birding tour to spot Bulbuls in the wild.', N'A guided birdwatching tour designed to showcase the diversity of Bulbul species in Vietnam. Participants will visit prime birding locations, led by experienced guides, and observe Bulbuls in their natural habitats.', CAST(N'2024-10-21T00:00:00.000' AS DateTime), CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), CAST(N'2024-11-20T00:00:00.000' AS DateTime), 6, N'Cancelled', 0, 0, 15, 1000000, N'AnhTuan', N'QuangHuy', NULL)
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (38, N'Bulbul Educational Workshop', N'Learn about Bulbul ecology and behavior through interactive workshops.', N'An educational workshop series focusing on Bulbul ecology, behavior, and conservation. Participants will engage in hands-on activities, presentations, and field trips to learn about Bulbuls and their importance in ecosystems.', CAST(N'2024-06-05T00:00:00.000' AS DateTime), CAST(N'2024-08-15T00:00:00.000' AS DateTime), CAST(N'2024-11-25T00:00:00.000' AS DateTime), CAST(N'2024-11-26T00:00:00.000' AS DateTime), 7, N'OpenRegistration', 0, 0, 25, 500000, N'LanHuong', N'TuanKiet', NULL)
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (39, N'Bulbul Nature Retreat', N'Immerse yourself in nature and discover the beauty of Bulbuls.', N'A nature retreat offering participants the opportunity to connect with nature and observe Bulbuls in their natural habitats. Activities include guided nature walks, birdwatching sessions, and outdoor workshops.', CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'2024-11-20T00:00:00.000' AS DateTime), CAST(N'2024-12-05T00:00:00.000' AS DateTime), CAST(N'2024-12-10T00:00:00.000' AS DateTime), 8, N'ClosedRegistration', 0, 0, 20, 750000, N'AnhTuan', N'QuangHuy', NULL)
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (40, N'Bulbul Conservation Awareness Campaign', N'Promote awareness and support for Bulbul conservation efforts.', N'A public awareness campaign aimed at promoting the conservation of Bulbuls and their habitats. Participants will engage in community outreach activities, educational workshops, and advocacy initiatives to raise awareness and garner support for Bulbul conservation.', CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-12-01T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), CAST(N'2024-12-20T00:00:00.000' AS DateTime), 9, N'Postponed', 0, 0, 30, 100000, N'LanHuong', N'TuanKiet', NULL)
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [details], [openRegistration], [registrationDeadline], [startDate], [endDate], [locationId], [status], [numberOfParticipants], [numberOfParticipantsMinReq], [numberOfParticipantsLimit], [fee], [host], [inCharge], [note]) VALUES (41, N'Bulbul Citizen Science Project', N'Contribute to Bulbul research through citizen science initiatives.', N'A citizen science project engaging volunteers in data collection and monitoring activities related to Bulbul populations. Participants will receive training and support to contribute valuable data for scientific research and conservation efforts.', CAST(N'2024-12-01T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), CAST(N'2024-12-25T00:00:00.000' AS DateTime), CAST(N'2024-12-30T00:00:00.000' AS DateTime), 10, N'Cancelled', 0, 0, 10, 1000000, N'AnhTuan', N'QuangHuy', NULL)
SET IDENTITY_INSERT [dbo].[FieldTrip] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldTripAdditionalDetails] ON 

INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (1, 22, N'Bird Species Checklist', N'A checklist of bird species expected to be encountered during the field trip.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (2, 23, N'Guided Birding Session', N'An expert guide will lead the birdwatching sessions and provide insights into bird behavior.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (3, 24, N'Weather Conditions', N'Information about the expected weather conditions during the field trip.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (4, 25, N'Transportation Details', N'Details about transportation arrangements for the field trip.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (5, 26, N'Accommodation Information', N'Information about accommodation arrangements for participants.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (6, 27, N'Photography Tips', N'Tips and techniques for capturing memorable photographs of birds during the trip.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (7, 28, N'Schedule of Activities', N'A detailed schedule of activities planned for each day of the field trip.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (8, 29, N'Meals and Refreshments', N'Information about meals and refreshments provided during the trip.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (9, 30, N'Safety Guidelines', N'Important safety guidelines and procedures to ensure participant safety during the trip.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (10, 31, N'Local Culture Exploration', N'Opportunities to explore and experience the local culture and traditions.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (11, 32, N'Birding Equipment Checklist', N'A checklist of essential birding equipment to bring along for the field trip.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (12, 33, N'Expert-led Bird Identification', N'Expert-led sessions on bird identification techniques and species recognition.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (13, 34, N'Health and Medical Precautions', N'Precautionary measures and health tips to ensure participant well-being during the trip.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (14, 35, N'Recommended Field Guides', N'Recommended field guides and reference materials for bird identification.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (15, 36, N'Field Trip Itinerary', N'Detailed itinerary outlining the daily schedule and activities planned for the trip.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (16, 37, N'Communication Channels', N'Information about communication channels and emergency contact numbers during the trip.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (17, 38, N'Birding Hotspots Map', N'A map highlighting popular birding hotspots and key locations for birdwatching.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (18, 39, N'Local Birdwatching Clubs', N'Join local birdwatching clubs for networking and shared experiences.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (19, 40, N'Restroom Facilities', N'Information about restroom facilities available along the route and at birding sites.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (20, 41, N'Birding Binoculars Guide', N'A guide to selecting and using birding binoculars for optimal birdwatching experiences.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (21, 22, N'Photography Opportunities', N'Capture stunning photographs of Bulbul birds in their natural habitat.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (22, 23, N'Bulbul Behavior Observations', N'Observe and document Bulbul bird behavior such as feeding, mating, and nesting.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (23, 24, N'Birding Equipment Recommendations', N'Recommended birding equipment including binoculars, field guides, and cameras.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (24, 25, N'Transportation Details', N'Details about transportation arrangements for the field trip.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (25, 26, N'Accommodation Information', N'Information about accommodation options during the field trip.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (26, 27, N'Photography Opportunities', N'Capture stunning photographs of Bulbul birds in their natural habitat.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (27, 28, N'Bulbul Behavior Observations', N'Observe and document Bulbul bird behavior such as feeding, mating, and nesting.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (28, 29, N'Birding Equipment Recommendations', N'Recommended birding equipment including binoculars, field guides, and cameras.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (29, 30, N'Bulbul Vocalization Recordings', N'Access recordings of Bulbul vocalizations for study and analysis.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (30, 31, N'Local Transportation Options', N'Information about local transportation options for getting around.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (31, 32, N'Local Birding Festivals', N'Participate in local birding festivals celebrating Bulbul birds.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (32, 33, N'Safety Guidelines', N'Guidelines for ensuring safety during the field trip.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (33, 34, N'Local Cuisine', N'Explore and enjoy local cuisine options available during the field trip.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (34, 35, N'Cultural Experiences', N'Opportunities to experience local culture and traditions.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (35, 36, N'Vehicle Rental Options', N'Information about vehicle rental options for independent exploration.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (36, 37, N'Suggested Itinerary', N'A suggested itinerary for the field trip including key attractions.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (37, 38, N'Language Tips', N'Useful phrases and language tips for communicating with locals.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (38, 39, N'Restaurant Recommendations', N'Recommended restaurants offering local delicacies.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (39, 40, N'Emergency Contact Information', N'Emergency contact details for medical and other emergencies.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (40, 41, N'Local Wildlife Guide', N'A guide to local wildlife species other than Bulbul birds.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (41, 22, N'Restroom Facilities', N'Information about restroom facilities along the field trip route.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (42, 23, N'Souvenir Shopping', N'Opportunities for souvenir shopping to remember the field trip.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (43, 24, N'Local Wildlife Conservation Efforts', N'Learn about local initiatives for wildlife conservation.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (44, 25, N'Vegetarian/Vegan Dining Options', N'Information about vegetarian and vegan dining options.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (45, 26, N'Birding Etiquette', N'Guidelines for ethical birdwatching practices and minimizing disturbance to birds.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (46, 27, N'Binocular Recommendations', N'Recommended binocular brands and features for birdwatching.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (47, 28, N'Translation Services', N'Information about translation services available for non-English speakers.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (48, 29, N'Bulbul Conservation Efforts', N'Learn about ongoing conservation efforts to protect Bulbul populations.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (49, 30, N'Health Precautions', N'Precautions to take to ensure health and well-being during the field trip.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (50, 31, N'Local Guides', N'Hire local guides for in-depth knowledge of Bulbul habitats and behavior.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (51, 32, N'Binocular Rental', N'Rent binoculars for birdwatching during the field trip.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (52, 33, N'Weather Appropriate Clothing', N'Advice on suitable clothing for the prevailing weather conditions.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (53, 34, N'Local Birding Hotspots', N'Explore local birding hotspots renowned for Bulbul sightings.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (54, 35, N'Snack Recommendations', N'Recommended snacks and energy bars for sustenance during the field trip.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (55, 36, N'Guided Birdwatching Tours', N'Join guided birdwatching tours led by expert ornithologists.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (56, 37, N'Bulbul Vocalizations', N'Learn to recognize Bulbul vocalizations and calls.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (57, 22, N'Emergency Medical Facilities', N'Locations of nearby medical facilities in case of emergencies.', N'important_to_know')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (58, 39, N'Bird Identification Guide', N'A guidebook to help identify Bulbul species and other birds.', N'tour_features')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (59, 40, N'Local Naturalists', N'Hire local naturalists for expert guidance and interpretation.', N'activities_and_transportation')
INSERT [dbo].[FieldTripAdditionalDetails] ([tripDetailsId], [tripId], [title], [description], [type]) VALUES (60, 41, N'Field Notes', N'Guidance on maintaining detailed field notes for scientific observation.', N'tour_features')
SET IDENTITY_INSERT [dbo].[FieldTripAdditionalDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldTripDaybyDay] ON 

INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (22, 2, 1, N'Arrival at Jungle Reserve, set up camp, introductory session about Bulbul birds.', N'Viet Nam', N'Forest Lodge', N'Breakfast: Lodge Restaurant, Lunch: Packed lunch in the field, Dinner: Lodge Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (22, 3, 2, N'Guided hike through the jungle, birdwatching sessions, evening campfire and storytelling.', N'Viet Nam', N'Lakeview Resort', N'Breakfast: Lodge Restaurant, Lunch: Packed lunch in the field, Dinner: Lodge Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (23, 4, 1, N'Departure to various habitats, birdwatching in forests.', N'Ba Be Lake', N'Lakeview Resort', N'Breakfast: Resort Restaurant, Lunch: Lakefront picnic, Dinner: Resort Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (23, 5, 2, N'Explore grasslands and wetlands, identify different species of Bulbul birds. Final birdwatching sessions, return to base camp, closing ceremony.', N'Ba Be Lake', N'Forest Lodge', N'Breakfast: Resort Restaurant, Lunch: Lakefront picnic, Dinner: Resort Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (23, 6, 3, N'Final birdwatching sessions, return to base camp, closing ceremony.', N'Ba Be Lake', N'Park Guesthouse', N'Breakfast: Resort Restaurant, Lunch: Lakefront picnic, Dinner: Resort Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (24, 7, 1, N'Travel to Highland Mountains, setup base camp.', N'Cuc Phuong National Park', N'Lakeview Resort', N'Breakfast: Guesthouse Restaurant, Lunch: Park cafeteria, Dinner: Local restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (24, 8, 2, N'Conduct bird surveys in different altitudes, collect data.', N'Cuc Phuong National Park', N'Park Guesthouse', N'Breakfast: Guesthouse Restaurant, Lunch: Park cafeteria, Dinner: Local restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (24, 9, 3, N'Continue surveys, data analysis session in the evening.', N'Cuc Phuong National Park', N'Forest Lodge', N'Breakfast: Guesthouse Restaurant, Lunch: Park cafeteria, Dinner: Local restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (24, 10, 4, N'Wrap-up, pack up, and return journey.', N'Cuc Phuong National Park', N'Park Guesthouse', N'Breakfast: Guesthouse Restaurant, Lunch: Park cafeteria, Dinner: Local restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (25, 11, 1, N'Guided walk through pristine forests, birdwatching sessions.', N'Vietnam Bird Sanctuary', N'Sanctuary Campsite', N'Breakfast: Campsite, Lunch: Packed lunch, Dinner: Campfire BBQ')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (26, 12, 1, N'Arrival at Nature Reserves, guided tour of reserve facilities.', N'Tam Coc Wetlands', N'Wetland Eco-lodge', N'Breakfast: Lodge Restaurant, Lunch: Floating restaurant, Dinner: Lodge Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (26, 13, 2, N'Birdwatching excursions in different zones of the reserve.', N'Tam Coc Wetlands', N'Wetland Eco-lodge', N'Breakfast: Lodge Restaurant, Lunch: Floating restaurant, Dinner: Lodge Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (27, 14, 1, N'Introduction to different habitats, begin tracking Bulbul birds.', N'Ba Vi National Park', N'Park Bungalows', N'Breakfast: Bungalow Restaurant, Lunch: Picnic lunch in the park, Dinner: Bungalow Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (27, 15, 2, N'Explore various habitats, study bird behaviors.', N'Ba Vi National Park', N'Wetland Eco-lodge', N'Breakfast: Bungalow Restaurant, Lunch: Picnic lunch in the park, Dinner: Bungalow Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (28, 16, 1, N'Nature walk in Green Forest Reserve, birdwatching sessions.', N'Ba Vi Peak', N'Park Campsite', N'Breakfast: Campsite, Lunch: Packed lunch, Dinner: Campfire Dinner')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (29, 17, 1, N'Arrival at Wetland Reserves, setup camp.', N'Ba Vi Forest', N'Forest Cabin', N'Breakfast: Cabin Kitchen, Lunch: Forest Picnic, Dinner: Cabin Kitchen')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (29, 18, 2, N'Bird surveys in wetland habitats, data collection.', N'Ba Vi Forest', N'Forest Cabin', N'Breakfast: Cabin Kitchen, Lunch: Forest Picnic, Dinner: Cabin Kitchen')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (29, 19, 3, N'Final surveys, wrap-up session.', N'Ba Vi Forest', N'Forest Cabin', N'Breakfast: Cabin Kitchen, Lunch: Forest Picnic, Dinner: Cabin Kitchen')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (30, 20, 1, N'Departure to open Landscapes, birdwatching sessions in grasslands.', N'Vietnam Wildlife Conservation Center', N'Center Guesthouse', N'Breakfast: Guesthouse Restaurant, Lunch: Center Cafeteria, Dinner: Guesthouse Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (30, 21, 2, N'Explore savannas and agricultural areas, observe Bulbul birds in their natural habitats.', N'Vietnam Wildlife Conservation Center', N'Center Guesthouse', N'Breakfast: Guesthouse Restaurant, Lunch: Center Cafeteria, Dinner: Guesthouse Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (31, 22, 1, N'Arrival at National Parks, guided tour of park facilities.', N'Hanoi', N'City Hotel', N'Dinner: Local Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (31, 23, 2, N'Birdwatching excursions in different zones of the park.', N'Hanoi', N'City Hotel', N'Dinner: Local Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (32, 24, 1, N'Arrival at Jungle Reserve, set up camp, introductory session about Bulbul birds.', N'Viet Nam', N'Forest Lodge', N'Breakfast: Lodge Restaurant, Lunch: Packed lunch in the field, Dinner: Lodge Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (32, 25, 2, N'Guided hike through the jungle, birdwatching sessions, evening campfire and storytelling.', N'Viet Nam', N'Forest Lodge', N'Breakfast: Lodge Restaurant, Lunch: Packed lunch in the field, Dinner: Lodge Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (33, 26, 1, N'Departure to various habitats, birdwatching in forests., return to base camp, closing ceremony.', N'Ba Be Lake', N'Lakeview Resort', N'Breakfast: Resort Restaurant, Lunch: Lakefront picnic, Dinner: Resort Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (33, 27, 2, N'Explore grasslands and wetlands, identify different species of Bulbul birds.', N'Ba Be Lake', N'Lakeview Resort', N'Breakfast: Resort Restaurant, Lunch: Lakefront picnic, Dinner: Resort Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (33, 28, 3, N'Final birdwatching sessions, return to base camp, closing ceremony.', N'Ba Be Lake', N'Lakeview Resort', N'Breakfast: Resort Restaurant, Lunch: Lakefront picnic, Dinner: Resort Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (34, 29, 1, N'Travel to Highland Mountains, setup base camp.', N'Cuc Phuong National Park', N'Park Guesthouse', N'Breakfast: Guesthouse Restaurant, Lunch: Park cafeteria, Dinner: Local restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (34, 30, 2, N'Conduct bird surveys in different altitudes, collect data.', N'Cuc Phuong National Park', N'Park Guesthouse', N'Breakfast: Guesthouse Restaurant, Lunch: Park cafeteria, Dinner: Local restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (34, 31, 3, N'Continue surveys, data analysis session in the evening.', N'Cuc Phuong National Park', N'Park Guesthouse', N'Breakfast: Guesthouse Restaurant, Lunch: Park cafeteria, Dinner: Local restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (34, 32, 4, N'Wrap-up, pack up, and return journey.', N'Cuc Phuong National Park', N'Park Guesthouse', N'Breakfast: Guesthouse Restaurant, Lunch: Park cafeteria, Dinner: Local restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (35, 33, 1, N'Guided walk through pristine forests, birdwatching sessions.', N'Vietnam Bird Sanctuary', N'Sanctuary Campsite', N'Breakfast: Campsite, Lunch: Packed lunch, Dinner: Campfire BBQ')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (36, 34, 1, N'Arrival at Nature Reserves, guided tour of reserve facilities.', N'Tam Coc Wetlands', N'Wetland Eco-lodge', N'Breakfast: Lodge Restaurant, Lunch: Floating restaurant, Dinner: Lodge Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (36, 35, 2, N'Birdwatching excursions in different zones of the reserve.', N'Tam Coc Wetlands', N'Wetland Eco-lodge', N'Breakfast: Lodge Restaurant, Lunch: Floating restaurant, Dinner: Lodge Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (37, 36, 1, N'Introduction to different habitats, begin tracking Bulbul birds.', N'Ba Vi National Park', N'Park Bungalows', N'Breakfast: Bungalow Restaurant, Lunch: Picnic lunch in the park, Dinner: Bungalow Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (37, 37, 2, N'Explore various habitats, study bird behaviors.', N'Ba Vi National Park', N'Park Bungalows', N'Breakfast: Bungalow Restaurant, Lunch: Picnic lunch in the park, Dinner: Bungalow Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (38, 38, 1, N'Nature walk in Green Forest Reserve, birdwatching sessions.', N'Ba Vi Peak', N'Park Campsite', N'Breakfast: Campsite, Lunch: Packed lunch, Dinner: Campfire Dinner')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (39, 39, 1, N'Arrival at Wetland Reserves, setup camp.', N'Ba Vi Forest', N'Forest Cabin', N'Breakfast: Cabin Kitchen, Lunch: Forest Picnic, Dinner: Cabin Kitchen')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (39, 40, 2, N'Bird surveys in wetland habitats, data collection.', N'Ba Vi Forest', N'Forest Cabin', N'Breakfast: Cabin Kitchen, Lunch: Forest Picnic, Dinner: Cabin Kitchen')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (39, 41, 3, N'Final surveys, wrap-up session.', N'Ba Vi Forest', N'Forest Cabin', N'Breakfast: Cabin Kitchen, Lunch: Forest Picnic, Dinner: Cabin Kitchen')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (40, 42, 1, N'Departure to open Landscapes, birdwatching sessions in grasslands.', N'Vietnam Wildlife Conservation Center', N'Center Guesthouse', N'Breakfast: Guesthouse Restaurant, Lunch: Center Cafeteria, Dinner: Guesthouse Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (40, 43, 2, N'Explore savannas and agricultural areas, observe Bulbul birds in their natural habitats.', N'Vietnam Wildlife Conservation Center', N'Center Guesthouse', N'Breakfast: Guesthouse Restaurant, Lunch: Center Cafeteria, Dinner: Guesthouse Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (41, 44, 1, N'Arrival at National Parks, guided tour of park facilities.', N'Hanoi', N'City Hotel', N'Dinner: Local Restaurant')
INSERT [dbo].[FieldTripDaybyDay] ([tripId], [dayByDayId], [day], [description], [mainDestination], [accommodation], [mealsAndDrinks]) VALUES (41, 45, 2, N'Birdwatching excursions in different zones of the park.', N'Hanoi', N'City Hotel', N'Dinner: Local Restaurant')
SET IDENTITY_INSERT [dbo].[FieldTripDaybyDay] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldTripGettingThere] ON 

INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (22, 11, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Tan Son Nhat Airport', N'Off-road vehicles or 4x4 trucks may be used to access remote jungle areas, followed by hiking or trekking on foot.', N'No accommodation can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (23, 12, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Da Nang Airport', N'Transportation could involve buses or vans to reach different habitats, with additional travel by foot or boat depending on the terrain.', N'Additional accommodation before and at the end of the tour can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (24, 13, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Tan Son Nhat Airport', N'Transportation might involve vans or SUVs for mountainous terrain, followed by hiking or trekking to higher elevations.', N'No accommodation can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (25, 14, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Tan Son Nhat Airport', N'Transportation could involve vans or buses to reach forested areas, with guided walks or hikes within the forest on foot.', N'Additional accommodation before and at the end of the tour can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (26, 15, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Phu Quoc Airport', N'Transportation may include vans or buses for travel to nature reserves, followed by guided tours or walks within the reserve.', N'No accommodation can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (27, 16, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Can Tho Airport', N'Transportation options could involve vans or buses to reach various habitats, with guided walks or hikes on foot to follow the bird''s footsteps.', N'Additional accommodation before and at the end of the tour can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (28, 17, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Phu Quoc Airport', N'Transportation may involve vans or buses for travel to forested areas, followed by guided walks or hikes on foot within the forest.', N'Additional accommodation before and at the end of the tour can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (29, 18, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Tan Son Nhat Airport', N'Transportation could include boats or kayaks to navigate wetland areas, with additional travel by foot or vehicle to reach specific observation points.', N'Additional accommodation before and at the end of the tour can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (30, 19, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Cam Ranh Airport', N'Transportation options may involve vans or buses for travel to open landscapes, with guided walks or hikes on foot within the area.', N'No accommodation can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (31, 20, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Tan Son Nhat Airport', N'Transportation could involve vans or buses for travel to national parks, with guided tours or walks within the park on foot or via designated transportation routes.', N'Additional accommodation before and at the end of the tour can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (32, 21, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Tan Son Nhat Airport', N'Off-road vehicles or 4x4 trucks may be used to access remote jungle areas, followed by hiking or trekking on foot.', N'No accommodation can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (33, 22, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Da Nang Airport', N'Transportation could involve buses or vans to reach different habitats, with additional travel by foot or boat depending on the terrain.', N'Additional accommodation before and at the end of the tour can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (34, 23, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Tan Son Nhat Airport', N'Transportation might involve vans or SUVs for mountainous terrain, followed by hiking or trekking to higher elevations.', N'No accommodation can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (35, 24, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Tan Son Nhat Airport', N'Transportation could involve vans or buses to reach forested areas, with guided walks or hikes within the forest on foot.', N'Additional accommodation before and at the end of the tour can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (36, 25, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Phu Quoc Airport', N'Transportation may include vans or buses for travel to nature reserves, followed by guided tours or walks within the reserve.', N'No accommodation can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (37, 26, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Can Tho Airport', N'Transportation options could involve vans or buses to reach various habitats, with guided walks or hikes on foot to follow the bird''s footsteps.', N'Additional accommodation before and at the end of the tour can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (38, 27, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Phu Quoc Airport', N'Transportation may involve vans or buses for travel to forested areas, followed by guided walks or hikes on foot within the forest.', N'Additional accommodation before and at the end of the tour can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (39, 28, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Tan Son Nhat Airport', N'Transportation could include boats or kayaks to navigate wetland areas, with additional travel by foot or vehicle to reach specific observation points.', N'Additional accommodation before and at the end of the tour can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (40, 29, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Cam Ranh Airport', N'Transportation options may involve vans or buses for travel to open landscapes, with guided walks or hikes on foot within the area.', N'No accommodation can be arranged for an extra cost')
INSERT [dbo].[FieldTripGettingThere] ([tripId], [gettingThereId], [gettingThereStartEnd], [gettingThereFlight], [gettingThereTransportation], [gettingThereAccommodation]) VALUES (41, 30, N'This tour starts and ends in Ho Chi Minh City', N'Fly from Tan Son Nhat Airport', N'Transportation could involve vans or buses for travel to national parks, with guided tours or walks within the park on foot or via designated transportation routes.', N'Additional accommodation before and at the end of the tour can be arranged for an extra cost')
SET IDENTITY_INSERT [dbo].[FieldTripGettingThere] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldTripInclusions] ON 

INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (22, 1, N'Camping gear', N'Tents, sleeping bags, cooking equipment', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (23, 2, N'Binoculars and spotting scopes', N'Spotting', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (24, 3, N'Altitude sickness medication', N'Medication provided for safety', N'Health', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (25, 4, N'Field guides on local flora and fauna', N'Enrich your experience', N'Reference', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (26, 5, N'Park entrance fees', N'Included in the package', N'Admission', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (27, 6, N'Bird checklist', N'Help you identify species', N'Reference', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (28, 7, N'Nature journal', N'Recording observations and memories', N'Reference', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (29, 8, N'Waterproof gear', N'Keep you dry', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (30, 9, N'Sunscreen and hats', N'Sun protection', N'Health', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (31, 10, N'Park maps', N'Navigation and exploration', N'Reference', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (32, 11, N'Safety Equipment', N'Provision of safety equipment such as first aid kits', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (33, 12, N'Guided Tours', N'Expert-led tours of Bulbul habitats and observation sites', N'Service', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (34, 13, N'Transportation', N'Round-trip transportation to field trip locations', N'Service', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (35, 14, N'Accommodation', N'Lodging arrangements at designated accommodations', N'Service', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (36, 15, N'Meals', N'Daily meals provided during the field trip', N'Service', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (37, 16, N'Expert Guidance', N'Guidance and interpretation by experienced naturalists', N'Service', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (38, 17, N'Entrance Fees', N'Fees for entry to national parks or protected areas', N'Fees', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (39, 18, N'Field Equipment', N'Basic field equipment such as binoculars and field guides', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (40, 19, N'Workshops', N'Workshops on bird identification, photography, etc.', N'Activity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (41, 20, N'Local Cultural Experiences', N'Opportunities to experience local culture and traditions', N'Activity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (22, 21, N'Bulbul Identification Guide', N'A guidebook to help identify different species of Bulbul birds', N'Reference', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (22, 22, N'Birdwatching Binoculars', N'High-quality binoculars for better bird observation', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (22, 23, N'Campfire Equipment', N'Wood, matches, and other supplies for campfires', N'Equipment', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (22, 24, N'Field Trip Photography Workshop', N'A workshop on bird photography techniques', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (23, 25, N'Night Vision Goggles', N'Goggles to aid in night-time bird observation', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (23, 26, N'Rain Gear', N'Raincoats and umbrellas to stay dry during rainy weather', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (23, 27, N'Bulbul Audio Guide', N'An audio guide with bird calls for Bulbul species', N'Reference', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (23, 28, N'First Aid Kit', N'Essential medical supplies for emergencies', N'Health', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (24, 29, N'Botanical Guidebook', N'A guide to local plants and trees frequented by Bulbuls', N'Reference', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (24, 30, N'Bird Feeding Equipment', N'Seed and feeders to attract Bulbuls for observation', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (24, 31, N'Satellite Phone', N'A communication device for emergencies in remote areas', N'Equipment', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (24, 32, N'Local Cuisine Sampling', N'Opportunities to taste traditional Bulbul bird habitat cuisine', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (25, 33, N'Bulbul Habitat Preservation Fund', N'A contribution to support Bulbul habitat conservation efforts', N'Charity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (25, 34, N'Adventure Photography Tour', N'A guided photography tour of Bulbul habitats', N'Activity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (25, 35, N'Bird Migration Seminar', N'A seminar on Bulbul migration patterns and behaviors', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (25, 36, N'Field Trip T-Shirt', N'A commemorative t-shirt for participants', N'Memorabilia', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (26, 37, N'Binocular Harness', N'A comfortable harness for carrying binoculars during hikes', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (26, 38, N'Bulbul Nesting Site Workshop', N'A workshop on identifying and preserving Bulbul nesting sites', N'Activity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (26, 39, N'Local Naturalist Guide', N'A guide with extensive knowledge of local Bulbul habitats', N'Service', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (26, 40, N'Bulbul Habitat Restoration Project', N'A hands-on project to restore Bulbul habitats', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (27, 41, N'Bulbul Field Guide App', N'A mobile application with detailed information on Bulbul species', N'Reference', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (27, 42, N'Spotting Scope', N'A high-powered spotting scope for distant bird observation', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (27, 43, N'Campsite Cooking Lessons', N'Cooking lessons on preparing meals using campfire', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (27, 44, N'Bulbul Call Playback Device', N'A device for playing recorded Bulbul calls to attract birds', N'Equipment', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (28, 45, N'Sleeping Bag', N'A warm and comfortable sleeping bag for overnight stays', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (28, 46, N'Nature Sketching Workshop', N'A workshop on sketching birds and their habitats', N'Activity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (28, 47, N'Field Trip Journal', N'A journal for recording observations and thoughts during the trip', N'Reference', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (28, 48, N'Bulbul Conservation Talk', N'A talk on the importance of conservation efforts for Bulbuls', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (29, 49, N'Emergency Whistle', N'A whistle for signaling in case of emergencies', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (29, 50, N'Bulbul Habitat Restoration Kit', N'A kit containing tools for habitat restoration activities', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (29, 51, N'Local Folklore Stories', N'Stories about Bulbuls from local folklore and traditions', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (29, 52, N'Wildlife Photography Competition', N'A friendly photography competition among participants', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (30, 53, N'Bulbul Coloring Book', N'A coloring book featuring Bulbul birds and their habitats', N'Reference', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (30, 54, N'Solar Power Bank', N'A portable power bank charged by solar energy for electronic devices', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (30, 55, N'Bulbul Song Contest', N'A contest for participants to imitate Bulbul bird songs', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (30, 56, N'Local Plant Identification Guide', N'A guidebook for identifying plants in Bulbul habitats', N'Reference', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (31, 57, N'Birdwatching Journal Template', N'A template for participants to log birdwatching observations', N'Reference', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (31, 58, N'Geocaching Adventure', N'An outdoor treasure hunt using GPS coordinates', N'Activity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (31, 59, N'Bulbul Storytelling Session', N'A storytelling session featuring tales of Bulbul birds', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (31, 60, N'Medical Evacuation Plan', N'A plan for emergency medical evacuation from remote locations', N'Health', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (32, 61, N'Fire Extinguisher', N'A safety device for extinguishing small fires', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (32, 62, N'Bulbul Coloring Contest', N'A coloring contest for participants of all ages', N'Activity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (32, 63, N'Bulbul Art Exhibition', N'An exhibition featuring artwork inspired by Bulbul birds', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (32, 64, N'Bulbul Birdhouse Building Workshop', N'A workshop on building birdhouses for Bulbul nesting sites', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (33, 65, N'Bulbul Habitat Documentary Screening', N'A screening of documentaries about Bulbul habitats and conservation', N'Activity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (33, 66, N'Bulbul Birding Photo Contest', N'A photo contest showcasing the best birding photographs', N'Activity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (33, 67, N'Environmental Cleanup', N'A community service activity to clean up Bulbul habitats', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (33, 68, N'Bulbul Birdsong CD', N'A CD containing recordings of Bulbul birdsongs for relaxation', N'Reference', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (34, 69, N'Artificial Nesting Boxes', N'Nesting boxes provided to support Bulbul conservation efforts', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (34, 70, N'Bulbul Birding Quiz', N'A quiz to test participants’ knowledge of Bulbul birds and their habitats', N'Activity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (34, 71, N'Local Herbal Medicine Workshop', N'A workshop on using local herbs for health and wellness', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (34, 72, N'Bulbul Birdsong Alarm Clock', N'An alarm clock with recorded Bulbul birdsongs to wake participants', N'Reference', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (35, 73, N'Portable Water Filter', N'A filter for purifying water from natural sources', N'Equipment', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (35, 74, N'Bulbul Birding Trivia Night', N'A trivia night with questions about Bulbul birds and birdwatching', N'Activity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (35, 75, N'Nature Soundscapes CD', N'A CD featuring natural sounds of Bulbul habitats for relaxation', N'Reference', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (35, 76, N'Bulbul Birding Bingo', N'A bingo game with birdwatching-themed bingo cards', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (36, 77, N'Nature Documentary Screening', N'A screening of documentaries about local wildlife and ecosystems', N'Activity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (36, 78, N'Bulbul Birdwatching Marathon', N'A marathon birdwatching session to observe as many Bulbul species as possible', N'Activity', N'Included')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (36, 79, N'Local Handicraft Workshop', N'A workshop on creating handicrafts inspired by Bulbul birds', N'Activity', N'Excluded')
INSERT [dbo].[FieldTripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type], [inclusiontype]) VALUES (36, 80, N'Bulbul Birding Quiz Night', N'A quiz night with questions about Bulbul bird identification and behavior', N'Activity', N'Excluded')
SET IDENTITY_INSERT [dbo].[FieldTripInclusions] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldTripMedia] ON 

INSERT [dbo].[FieldTripMedia] ([pictureId], [tripId], [description], [image], [type], [dayByDayId]) VALUES (1, 22, N'Scenic views from the Red Bulbul Field Trip', N'https://lh3.googleusercontent.com/proxy/g9RyJwa_Qcp36AcDeLqDNwHoJBQByeC1mFhQ4QnMMWASrg0pZS-sBWyuomc3A3mnIxU5nfOlS5K1Keej9U_z8uuz9UUImDK_CZoIebppihXCCUzEw-uR_pWOaZvxFthbTPdXTJ6LlTeurs0', N'Spotlight', NULL)
INSERT [dbo].[FieldTripMedia] ([pictureId], [tripId], [description], [image], [type], [dayByDayId]) VALUES (2, 25, N'Participants enjoying the nature walk', N'https://baolamdong.vn/file/e7837c02845ffd04018473e6df282e92/dataimages/201701/original/images1809050_T51.jpg', N'Spotlight', NULL)
INSERT [dbo].[FieldTripMedia] ([pictureId], [tripId], [description], [image], [type], [dayByDayId]) VALUES (3, 32, N'Exploring diverse bird habitats during the field trip', N'https://media.istockphoto.com/id/497259957/vi/anh/c%E1%BA%B7p-v%E1%BB%A3-ch%E1%BB%93ng-cao-ni%C3%AAn-n%C4%83ng-%C4%91%E1%BB%99ng-ngo%C3%A0i-tr%E1%BB%9Di-%C4%91i-b%E1%BB%99-%C4%91%C6%B0%E1%BB%9Dng-d%C3%A0i-trong-r%E1%BB%ABng-t%C3%ADnh.jpg?s=612x612&w=0&k=20&c=8NC6cBV0cAQuFKfzzI3EWNUFy7SS9EORMHyHZ69V_Tg=', N'Spotlight', NULL)
INSERT [dbo].[FieldTripMedia] ([pictureId], [tripId], [description], [image], [type], [dayByDayId]) VALUES (4, 34, N'Birdwatching in the early morning', N'https://images2.thanhnien.vn/528068263637045248/2024/3/9/anh-1-17099927660971731505313.jpg', N'Spotlight', NULL)
INSERT [dbo].[FieldTripMedia] ([pictureId], [tripId], [description], [image], [type], [dayByDayId]) VALUES (5, 38, N'Learning about bird behavior from experts', N'https://media.istockphoto.com/id/1356557792/vi/anh/b%E1%BA%A1n-b%C3%A8-s%E1%BB%AD-d%E1%BB%A5ng-%E1%BB%91ng-nh%C3%B2m-trong-r%E1%BB%ABng.jpg?s=612x612&w=0&k=20&c=YROxU1yQEZPPMy9RnT6zsf8PJbZXGdzPTiOrEK6WH4U=', N'Spotlight', NULL)
INSERT [dbo].[FieldTripMedia] ([pictureId], [tripId], [description], [image], [type], [dayByDayId]) VALUES (6, 24, N'Capturing the beauty of rare bird species', N'https://edwinbirdclubstorage.blob.core.windows.net/images/fieldtrip/fieldtrip_image_6.jpg', N'Spotlight', NULL)
INSERT [dbo].[FieldTripMedia] ([pictureId], [tripId], [description], [image], [type], [dayByDayId]) VALUES (7, 25, N'Field trip attendees observing bird nesting sites', N'https://edwinbirdclubstorage.blob.core.windows.net/images/fieldtrip/fieldtrip_image_7.jpg', N'DayByDay', 8)
INSERT [dbo].[FieldTripMedia] ([pictureId], [tripId], [description], [image], [type], [dayByDayId]) VALUES (8, 25, N'Bird enthusiasts documenting their findings', N'https://edwinbirdclubstorage.blob.core.windows.net/images/fieldtrip/fieldtrip_image_8.jpg', N'DayByDay', 9)
INSERT [dbo].[FieldTripMedia] ([pictureId], [tripId], [description], [image], [type], [dayByDayId]) VALUES (9, 26, N'Field trip group exploring a new birding destination', N'https://edwinbirdclubstorage.blob.core.windows.net/images/fieldtrip/fieldtrip_image_9.jpg', N'Spotlight', NULL)
INSERT [dbo].[FieldTripMedia] ([pictureId], [tripId], [description], [image], [type], [dayByDayId]) VALUES (10, 27, N'Memorable moments from the field trip', N'https://edwinbirdclubstorage.blob.core.windows.net/images/fieldtrip/fieldtrip_image_10.jpg', N'Spotlight', NULL)
INSERT [dbo].[FieldTripMedia] ([pictureId], [tripId], [description], [image], [type], [dayByDayId]) VALUES (11, 22, N'Rare moment in the field trip', N'https://edwinbirdclubstorage.blob.core.windows.net/images/fieldtrip/fieldtrip_image_10.jpg', N'DayByDay', 2)
INSERT [dbo].[FieldTripMedia] ([pictureId], [tripId], [description], [image], [type], [dayByDayId]) VALUES (12, 22, N'Part of the field trip', N'https://edwinbirdclubstorage.blob.core.windows.net/images/fieldtrip/fieldtrip_image_8.jpg', N'DayByDay', 3)
INSERT [dbo].[FieldTripMedia] ([pictureId], [tripId], [description], [image], [type], [dayByDayId]) VALUES (13, 22, N'Activity of the field trip', N'https://edwinbirdclubstorage.blob.core.windows.net/images/fieldtrip/fieldtrip_image_6.jpg', N'DayByDay', 2)
SET IDENTITY_INSERT [dbo].[FieldTripMedia] OFF
GO
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'1', 1, N'Checked-In')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'2', 2, N'Checked-In')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'3', 3, N'Checked-In')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'4', 4, N'Checked-In')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'5', 5, N'Checked-In')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'6', 6, N'Checked-In')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'7', 7, N'Checked-In')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'8', 8, N'Checked-In')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'9', 9, N'Checked-In')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (26, N'10', 1, N'Not Checked-In')
GO
SET IDENTITY_INSERT [dbo].[Gallery] ON 

INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (1, N'Beautiful Red Bulbul in Flight', 1, N'red_bulbul_flight.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (2, N'Colorful Plumage of the Red Bulbul', 2, N'plumage_colorful.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (3, N'Close-up of a Red Bulbul Nest', 3, N'nest_closeup.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (4, N'Red Bulbul Perched on a Branch', 4, N'perched_on_branch.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (5, N'Flock of Red Bulbuls in the Morning Sun', 5, N'morning_sun_flock.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (6, N'Red Bulbul Feeding its Chicks', 6, N'feeding_chicks.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (7, N'Red Bulbul Preening its Feathers', 7, N'preening_feathers.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (8, N'Artistic Shot of a Red Bulbul', 8, N'artistic_shot.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (9, N'Red Bulbul Pair Building a Nest', 9, N'building_nest.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (10, N'Red Bulbul in its Natural Habitat', 10, N'natural_habitat.jpg')
SET IDENTITY_INSERT [dbo].[Gallery] OFF
GO
SET IDENTITY_INSERT [dbo].[Location] ON 

INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (1, N'67, Old Street, Hoan Kiem District, Hanoi', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (2, N'89, Hung Vuong Avenue, Hong Bang District, Hai Phong', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (3, N'45, Tran Phu Street, Hai Chau District, Da Nang', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (4, N'112, Nguyen Hue Street, District 1, Ho Chi Minh City', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (5, N'31, Hai Ba Trung Street, Ninh Kieu District, Can Tho', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (6, N'76, Tran Phu Street, Nha Trang City, Khanh Hoa', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (7, N'14, Le Duan Street, Thanh Thai District, Buon Ma Thuot, Dak Lak', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (8, N'50, Quang Trung Street, Le Loi District, Quy Nhon City, Binh Dinh', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (9, N'23, Ly Thuong Kiet Street, Dong Xoai Town, Binh Phuoc', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (10, N'98, Hong Thai Street, Bac Giang City, Bac Giang', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (11, N'116, Lang Thuong, Dong Da, Hanoi', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (12, N'23, Green Valley, District 7, Ho Chi Minh City', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (13, N'105, Tran Hung Dao, District 1, Ho Chi Minh City', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (14, N'78, Nguyen Van Linh, District 2, Ho Chi Minh City', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (15, N'42, Le Loi, District 3, Ho Chi Minh City', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (16, N'15, Hoang Van Thu, District 10, Ho Chi Minh City', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (17, N'29, Nguyen Sinh Sac, Lien Chieu District, Da Nang City', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (18, N'56, Tran Phu, Hai Chau District, Da Nang City', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (19, N'37, Nguyen Van Cu, Cam Le District, Da Nang City', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (20, N'91, Le Duan, Son Tra District, Da Nang City', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (21, N'42, Le Loi, District 3, Ho Chi Minh City', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (22, N'42, Le Loi, District 3, Ho Chi Minh City', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (23, N'29, Nguyen Sinh Sac, Lien Chieu District, Da Nang City', N'')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (24, N'23, Truong Cong Dinh, 7, HCM', N'')
SET IDENTITY_INSERT [dbo].[Location] OFF
GO
SET IDENTITY_INSERT [dbo].[Meeting] ON 

INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (1, N'Gathering of Love for Bulbuls', N'This meeting serves as a platform for Bulbul bird lovers to come together, share experiences, and discuss their passion for these birds.', CAST(N'2024-10-01T00:00:00.000' AS DateTime), CAST(N'2024-10-11T00:00:00.000' AS DateTime), CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-11-02T00:00:00.000' AS DateTime), 0, N'AnhTuan', N'TuanKiet', 3, N'OnHold', N'Foster an inclusive and welcoming atmosphere where participants feel comfortable sharing their knowledge and enthusiasm for Bulbul birds.', 0, 100)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (2, N'Conference of Bulbul Enthusiasts', N'The conference brings together enthusiasts of Bulbul birds to explore topics related to their behavior, conservation, and welfare.', CAST(N'2024-10-02T00:00:00.000' AS DateTime), CAST(N'2024-10-15T00:00:00.000' AS DateTime), CAST(N'2024-11-05T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), 0, N'BichNgoc', N'QuangHuy', 4, N'Postponed', N'Ensure that presentations and discussions are informative and engaging, catering to both experienced enthusiasts and newcomers to the field.', 0, 100)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (3, N'Workshop for Bird Enthusiasts: Bulbuls', N'This seminar is designed for individuals passionate about Bulbul birds to learn from experts, share insights, and discuss challenges.', CAST(N'2024-10-15T00:00:00.000' AS DateTime), CAST(N'2024-11-05T00:00:00.000' AS DateTime), CAST(N'2024-12-01T00:00:00.000' AS DateTime), CAST(N'2024-12-11T00:00:00.000' AS DateTime), 0, N'AnhTuan', N'ThanhPhat', 6, N'Cancelled', N'Encourage active participation and provide opportunities for networking and collaboration among attendees.', 0, 100)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (4, N'Community Meeting for Bulbul Lovers', N'The community meeting aims to strengthen bonds among Bulbul bird enthusiasts, promote conservation efforts, and organize collective actions.', CAST(N'2024-05-10T00:00:00.000' AS DateTime), CAST(N'2024-06-20T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), CAST(N'2024-11-25T00:00:00.000' AS DateTime), 0, N'LanHuong', N'MinhChau', 8, N'Postponed', N'Emphasize the importance of community involvement in bird conservation and encourage members to take an active role in local initiatives.', 0, 100)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (5, N'Bulbul Encounter Session', N'This casual meetup provides an opportunity for Bulbul bird enthusiasts to socialize, exchange stories, and enjoy observing these birds.', CAST(N'2024-10-12T00:00:00.000' AS DateTime), CAST(N'2024-10-25T00:00:00.000' AS DateTime), CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), 1, N'AnhTuan', N'TuanKiet', 3, N'ClosedRegistration', N'Keep the atmosphere relaxed and informal to encourage interaction and camaraderie among participants.', 0, 100)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (6, N'Exciting Symposium on Bulbuls', N'The symposium offers an exciting platform for discussions, presentations, and workshops on various aspects of Bulbul bird biology and ecology.', CAST(N'2024-10-20T00:00:00.000' AS DateTime), CAST(N'2024-11-01T00:00:00.000' AS DateTime), CAST(N'2024-12-10T00:00:00.000' AS DateTime), CAST(N'2024-12-20T00:00:00.000' AS DateTime), 0, N'LanHuong', N'QuangHuy', 6, N'CheckingIn', N'Plan diverse and engaging sessions to cater to different interests within the Bulbul bird enthusiast community.', 0, 100)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (7, N'Symposium for Bird Lovers: Bulbuls', N'This workshop aims to educate and inspire participants about Bulbul birds through interactive sessions, demonstrations, and hands-on activities.', CAST(N'2024-10-30T00:00:00.000' AS DateTime), CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'2024-12-05T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), 0, N'BichNgoc', N'DucAnh', 5, N'Ongoing', N'Provide educational materials and resources to empower participants to become advocates for Bulbul bird conservation.', 0, 100)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (8, N'Bulbul Workshop Session', N'The seminar focuses on sharing knowledge, research findings, and best practices related to the care and conservation of Bulbul birds.', CAST(N'2024-10-20T00:00:00.000' AS DateTime), CAST(N'2024-10-30T00:00:00.000' AS DateTime), CAST(N'2024-11-25T00:00:00.000' AS DateTime), CAST(N'2024-11-28T00:00:00.000' AS DateTime), 0, N'LanHuong', N'QuangHuy', 8, N'Ended', N'Invite expert speakers and encourage open discussions to facilitate learning and knowledge exchange among participants.', 0, 100)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (9, N'Discussion on Conservation of Bulbuls', N'This meeting is dedicated to discussing strategies and initiatives for the conservation and protection of Bulbul bird populations and habitats.', CAST(N'2024-11-03T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), CAST(N'2024-12-25T00:00:00.000' AS DateTime), 0, N'BichNgoc', N'TuanKiet', 7, N'OnHold', N'Emphasize the importance of collective action and collaboration among stakeholders to address conservation challenges effectively.', 0, 100)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (10, N'Workshop on Raising Bulbuls', N'The workshop provides practical guidance and tips on the responsible care, breeding, and welfare of Bulbul birds in captivity.', CAST(N'2024-05-12T00:00:00.000' AS DateTime), CAST(N'2024-06-15T00:00:00.000' AS DateTime), CAST(N'2024-12-20T00:00:00.000' AS DateTime), CAST(N'2024-12-23T00:00:00.000' AS DateTime), 0, N'AnhTuan', N'TuanKiet', 9, N'OpenRegistration', N'Ensure that participants receive accurate and ethical advice to promote the well-being of Bulbul birds kept in captivity.', 0, 100)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (11, N'Seminar on Bulbul Bird Conservation', N'A seminar focusing on the conservation status, threats, and strategies for the protection of Bulbul bird populations in Vietnam.', CAST(N'2024-01-10T00:00:00.000' AS DateTime), CAST(N'2024-01-20T00:00:00.000' AS DateTime), CAST(N'2024-02-10T00:00:00.000' AS DateTime), CAST(N'2024-02-15T00:00:00.000' AS DateTime), 0, N'BichNgoc', N'MinhChau', 11, N'Postponed', N'Discuss challenges and opportunities for Bulbul bird conservation efforts in Vietnam.', 0, 120)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (12, N'Training Workshop on Bulbul Bird Identification', N'A hands-on workshop to enhance participants skills in identifying Bulbul bird species based on plumage, calls, and habitat characteristics.', CAST(N'2024-02-05T00:00:00.000' AS DateTime), CAST(N'2024-02-15T00:00:00.000' AS DateTime), CAST(N'2024-03-05T00:00:00.000' AS DateTime), CAST(N'2024-03-08T00:00:00.000' AS DateTime), 0, N'BichNgoc', N'TuanKiet', 13, N'OpenRegistration', N'Provide participants with practical skills for accurate Bulbul bird identification in the field.', 0, 80)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (13, N'Discussion Forum on Bulbul Bird Behavior', N'A forum for researchers and enthusiasts to share observations and insights into the behavior, ecology, and natural history of Bulbul birds.', CAST(N'2024-02-28T00:00:00.000' AS DateTime), CAST(N'2024-03-10T00:00:00.000' AS DateTime), CAST(N'2024-03-30T00:00:00.000' AS DateTime), CAST(N'2024-04-05T00:00:00.000' AS DateTime), 0, N'LanHuong', N'MinhChau', 14, N'OpenRegistration', N'Facilitate knowledge exchange and collaboration among researchers and birdwatchers interested in Bulbul bird behavior.', 0, 70)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (14, N'Symposium on Bulbul Bird Ecology', N'A symposium featuring presentations and discussions on the ecological roles, interactions, and conservation implications of Bulbul birds in forest ecosystems.', CAST(N'2024-03-15T00:00:00.000' AS DateTime), CAST(N'2024-03-25T00:00:00.000' AS DateTime), CAST(N'2024-04-15T00:00:00.000' AS DateTime), CAST(N'2024-04-20T00:00:00.000' AS DateTime), 0, N'LanHuong', N'MinhChau', 15, N'OpenRegistration', N'Explore the ecological significance of Bulbul birds and their role in forest dynamics and biodiversity conservation.', 0, 100)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (15, N'Interactive Workshop on Bulbul Bird Photography', N'A workshop focusing on techniques and strategies for capturing high-quality photographs of Bulbul birds in their natural habitats.', CAST(N'2024-04-05T00:00:00.000' AS DateTime), CAST(N'2024-04-15T00:00:00.000' AS DateTime), CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-05-15T00:00:00.000' AS DateTime), 0, N'AnhTuan', N'MinhChau', 16, N'Postponed', N'Provide practical guidance and hands-on experience to improve participants skills in Bulbul bird photography.', 0, 80)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (16, N'Field Workshop on Bulbul Bird Survey Techniques', N'A field-based workshop to train participants in conducting systematic surveys and monitoring of Bulbul bird populations in diverse habitats.', CAST(N'2024-04-20T00:00:00.000' AS DateTime), CAST(N'2024-04-30T00:00:00.000' AS DateTime), CAST(N'2024-05-20T00:00:00.000' AS DateTime), CAST(N'2024-05-25T00:00:00.000' AS DateTime), 0, N'LanHuong', N'TuanKiet', 17, N'OpenRegistration', N'Empower participants with practical skills and knowledge to contribute to Bulbul bird conservation efforts through effective survey techniques.', 0, 70)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (17, N'Workshop on Bulbul Bird Vocalization Analysis', N'A workshop focusing on methods and tools for analyzing Bulbul bird vocalizations to understand communication patterns and behaviors.', CAST(N'2024-05-10T00:00:00.000' AS DateTime), CAST(N'2024-05-20T00:00:00.000' AS DateTime), CAST(N'2024-06-10T00:00:00.000' AS DateTime), CAST(N'2024-06-15T00:00:00.000' AS DateTime), 0, N'BichNgoc', N'ThanhPhat', 18, N'OpenRegistration', N'Equip participants with knowledge and tools for conducting Bulbul bird vocalization research and analysis.', 0, 60)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (18, N'Discussion Panel on Bulbul Bird Conservation Challenges', N'A panel discussion involving experts and stakeholders to identify and address conservation challenges facing Bulbul bird populations in urban and rural areas.', CAST(N'2024-06-05T00:00:00.000' AS DateTime), CAST(N'2024-06-15T00:00:00.000' AS DateTime), CAST(N'2024-07-05T00:00:00.000' AS DateTime), CAST(N'2024-07-15T00:00:00.000' AS DateTime), 0, N'LanHuong', N'MinhChau', 19, N'Cancelled', N'Foster dialogue and collaboration among stakeholders to develop effective conservation strategies for Bulbul birds.', 0, 80)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (19, N'Workshop on Bulbul Bird Nesting Ecology', N'A workshop focusing on the nesting ecology, reproductive biology, and conservation of Bulbul birds in various habitats.', CAST(N'2024-06-20T00:00:00.000' AS DateTime), CAST(N'2024-06-30T00:00:00.000' AS DateTime), CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-07-25T00:00:00.000' AS DateTime), 0, N'AnhTuan', N'TuanKiet', 20, N'OpenRegistration', N'Enhance understanding of Bulbul bird nesting ecology and contribute to conservation efforts through effective habitat management.', 0, 70)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (20, N'Symposium on Bulbul Bird Migration Patterns', N'A symposium featuring presentations and discussions on the migration patterns, routes, and conservation of Bulbul birds across different regions.', CAST(N'2024-07-10T00:00:00.000' AS DateTime), CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-08-10T00:00:00.000' AS DateTime), CAST(N'2024-08-15T00:00:00.000' AS DateTime), 0, N'LanHuong', N'TuanKiet', 20, N'Postponed', N'Explore the fascinating migration behavior of Bulbul birds and its implications for conservation and management.', 0, 60)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (21, N'Field Workshop on Bulbul Bird Habitat Restoration', N'A field workshop focusing on habitat restoration techniques to improve Bulbul bird habitats and promote biodiversity conservation.', CAST(N'2024-08-05T00:00:00.000' AS DateTime), CAST(N'2024-08-15T00:00:00.000' AS DateTime), CAST(N'2024-09-05T00:00:00.000' AS DateTime), CAST(N'2024-09-15T00:00:00.000' AS DateTime), 0, N'LanHuong', N'TuanKiet', 12, N'OnHold', N'Demonstrate and promote habitat restoration techniques to enhance Bulbul bird populations and overall ecosystem health.', 0, 80)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (22, N'Seminar on Bulbul Bird Vocalizations and Calls', N'A seminar focusing on the vocalizations, calls, and communication behaviors of Bulbul birds, including field demonstrations and audio recordings.', CAST(N'2024-09-10T00:00:00.000' AS DateTime), CAST(N'2024-09-20T00:00:00.000' AS DateTime), CAST(N'2024-10-10T00:00:00.000' AS DateTime), CAST(N'2024-10-15T00:00:00.000' AS DateTime), 9, N'AnhTuan', N'MinhChau', 13, N'OpenRegistration', N'Explore the diverse vocal repertoire of Bulbul birds and its significance in communication and behavior.', 0, 10)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (23, N'Roundtable Discussion on Bulbul Bird Habitat Fragmentation', N'A roundtable discussion involving experts and stakeholders to address issues related to habitat fragmentation and loss affecting Bulbul bird populations.', CAST(N'2024-10-05T00:00:00.000' AS DateTime), CAST(N'2024-10-15T00:00:00.000' AS DateTime), CAST(N'2024-11-05T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), 0, N'BichNgoc', N'ThanhPhat', 14, N'OpenRegistration', N'Develop strategies to mitigate habitat fragmentation and safeguard Bulbul bird populations and their habitats.', 0, 60)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (24, N'Workshop on Bulbul Bird Diet and Foraging Behavior', N'A workshop focusing on the diet, feeding ecology, and foraging behavior of Bulbul birds in different habitats and seasons.', CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'2024-11-20T00:00:00.000' AS DateTime), CAST(N'2024-12-10T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), 0, N'BichNgoc', N'MinhChau', 15, N'Postponed', N'Examine the dietary preferences and foraging strategies of Bulbul birds to inform conservation and management efforts.', 0, 80)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (25, N'Symposium on Bulbul Bird Genetic Diversity', N'A symposium featuring presentations and discussions on the genetic diversity, population structure, and conservation genetics of Bulbul birds.', CAST(N'2024-12-05T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), CAST(N'2024-01-05T00:00:00.000' AS DateTime), CAST(N'2024-01-15T00:00:00.000' AS DateTime), 0, N'AnhTuan', N'QuangHuy', 16, N'OpenRegistration', N'Explore the genetic variation and evolutionary history of Bulbul birds to guide conservation and management decisions.', 0, 70)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (26, N'Field Workshop on Bulbul Bird Roosting Behavior', N'A field workshop focusing on the roosting behavior, roost site selection, and communal roosting dynamics of Bulbul birds.', CAST(N'2024-01-10T00:00:00.000' AS DateTime), CAST(N'2024-01-20T00:00:00.000' AS DateTime), CAST(N'2024-02-10T00:00:00.000' AS DateTime), CAST(N'2024-02-15T00:00:00.000' AS DateTime), 0, N'BichNgoc', N'QuangHuy', 17, N'OpenRegistration', N'Investigate the roosting ecology and social behavior of Bulbul birds to support conservation and habitat management efforts.', 0, 60)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (27, N'Seminar on Bulbul Bird Reproductive Biology', N'A seminar focusing on the reproductive biology, breeding ecology, and nesting behavior of Bulbul birds in various habitats and environmental conditions.', CAST(N'2024-02-05T00:00:00.000' AS DateTime), CAST(N'2024-02-15T00:00:00.000' AS DateTime), CAST(N'2024-03-05T00:00:00.000' AS DateTime), CAST(N'2024-03-15T00:00:00.000' AS DateTime), 0, N'AnhTuan', N'MinhChau', 18, N'Cancelled', N'Enhance understanding of Bulbul bird reproductive biology and contribute to the conservation of breeding populations.', 0, 70)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (28, N'Workshop on Bulbul Bird Habitat Selection', N'A workshop focusing on habitat selection criteria, preferences, and adaptations of Bulbul birds across different landscapes and ecological gradients.', CAST(N'2024-03-10T00:00:00.000' AS DateTime), CAST(N'2024-03-20T00:00:00.000' AS DateTime), CAST(N'2024-04-10T00:00:00.000' AS DateTime), CAST(N'2024-04-15T00:00:00.000' AS DateTime), 0, N'LanHuong', N'ThanhPhat', 19, N'OpenRegistration', N'Investigate factors influencing Bulbul bird habitat selection and apply findings to habitat management and conservation.', 0, 80)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (29, N'Discussion Panel on Bulbul Bird Threats and Conservation', N'A panel discussion involving experts and stakeholders to assess threats and develop strategies for the conservation of Bulbul birds and their habitats.', CAST(N'2024-04-05T00:00:00.000' AS DateTime), CAST(N'2024-04-15T00:00:00.000' AS DateTime), CAST(N'2024-05-05T00:00:00.000' AS DateTime), CAST(N'2024-05-15T00:00:00.000' AS DateTime), 0, N'BichNgoc', N'ThanhPhat', 20, N'OpenRegistration', N'Identify and prioritize conservation actions to mitigate threats facing Bulbul birds and their habitats.', 0, 70)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (30, N'Workshop on Bulbul Bird Health and Disease', N'A workshop focusing on the health, diseases, and veterinary care of Bulbul birds in captivity and the wild, including preventive measures and treatment options.', CAST(N'2024-05-10T00:00:00.000' AS DateTime), CAST(N'2024-05-20T00:00:00.000' AS DateTime), CAST(N'2024-06-10T00:00:00.000' AS DateTime), CAST(N'2024-06-15T00:00:00.000' AS DateTime), 0, N'LanHuong', N'MinhChau', 11, N'OpenRegistration', N'Provide knowledge and resources to ensure the health and well-being of Bulbul birds in captivity and the wild.', 0, 70)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (31, N'Symposium on Bulbul Bird Social Behavior', N'A symposium featuring presentations and discussions on the social structure, communication, and interactions of Bulbul birds in flocks and breeding pairs.', CAST(N'2024-06-05T00:00:00.000' AS DateTime), CAST(N'2024-06-15T00:00:00.000' AS DateTime), CAST(N'2024-07-05T00:00:00.000' AS DateTime), CAST(N'2024-07-15T00:00:00.000' AS DateTime), 0, N'AnhTuan', N'QuangHuy', 12, N'Postponed', N'Explore the social dynamics and cooperative behaviors of Bulbul birds and their implications for conservation and management.', 0, 60)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (32, N'Field Workshop on Bulbul Bird Habitat Use', N'A field-based workshop to study the habitat use patterns, resource requirements, and ecological niches of Bulbul birds in diverse landscapes.', CAST(N'2024-07-10T00:00:00.000' AS DateTime), CAST(N'2024-07-20T00:00:00.000' AS DateTime), CAST(N'2024-08-10T00:00:00.000' AS DateTime), CAST(N'2024-08-15T00:00:00.000' AS DateTime), 0, N'BichNgoc', N'MinhChau', 3, N'Postponed', N'Investigate habitat preferences and use patterns of Bulbul birds to inform habitat conservation and management strategies.', 0, 60)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (33, N'Workshop on Bulbul Bird Conservation Education', N'A workshop focusing on educational strategies and materials for raising awareness and promoting conservation of Bulbul birds and their habitats.', CAST(N'2024-08-05T00:00:00.000' AS DateTime), CAST(N'2024-08-15T00:00:00.000' AS DateTime), CAST(N'2024-09-05T00:00:00.000' AS DateTime), CAST(N'2024-09-15T00:00:00.000' AS DateTime), 0, N'LanHuong', N'ThanhPhat', 4, N'Cancelled', N'Develop educational programs and resources to engage communities in Bulbul bird conservation.', 0, 70)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (34, N'Symposium on Bulbul Bird Population Dynamics', N'A symposium featuring presentations and discussions on the population dynamics, trends, and monitoring techniques of Bulbul birds in different habitats.', CAST(N'2024-09-10T00:00:00.000' AS DateTime), CAST(N'2024-09-20T00:00:00.000' AS DateTime), CAST(N'2024-10-10T00:00:00.000' AS DateTime), CAST(N'2024-10-15T00:00:00.000' AS DateTime), 0, N'BichNgoc', N'ThanhPhat', 5, N'Postponed', N'Assess population trends and dynamics of Bulbul birds to inform conservation and management decisions.', 0, 60)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (35, N'Workshop on Bulbul Bird Habitat Restoration Techniques', N'A workshop focusing on practical techniques and strategies for restoring degraded habitats and creating suitable habitats for Bulbul birds.', CAST(N'2024-10-05T00:00:00.000' AS DateTime), CAST(N'2024-10-15T00:00:00.000' AS DateTime), CAST(N'2024-11-05T00:00:00.000' AS DateTime), CAST(N'2024-11-15T00:00:00.000' AS DateTime), 0, N'BichNgoc', N'MinhChau', 6, N'OnHold', N'Demonstrate and promote habitat restoration techniques to enhance Bulbul bird populations and overall ecosystem health.', 0, 70)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (36, N'Seminar on Bulbul Bird Vocal Mimicry', N'A seminar focusing on the vocal mimicry abilities of Bulbul birds, including the functions, mechanisms, and ecological significance of mimicry behavior.', CAST(N'2024-11-10T00:00:00.000' AS DateTime), CAST(N'2024-11-20T00:00:00.000' AS DateTime), CAST(N'2024-12-10T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), 0, N'AnhTuan', N'MinhChau', 7, N'Cancelled', N'Explore the fascinating phenomenon of vocal mimicry in Bulbul birds and its adaptive significance in communication and behavior.', 0, 70)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (37, N'Workshop on Bulbul Bird Conservation Planning', N'A workshop focusing on collaborative planning processes and tools for developing effective conservation plans and actions for Bulbul birds and their habitats.', CAST(N'2024-12-05T00:00:00.000' AS DateTime), CAST(N'2024-12-15T00:00:00.000' AS DateTime), CAST(N'2024-01-05T00:00:00.000' AS DateTime), CAST(N'2024-01-15T00:00:00.000' AS DateTime), 0, N'LanHuong', N'QuangHuy', 8, N'OnHold', N'Develop collaborative conservation plans and strategies to safeguard Bulbul birds and their habitats.', 0, 70)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (38, N'Symposium on Bulbul Bird Hybridization', N'A symposium featuring presentations and discussions on hybridization events, mechanisms, and implications for conservation and evolutionary biology of Bulbul birds.', CAST(N'2024-01-10T00:00:00.000' AS DateTime), CAST(N'2024-01-20T00:00:00.000' AS DateTime), CAST(N'2024-02-10T00:00:00.000' AS DateTime), CAST(N'2024-02-15T00:00:00.000' AS DateTime), 0, N'AnhTuan', N'QuangHuy', 9, N'Cancelled', N'Examine the occurrence and implications of hybridization in Bulbul birds and its relevance to conservation management.', 0, 60)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (39, N'Field Workshop on Bulbul Bird Behavior and Ecology', N'A field workshop focusing on observing and studying Bulbul bird behavior, ecology, and interactions in natural habitats.', CAST(N'2024-02-05T00:00:00.000' AS DateTime), CAST(N'2024-02-15T00:00:00.000' AS DateTime), CAST(N'2024-03-05T00:00:00.000' AS DateTime), CAST(N'2024-03-15T00:00:00.000' AS DateTime), 0, N'LanHuong', N'Tien Giang Birdwatching Society', 10, N'Postponed', N'Conduct field-based research to advance understanding of Bulbul bird behavior and ecology for conservation purposes.', 0, 70)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (40, N'Seminar on Bulbul Bird Vocal Repertoire', N'A seminar focusing on the vocal repertoire, communication functions, and cultural significance of Bulbul bird vocalizations in traditional and contemporary contexts.', CAST(N'2024-03-10T00:00:00.000' AS DateTime), CAST(N'2024-03-20T00:00:00.000' AS DateTime), CAST(N'2024-04-10T00:00:00.000' AS DateTime), CAST(N'2024-04-15T00:00:00.000' AS DateTime), 0, N'AnhTuan', N'QuangHuy', 1, N'OpenRegistration', N'Explore the diversity and cultural significance of Bulbul bird vocalizations and their role in local traditions and biodiversity conservation.', 0, 70)
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [openRegistration], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsMinReq], [numberOfParticipantsLimit]) VALUES (41, N'Bulbul Discussion', N'<p>Meeting</p>
', CAST(N'2024-06-09T11:12:00.000' AS DateTime), CAST(N'2024-06-19T11:12:00.000' AS DateTime), CAST(N'2024-06-29T11:12:00.000' AS DateTime), CAST(N'2024-06-30T11:12:00.000' AS DateTime), 0, N'John Doe', N'Nguyen Quang Huy', 24, N'OnHold', N'Note', 10, 200)
SET IDENTITY_INSERT [dbo].[Meeting] OFF
GO
SET IDENTITY_INSERT [dbo].[MeetingMedia] ON 

INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image], [type]) VALUES (1, 4, N'Memorable moments from the Red Bulbul Club Meeting', N'https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_1.png', N'Spotlight')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image], [type]) VALUES (2, 10, N'Guest speaker sharing insights about bird conservation', N'https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_2.jpg', N'Spotlight')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image], [type]) VALUES (3, 12, N'Members engaged in a lively discussion', N'https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_3.jpg', N'Spotlight')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image], [type]) VALUES (4, 14, N'Exploring new ideas for the bird club', N'https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_4.jpg', N'Spotlight')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image], [type]) VALUES (5, 16, N'Highlights from the annual meeting', N'https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_5.jpg', N'Spotlight')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image], [type]) VALUES (6, 17, N'Club members sharing their experiences', N'https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_6.jpg', N'Spotlight')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image], [type]) VALUES (7, 19, N'Bird enthusiasts showcasing their latest findings', N'https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_7.jpg', N'Spotlight')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image], [type]) VALUES (8, 22, N'Learning about rare bird species', N'https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_8.jpg', N'Spotlight')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image], [type]) VALUES (9, 23, N'Interactive session on bird photography techniques', N'https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_9.jpg', N'Spotlight')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image], [type]) VALUES (10, 25, N'Networking and socializing at the club meeting', N'https://edwinbirdclubstorage.blob.core.windows.net/images/meeting/meeting_image_10.jpg', N'Spotlight')
SET IDENTITY_INSERT [dbo].[MeetingMedia] OFF
GO
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (5, N'10', 1, N'Checked-In')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'1', 1, N'Checked-In')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'2', 2, N'Checked-In')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'3', 3, N'Checked-In')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'4', 4, N'Checked-In')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'5', 5, N'Checked-In')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'6', 6, N'Checked-In')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'7', 7, N'Checked-In')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'8', 8, N'Checked-In')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'9', 9, N'Checked-In')
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'1', N'John Doe', N'john_doe', N'john.doe@email.com', N'Male', N'Manager', N'123, Nguyen Trai Street, Thanh Xuan, Hanoi', N'555-1234', NULL, N'Active', CAST(N'2024-01-15T14:23:45.000' AS DateTime), CAST(N'2024-02-02T09:15:32.000' AS DateTime), CAST(N'2024-08-02T17:48:11.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'10', N'Sophia Davis', N'sophia_d', N'sophia.d@email.com', N'Female', N'Manager', N'456, Le Loi Avenue, District 1, Ho Chi Minh City', N'555-3456', N'Wildlife advocate', N'Active', CAST(N'2024-02-15T07:45:30.000' AS DateTime), CAST(N'2024-03-04T19:12:27.000' AS DateTime), CAST(N'2024-09-04T03:23:49.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'11', N'Dao Minh Hoa', N'MinhHoa', N'dm.hoa01@gmail.com', N'Male', N'Admin', N'149/9, Nhieu Tu Street, Phu Nhuan District, Ho Chi Minh City', N'0906693410', NULL, N'Active', CAST(N'2024-02-15T18:57:53.000' AS DateTime), CAST(N'2024-03-29T15:04:09.000' AS DateTime), CAST(N'2024-10-15T04:32:18.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'12', N'Truong Minh Huan', N'MinhHuan', N'tmh@gmail.com', N'Male', N'Member', N'789, Hoang Dieu Boulevard, Hai Chau, Da Nang', N'059 059 3641', NULL, N'Active', CAST(N'2024-01-07T10:21:18.000' AS DateTime), CAST(N'2024-02-01T17:43:06.000' AS DateTime), CAST(N'2024-10-15T11:59:54.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'13', N'Duong Thanh Tuan', N'ThanhTuan', N'duongthanhtuan33@hotmail.com', N'Male', N'Member', N'101, Tran Hung Dao Street, Ninh Kieu, Can Tho', N'084 346 1580', NULL, N'Active', CAST(N'2024-01-15T03:14:36.000' AS DateTime), CAST(N'2024-02-01T06:49:22.000' AS DateTime), CAST(N'2024-10-15T19:45:11.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'14', N'Le Duc Hoang', N'DucHoang', N'leduchoanggg987@facebook.com', N'Male', N'Member', N'234, Le Thanh Tong Road, Hong Bang, Hai Phong', N'086 932 6514', NULL, N'Active', CAST(N'2024-01-24T23:54:38.000' AS DateTime), CAST(N'2024-02-10T14:27:09.000' AS DateTime), CAST(N'2024-08-10T09:03:22.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'15', N'Mac Xuan Han', N'XuanHan', N'macxuanhan836@facebook.com', N'Male', N'Member', N'567, Nguyen Hue Avenue, Thanh Khe, Da Nang', N'078 086 3257', NULL, N'Expired', CAST(N'2024-01-15T13:32:14.000' AS DateTime), CAST(N'2024-02-01T15:18:09.000' AS DateTime), CAST(N'2024-01-15T07:14:29.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'16', N'Doan Tri Huu', N'TriHuu', N'doantrihuu22@gmail.com', N'Male', N'Member', N'112, Trung Kinh Road, Cau Giay, Hanoi', N'092 784 1346', NULL, N'Active', CAST(N'2024-02-01T08:17:56.000' AS DateTime), CAST(N'2024-02-17T18:39:29.000' AS DateTime), CAST(N'2024-10-17T04:23:48.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'17', N'Nguyen Duc Quan', N'DucQuan', N'nguyenducquan10012000@facebook.com', N'Male', N'Member', N'431, Hang Bong Street, Hoan Kiem, Hanoi', N'096 463 7289', NULL, N'Active', CAST(N'2024-01-10T16:27:41.000' AS DateTime), CAST(N'2024-02-01T09:42:36.000' AS DateTime), CAST(N'2024-08-01T11:53:44.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'18', N'Pham Ngoc Ha', N'NgocHa', N'phamngocha30@gmail.com', N'Female', N'Member', N'123, Dien Bien Phu Avenue, Ba Dinh, Hanoi', N'087 462 5731', NULL, N'Expired', CAST(N'2024-01-13T06:43:58.000' AS DateTime), CAST(N'2024-02-02T20:19:22.000' AS DateTime), CAST(N'2024-01-13T14:59:32.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'19', N'Vu Thi Trang', N'ThiTrang', N'vuthitrang37@yahoo.com', N'Female', N'Member', N'123, Le Loi Avenue, District 1, Ho Chi Minh City', N'091 378 2468', NULL, N'Active', CAST(N'2024-01-22T05:23:17.000' AS DateTime), CAST(N'2024-02-15T11:04:51.000' AS DateTime), CAST(N'2024-08-15T22:15:29.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'2', N'Jane Smith', N'jane_smith', N'jane.smith@email.com', N'Female', N'Admin', N'213, Pham Van Dong Road, Thu Duc, Ho Chi Minh City', N'555-5678', NULL, N'Active', CAST(N'2024-02-27T11:37:22.000' AS DateTime), CAST(N'2024-03-17T13:44:55.000' AS DateTime), CAST(N'2024-09-17T19:22:33.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'20', N'Le Minh Tri', N'MinhTri', N'leminhtri121@facebook.com', N'Male', N'Member', N'34, Thanh Nhan Street, Hai Ba Trung, Hanoi', N'083 573 6824', NULL, N'Active', CAST(N'2024-01-19T19:32:08.000' AS DateTime), CAST(N'2024-02-01T07:21:47.000' AS DateTime), CAST(N'2024-08-01T12:49:05.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'21', N'Nguyen Quang Huy', N'QuangHuy', N'quanghuy@gmail.com', N'Male', N'Staff', N'123, Hoang Quoc Viet Street, Cau Giay District, Hanoi', N'0912345671', NULL, N'Active', CAST(N'2024-01-05T00:00:00.000' AS DateTime), CAST(N'2024-01-06T03:23:03.000' AS DateTime), CAST(N'2025-01-05T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'22', N'Tran Bich Ngoc', N'BichNgoc', N'bichngoc@gmail.com', N'Female', N'Manager', N'456, Nguyen Trai Street, Thanh Xuan District, Hanoi', N'0912345672', NULL, N'Active', CAST(N'2024-02-10T00:00:00.000' AS DateTime), CAST(N'2024-02-11T07:12:49.000' AS DateTime), CAST(N'2025-02-10T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'23', N'Le Minh Chau', N'MinhChau', N'minhchau@gmail.com', N'Female', N'Staff', N'789, Le Duan Street, Dong Da District, Hanoi', N'0912345673', NULL, N'Active', CAST(N'2024-03-15T05:23:17.000' AS DateTime), CAST(N'2024-03-16T23:20:50.000' AS DateTime), CAST(N'2025-03-15T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'24', N'Pham Anh Tuan', N'AnhTuan', N'anhtuan@gmail.com', N'Male', N'Staff', N'321, Kim Ma Street, Ba Dinh District, Hanoi', N'0912345674', NULL, N'Active', CAST(N'2024-04-20T00:00:00.000' AS DateTime), CAST(N'2024-04-21T10:17:52.000' AS DateTime), CAST(N'2025-04-20T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'25', N'Vu Thanh Phat', N'ThanhPhat', N'thanhphat@gmail.com', N'Male', N'Manager', N'654, Tran Hung Dao Street, Hoan Kiem District, Hanoi', N'0912345675', NULL, N'Active', CAST(N'2024-05-25T04:30:56.000' AS DateTime), CAST(N'2024-05-26T18:43:20.000' AS DateTime), CAST(N'2025-05-25T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'26', N'Nguyen Duc Anh', N'DucAnh', N'ducanh@gmail.com', N'Male', N'Member', N'987, Giai Phong Street, Hai Ba Trung District, Hanoi', N'0912345676', NULL, N'Active', CAST(N'2024-01-22T20:03:02.000' AS DateTime), CAST(N'2024-02-11T06:30:30.000' AS DateTime), CAST(N'2024-10-11T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'27', N'Tran Lan Huong', N'LanHuong', N'lanhuong@gmail.com', N'Female', N'Member', N'654, Pham Van Dong Street, Tu Liem District, Hanoi', N'0912345677', NULL, N'Active', CAST(N'2024-01-27T10:44:37.000' AS DateTime), CAST(N'2024-02-14T08:06:09.000' AS DateTime), CAST(N'2024-10-14T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'28', N'Le Gia Bao', N'GiaBao', N'giabao@gmail.com', N'Male', N'Staff', N'789, Doi Can Street, Ba Dinh District, Hanoi', N'0912345678', NULL, N'Active', CAST(N'2024-01-15T10:20:02.000' AS DateTime), CAST(N'2024-02-01T00:00:00.000' AS DateTime), CAST(N'2024-10-15T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'29', N'Nguyen Thuy Linh', N'ThuyLinh', N'thuylinh@gmail.com', N'Female', N'Staff', N'123, Hang Bong Street, Hoan Kiem District, Hanoi', N'0912345679', NULL, N'Active', CAST(N'2024-01-15T08:17:47.000' AS DateTime), CAST(N'2024-03-22T07:50:03.000' AS DateTime), CAST(N'2024-07-22T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'3', N'Robert Johnson', N'robert_j', N'robert.j@email.com', N'Male', N'Manager', N'325, Kim Ma Street, Ba Dinh, Hanoi', N'555-9876', N'Avian researcher', N'Active', CAST(N'2024-03-15T08:12:47.000' AS DateTime), CAST(N'2024-04-03T20:05:59.000' AS DateTime), CAST(N'2024-10-03T10:34:21.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'30', N'Do Tuan Kiet', N'TuanKiet', N'tuankiet@gmail.com', N'Male', N'Manager', N'456, Hai Ba Trung Street, Hai Ba Trung District, Hanoi', N'0912345680', NULL, N'Active', CAST(N'2024-01-15T09:22:55.000' AS DateTime), CAST(N'2024-02-01T05:00:11.000' AS DateTime), CAST(N'2024-07-01T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'4', N'Emily White', N'emily_white', N'emily.white@email.com', N'Female', N'Manager', N'487, Dien Bien Phu Avenue, Binh Thanh, Ho Chi Minh City', N'555-5432', N'Nature educator', N'Expired', CAST(N'2024-01-13T17:55:43.000' AS DateTime), CAST(N'2024-02-01T21:18:26.000' AS DateTime), CAST(N'2024-05-15T06:39:54.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'5', N'Michael Green', N'michael_g', N'michael.g@email.com', N'Male', N'Manager', N'591, Nguyen Van Cu Street, Long Bien, Hanoi', N'555-1122', N'Wildlife conservationist', N'Active', CAST(N'2024-01-12T09:27:35.000' AS DateTime), CAST(N'2024-02-01T15:23:09.000' AS DateTime), CAST(N'2024-10-15T18:52:47.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'6', N'Sarah Black', N'sarah_b', N'sarah.b@email.com', N'Female', N'Staff', N'678, Bach Dang Road, Hai Ba Trung, Hanoi', N'555-3344', N'Nature enthusiast', N'Inactive', CAST(N'2024-04-19T00:00:00.000' AS DateTime), CAST(N'2024-04-20T00:00:00.000' AS DateTime), CAST(N'2024-10-20T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'7', N'Daniel Brown', N'daniel_b', N'daniel.b@email.com', N'Male', N'Staff', N'723, Nguyen Dinh Chieu Street, District 3, Ho Chi Minh City', N'555-8765', N'Birdwatching enthusiast', N'Inactive', CAST(N'2024-04-29T00:00:00.000' AS DateTime), CAST(N'2024-04-30T00:00:00.000' AS DateTime), CAST(N'2024-10-30T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'8', N'Olivia Taylor', N'olivia_t', N'olivia.t@email.com', N'Female', N'Manager', N'845, Thanh Nhan Street, Hai Ba Trung, Hanoi', N'555-7890', N'Bird photographer', N'Active', CAST(N'2024-04-10T04:30:56.000' AS DateTime), CAST(N'2024-04-25T14:12:48.000' AS DateTime), CAST(N'2024-10-25T07:55:31.000' AS DateTime), 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [registerDate], [joinDate], [expiryDate], [clubId]) VALUES (N'9', N'William Miller', N'william_m', N'william.m@email.com', N'Male', N'Staff', N'49/9, Nhieu Tu Street, Phu Nhuan District, Ho Chi Minh City', N'555-6543', NULL, N'Active', CAST(N'2024-01-16T13:03:14.000' AS DateTime), CAST(N'2024-02-05T22:43:12.000' AS DateTime), CAST(N'2024-08-05T08:24:37.000' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [userId]) VALUES (1, N'New Bird Species Discovered', N'Announcement', N'Exciting news about a new bird species found in our region.', CAST(N'2024-07-15T00:00:00.000' AS DateTime), N'Active', N'https://wanee.asia/wp-content/uploads/2023/02/Frame-chim-17.jpg', 1)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [userId]) VALUES (2, N'Upcoming Bird Watching Event', N'Fieldtrip', N'Join us for a bird watching event on the upcoming weekend.', CAST(N'2024-06-15T00:00:00.000' AS DateTime), N'Active', N'https://cdn.tuoitre.vn/zoom/600_315/471584752817336320/2024/5/8/birdswatch-read-only-1715133341905617095998-688-0-1306-1181-crop-17151333662681232243142.jpg', 2)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [userId]) VALUES (3, N'Conservation Success Story', N'Others', N'Our efforts in bird conservation have shown positive results.', CAST(N'2024-03-25T00:00:00.000' AS DateTime), N'Draft', N'image3.jpg', 3)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [userId]) VALUES (4, N'Bird Photography Contest Winners', N'Contest', N'Announcing the winners of our recent bird photography contest.', CAST(N'2024-07-05T00:00:00.000' AS DateTime), N'Active', N'https://images.hive.blog/0x0/https://files.peakd.com/file/peakd-hive/uttambarman/wjWUr7Tn-DSC_0292.jpg', 4)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [userId]) VALUES (5, N'Important Bird Migration Update', N'Announcement', N'Stay informed about the latest bird migration patterns in our region.', CAST(N'2024-08-20T00:00:00.000' AS DateTime), N'Active', N'https://www.birdskoreablog.org/wp-content/uploads/2012/10/Bulbuls3.jpg', 5)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [userId]) VALUES (6, N'Bird Club Achievements', N'Announcement', N'Celebrating the achievements and milestones of our bird club members.', CAST(N'2024-07-02T00:00:00.000' AS DateTime), N'Active', N'https://cdn-img.thethao247.vn/upload/quy/2017/09/10/IMG_3080.JPG', 7)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [userId]) VALUES (7, N'Educational Workshop on Bird Identification', N'Meeting', N'Learn the basics of bird identification in our upcoming workshop.', CAST(N'2024-08-15T00:00:00.000' AS DateTime), N'Draft', N'image8.jpg', 8)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [userId]) VALUES (8, N'Bird Club Newsletter - Summer Edition', N'Meeting', N'Stay updated with the latest news and events in our summer newsletter.', CAST(N'2024-09-10T00:00:00.000' AS DateTime), N'Active', N'https://live.staticflickr.com/4916/31382927917_f9a25f4342_b.jpg', 9)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [userId]) VALUES (9, N'Bird Watching Tips for Beginners', N'Others', N'New to bird watching? Check out our tips for beginners to get started.', CAST(N'2024-10-01T00:00:00.000' AS DateTime), N'Active', N'https://www.wellandgood.com/wp-content/uploads/2023/05/WG_Editorial_Birdwatching_feature.jpg', 10)
SET IDENTITY_INSERT [dbo].[News] OFF
GO
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'1', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-01-05T00:00:00.000' AS DateTime), 12, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'11', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-01-18T00:00:00.000' AS DateTime), 13, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'12', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-01-22T00:00:00.000' AS DateTime), 14, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'14', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-01-24T00:00:00.000' AS DateTime), 15, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'15', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-01-27T00:00:00.000' AS DateTime), 16, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'16', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-01-28T00:00:00.000' AS DateTime), 17, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'17', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-01-30T00:00:00.000' AS DateTime), 18, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'18', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-02-06T00:00:00.000' AS DateTime), 19, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'19', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-02-10T00:00:00.000' AS DateTime), 20, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'2', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-01-07T00:00:00.000' AS DateTime), 21, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'20', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-02-15T00:00:00.000' AS DateTime), 22, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'23', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-03-02T00:00:00.000' AS DateTime), 23, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'24', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-03-02T00:00:00.000' AS DateTime), 24, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'25', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-03-03T00:00:00.000' AS DateTime), 25, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'26', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-03-03T00:00:00.000' AS DateTime), 26, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'27', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-03-03T00:00:00.000' AS DateTime), 27, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'30', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-03-04T00:00:00.000' AS DateTime), 28, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'32', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-03-04T00:00:00.000' AS DateTime), 29, N'Read')
INSERT [dbo].[Notification] ([notificationId], [title], [description], [date], [userId], [status]) VALUES (N'33', N'Account Registration', N'You have successfully joined ChaoMao Bird Club!', CAST(N'2024-03-05T00:00:00.000' AS DateTime), 30, N'Read')
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 

INSERT [dbo].[Transactions] ([transactionId], [vnPayId], [userId], [transactionType], [value], [transactionDate], [paymentDate], [status], [docNo]) VALUES (1, N'1', 1, N'Membership-Renewal-Transaction-Type', 500000, CAST(N'2024-01-01T00:00:00.000' AS DateTime), CAST(N'2024-01-02T00:00:00.000' AS DateTime), N'Completed', N'DOC001')
INSERT [dbo].[Transactions] ([transactionId], [vnPayId], [userId], [transactionType], [value], [transactionDate], [paymentDate], [status], [docNo]) VALUES (2, N'2', 2, N'Event Registration', 200000, CAST(N'2024-02-15T00:00:00.000' AS DateTime), CAST(N'2024-02-16T00:00:00.000' AS DateTime), N'Completed', N'DOC002')
INSERT [dbo].[Transactions] ([transactionId], [vnPayId], [userId], [transactionType], [value], [transactionDate], [paymentDate], [status], [docNo]) VALUES (3, N'3', 3, N'Donation', 300000, CAST(N'2024-03-10T00:00:00.000' AS DateTime), CAST(N'2024-03-11T00:00:00.000' AS DateTime), N'Pending', N'DOC003')
INSERT [dbo].[Transactions] ([transactionId], [vnPayId], [userId], [transactionType], [value], [transactionDate], [paymentDate], [status], [docNo]) VALUES (4, N'4', 4, N'Membership-Renewal-Transaction-Type', 500000, CAST(N'2024-04-05T00:00:00.000' AS DateTime), CAST(N'2024-04-06T00:00:00.000' AS DateTime), N'Completed', N'DOC004')
INSERT [dbo].[Transactions] ([transactionId], [vnPayId], [userId], [transactionType], [value], [transactionDate], [paymentDate], [status], [docNo]) VALUES (5, N'5', 5, N'Event Registration', 250000, CAST(N'2024-05-20T00:00:00.000' AS DateTime), CAST(N'2024-05-21T00:00:00.000' AS DateTime), N'Completed', N'DOC005')
INSERT [dbo].[Transactions] ([transactionId], [vnPayId], [userId], [transactionType], [value], [transactionDate], [paymentDate], [status], [docNo]) VALUES (6, N'6', 6, N'Donation', 400000, CAST(N'2024-06-15T00:00:00.000' AS DateTime), CAST(N'2024-06-16T00:00:00.000' AS DateTime), N'Completed', N'DOC006')
INSERT [dbo].[Transactions] ([transactionId], [vnPayId], [userId], [transactionType], [value], [transactionDate], [paymentDate], [status], [docNo]) VALUES (7, N'7', 7, N'Membership-Renewal-Transaction-Type', 500000, CAST(N'2024-07-01T00:00:00.000' AS DateTime), CAST(N'2024-07-02T00:00:00.000' AS DateTime), N'Pending', N'DOC007')
INSERT [dbo].[Transactions] ([transactionId], [vnPayId], [userId], [transactionType], [value], [transactionDate], [paymentDate], [status], [docNo]) VALUES (8, N'8', 8, N'Event Registration', 300000, CAST(N'2024-08-10T00:00:00.000' AS DateTime), CAST(N'2024-08-11T00:00:00.000' AS DateTime), N'Completed', N'DOC008')
INSERT [dbo].[Transactions] ([transactionId], [vnPayId], [userId], [transactionType], [value], [transactionDate], [paymentDate], [status], [docNo]) VALUES (9, N'9', 9, N'Donation', 350000, CAST(N'2024-09-05T00:00:00.000' AS DateTime), CAST(N'2024-09-06T00:00:00.000' AS DateTime), N'Pending', N'DOC009')
INSERT [dbo].[Transactions] ([transactionId], [vnPayId], [userId], [transactionType], [value], [transactionDate], [paymentDate], [status], [docNo]) VALUES (10, N'10', 10, N'Membership-Renewal-Transaction-Type', 500000, CAST(N'2024-10-20T00:00:00.000' AS DateTime), CAST(N'2024-10-21T00:00:00.000' AS DateTime), N'Completed', N'DOC010')
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (1, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/63c8c35f-8248-4334-82e3-b39207e538e2-avatar-hoa.png', N'1', N'john_doe', N'password123', N'Manager')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (2, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'2', N'jane_smith', N'securepass', N'Admin')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (3, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'3', N'robert_j', N'pass123', N'Manager')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (4, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'4', N'emily_white', N'secretword', N'Manager')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (5, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'5', N'michael_g', N'myp@ssw0rd', N'Manager')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (6, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'6', N'sarah_b', N'strongpassword', N'Staff')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (7, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'7', N'daniel_b', N'mypass123', N'Staff')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (8, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'8', N'olivia_t', N'letmein', N'Manager')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (9, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'9', N'william_m', N'password456', N'Staff')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (10, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'10', N'sophia_d', N'secure123', N'Manager')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (11, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/4f2cdaee-a4f4-48be-ab68-f0ce9902365f-avatar-hoa.png', N'11', N'MinhHoa', N'Test123', N'Admin')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (12, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'12', N'MinhHuan', N'huan123', N'Member')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (13, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'13', N'ThanhTuan', N'tuanld', N'Member')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (14, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'14', N'DucHoang', N'hoangchaomao', N'Member')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (15, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'15', N'XuanHan', N'hanhan', N'Member')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (16, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'16', N'TriHuu', N'trihuuvotri', N'Member')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (17, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'17', N'HuuLe', N'lenguyen', N'Member')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (18, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'18', N'NgocCuong', N'vancuong68', N'Member')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (19, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'19', N'HieuMinh', N'minhnguyen87', N'Member')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (20, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'20', N'BirdClubSystemManager', N'thebirdclubbulbulisrealOMG365', N'Manager')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (21, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'21', N'MinhChau', N'chauminh90', N'Staff')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (22, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'22', N'AnhTuan', N'tuananh99', N'Manager')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (23, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'23', N'ThanhPhat', N'phatthanh10', N'Staff')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (24, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'24', N'DucAnh', N'anhduc20', N'Staff')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (25, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'25', N'LanHuong', N'huonglan30', N'Manager')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (26, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'26', N'GiaBao', N'baogia40', N'Member')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (27, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'27', N'ThuyLinh', N'linhthuy50', N'Member')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (28, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'28', N'TuanKiet', N'kiettuan60', N'Staff')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (29, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'19', N'QuangHuy', N'huyquang78', N'Staff')
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (30, 1, N'https://edwinbirdclubstorage.blob.core.windows.net/images/avatar/avatar2.png', N'20', N'BichNgoc', N'ngocbich87', N'Manager')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Bird]  WITH CHECK ADD  CONSTRAINT [FK_Bird_Member] FOREIGN KEY([memberId])
REFERENCES [dbo].[Member] ([memberId])
GO
ALTER TABLE [dbo].[Bird] CHECK CONSTRAINT [FK_Bird_Member]
GO
ALTER TABLE [dbo].[BirdMedia]  WITH CHECK ADD  CONSTRAINT [FK_BirdMedia_Bird] FOREIGN KEY([birdId])
REFERENCES [dbo].[Bird] ([birdId])
GO
ALTER TABLE [dbo].[BirdMedia] CHECK CONSTRAINT [FK_BirdMedia_Bird]
GO
ALTER TABLE [dbo].[Blog]  WITH CHECK ADD  CONSTRAINT [FK_Blog_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Blog] CHECK CONSTRAINT [FK_Blog_Users]
GO
ALTER TABLE [dbo].[ClubInformation]  WITH CHECK ADD  CONSTRAINT [FK_ClubInformation_ClubLocation] FOREIGN KEY([clubLocationId])
REFERENCES [dbo].[ClubLocation] ([clubLocationId])
GO
ALTER TABLE [dbo].[ClubInformation] CHECK CONSTRAINT [FK_ClubInformation_ClubLocation]
GO
ALTER TABLE [dbo].[ClubLocation]  WITH CHECK ADD  CONSTRAINT [FK_ClubLocation_Location] FOREIGN KEY([locationId])
REFERENCES [dbo].[Location] ([locationId])
GO
ALTER TABLE [dbo].[ClubLocation] CHECK CONSTRAINT [FK_ClubLocation_Location]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Blog] FOREIGN KEY([blogId])
REFERENCES [dbo].[Blog] ([blogId])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Blog]
GO
ALTER TABLE [dbo].[ContestMedia]  WITH CHECK ADD  CONSTRAINT [FK_Contest] FOREIGN KEY([contestId])
REFERENCES [dbo].[Contest] ([contestId])
GO
ALTER TABLE [dbo].[ContestMedia] CHECK CONSTRAINT [FK_Contest]
GO
ALTER TABLE [dbo].[ContestParticipants]  WITH CHECK ADD  CONSTRAINT [FK__TournamentP__BID__0E6E26BF] FOREIGN KEY([birdId])
REFERENCES [dbo].[Bird] ([birdId])
GO
ALTER TABLE [dbo].[ContestParticipants] CHECK CONSTRAINT [FK__TournamentP__BID__0E6E26BF]
GO
ALTER TABLE [dbo].[ContestParticipants]  WITH CHECK ADD  CONSTRAINT [FK__TournamentP__MID] FOREIGN KEY([memberId])
REFERENCES [dbo].[Member] ([memberId])
GO
ALTER TABLE [dbo].[ContestParticipants] CHECK CONSTRAINT [FK__TournamentP__MID]
GO
ALTER TABLE [dbo].[ContestParticipants]  WITH CHECK ADD  CONSTRAINT [FK__TournamentP__TID__0D7A0286] FOREIGN KEY([contestId])
REFERENCES [dbo].[Contest] ([contestId])
GO
ALTER TABLE [dbo].[ContestParticipants] CHECK CONSTRAINT [FK__TournamentP__TID__0D7A0286]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Users]
GO
ALTER TABLE [dbo].[FieldTripAdditionalDetails]  WITH CHECK ADD  CONSTRAINT [FK_FieldTripAdditionalDetails_Trip] FOREIGN KEY([tripId])
REFERENCES [dbo].[FieldTrip] ([tripId])
GO
ALTER TABLE [dbo].[FieldTripAdditionalDetails] CHECK CONSTRAINT [FK_FieldTripAdditionalDetails_Trip]
GO
ALTER TABLE [dbo].[FieldTripDaybyDay]  WITH CHECK ADD  CONSTRAINT [FK_FieldTripDaybyDay_FieldTrip] FOREIGN KEY([tripId])
REFERENCES [dbo].[FieldTrip] ([tripId])
GO
ALTER TABLE [dbo].[FieldTripDaybyDay] CHECK CONSTRAINT [FK_FieldTripDaybyDay_FieldTrip]
GO
ALTER TABLE [dbo].[FieldTripGettingThere]  WITH CHECK ADD  CONSTRAINT [FK_FieldTripGettingThere_FieldTrip] FOREIGN KEY([tripId])
REFERENCES [dbo].[FieldTrip] ([tripId])
GO
ALTER TABLE [dbo].[FieldTripGettingThere] CHECK CONSTRAINT [FK_FieldTripGettingThere_FieldTrip]
GO
ALTER TABLE [dbo].[FieldTripInclusions]  WITH CHECK ADD  CONSTRAINT [FK_FieldTripInclusions_FieldTrip] FOREIGN KEY([tripId])
REFERENCES [dbo].[FieldTrip] ([tripId])
GO
ALTER TABLE [dbo].[FieldTripInclusions] CHECK CONSTRAINT [FK_FieldTripInclusions_FieldTrip]
GO
ALTER TABLE [dbo].[FieldTripMedia]  WITH CHECK ADD  CONSTRAINT [FK_FieldTripMedia_FieldTrip] FOREIGN KEY([tripId])
REFERENCES [dbo].[FieldTrip] ([tripId])
GO
ALTER TABLE [dbo].[FieldTripMedia] CHECK CONSTRAINT [FK_FieldTripMedia_FieldTrip]
GO
ALTER TABLE [dbo].[FieldTripParticipants]  WITH CHECK ADD  CONSTRAINT [FK_FieldTripParticipants_FieldTrip] FOREIGN KEY([tripId])
REFERENCES [dbo].[FieldTrip] ([tripId])
GO
ALTER TABLE [dbo].[FieldTripParticipants] CHECK CONSTRAINT [FK_FieldTripParticipants_FieldTrip]
GO
ALTER TABLE [dbo].[FieldTripParticipants]  WITH CHECK ADD  CONSTRAINT [FK_FieldTripParticipants_Member] FOREIGN KEY([memberId])
REFERENCES [dbo].[Member] ([memberId])
GO
ALTER TABLE [dbo].[FieldTripParticipants] CHECK CONSTRAINT [FK_FieldTripParticipants_Member]
GO
ALTER TABLE [dbo].[Gallery]  WITH CHECK ADD  CONSTRAINT [FK_Gallery_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Gallery] CHECK CONSTRAINT [FK_Gallery_Users]
GO
ALTER TABLE [dbo].[MeetingMedia]  WITH CHECK ADD  CONSTRAINT [FK_Meeting] FOREIGN KEY([meetingId])
REFERENCES [dbo].[Meeting] ([meetingId])
GO
ALTER TABLE [dbo].[MeetingMedia] CHECK CONSTRAINT [FK_Meeting]
GO
ALTER TABLE [dbo].[MeetingParticipant]  WITH CHECK ADD  CONSTRAINT [FK__MeetingPar__MeID__03F0984C] FOREIGN KEY([meetingId])
REFERENCES [dbo].[Meeting] ([meetingId])
GO
ALTER TABLE [dbo].[MeetingParticipant] CHECK CONSTRAINT [FK__MeetingPar__MeID__03F0984C]
GO
ALTER TABLE [dbo].[MeetingParticipant]  WITH CHECK ADD  CONSTRAINT [FK_MeetingParticipant_Member] FOREIGN KEY([memberId])
REFERENCES [dbo].[Member] ([memberId])
GO
ALTER TABLE [dbo].[MeetingParticipant] CHECK CONSTRAINT [FK_MeetingParticipant_Member]
GO
ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK_News_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK_News_Users]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_Users]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Member] FOREIGN KEY([memberId])
REFERENCES [dbo].[Member] ([memberId])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Member]
GO
USE [master]
GO
ALTER DATABASE [BirdClub] SET  READ_WRITE 
GO
