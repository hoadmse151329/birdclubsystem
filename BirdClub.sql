/****** Object:  Database [BirdClub]    Script Date: 4/13/2024 9:43:39 PM ******/
USE [master]
GO
DROP DATABASE IF EXISTS [BirdClub]
GO
/****** Object:  Database [BirdClub]    Script Date: 4/13/2024 9:43:39 PM ******/
CREATE DATABASE [BirdClub]
GO
USE [BirdClub]
GO
/****** Object:  Table [dbo].[Bird]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bird](
	[birdId] [int] IDENTITY(1,1) NOT NULL,
	[memberId] [nvarchar](50) NOT NULL,
	[birdName] [nvarchar](max) NOT NULL,
	[ELO] [int] NOT NULL,
	[age] [int] NULL,
	[description] [nvarchar](max) NULL,
	[color] [nvarchar](max) NULL,
	[addDate] [date] NULL,
	[profilePic] [nvarchar](max) NULL,
	[status] [nvarchar](max) NOT NULL,
	[origin] [nvarchar](max) NULL,
 CONSTRAINT [PK__Bird__C6DE0D21564B20FB] PRIMARY KEY CLUSTERED 
(
	[birdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BirdMedia]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BirdMedia](
	[pictureId] [int] IDENTITY(1,1) NOT NULL,
	[birdId] [int] NULL,
	[description] [nvarchar](max) NULL,
	[image] [nvarchar](max) NULL,
 CONSTRAINT [PK__BirdMedi__8C2866D8E5255F8B] PRIMARY KEY CLUSTERED 
(
	[pictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blog]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blog](
	[blogId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[category] [nvarchar](255) NULL,
	[uploadDate] [datetime] NOT NULL,
	[vote] [int] NOT NULL,
	[image] [varchar](max) NULL,
	[status] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK__Blog__3B7E5742B9C74200] PRIMARY KEY CLUSTERED 
(
	[blogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClubInformation]    Script Date: 4/13/2024 9:43:40 PM ******/
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
/****** Object:  Table [dbo].[ClubLocation]    Script Date: 4/13/2024 9:43:40 PM ******/
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
/****** Object:  Table [dbo].[Comment]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[commentId] [int] IDENTITY(1,1) NOT NULL,
	[blogId] [int] NULL,
	[vote] [int] NULL,
	[description] [nvarchar](max) NULL,
	[date] [date] NULL,
	[userId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[commentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contest]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contest](
	[contestId] [int] IDENTITY(1,1) NOT NULL,
	[contestName] [nvarchar](255) NOT NULL,
	[description] [nvarchar](max) NULL,
	[registrationDeadline] [datetime] NULL,
	[locationId] [int] NULL,
	[status] [nvarchar](20) NULL,
	[startDate] [date] NULL,
	[endDate] [date] NULL,
	[beforeScore] [int] NULL,
	[afterScore] [int] NULL,
	[fee] [decimal](10, 2) NULL,
	[prize] [decimal](12, 2) NULL,
	[host] [nvarchar](100) NULL,
	[incharge] [nvarchar](100) NULL,
	[note] [nvarchar](max) NULL,
	[review] [nvarchar](max) NULL,
	[numberOfParticipants] [int] NULL,
	[clubId] [int] NULL,
	[numberOfParticipantsLimit] [int] NULL,
 CONSTRAINT [PK__Tourname__C456D729169E357B] PRIMARY KEY CLUSTERED 
(
	[contestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContestMedia]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContestMedia](
	[pictureId] [int] IDENTITY(1,1) NOT NULL,
	[contestId] [int] NULL,
	[description] [nvarchar](max) NULL,
	[image] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[pictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContestParticipants]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContestParticipants](
	[contestId] [int] NULL,
	[birdId] [int] NULL,
	[ELO] [int] NOT NULL,
	[participantNo] [nvarchar](50) NOT NULL,
	[checkInStatus] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContestScore]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContestScore](
	[scoreId] [int] IDENTITY(1,1) NOT NULL,
	[contestId] [int] NULL,
	[birdId] [int] NULL,
	[memberId] [int] NULL,
	[score] [decimal](5, 2) NULL,
	[scoreDate] [date] NULL,
	[comment] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[scoreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Feedback]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedback](
	[feedbackId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[detail] [nvarchar](max) NULL,
	[date] [datetime] NULL,
	[category] [nvarchar](50) NULL,
	[status] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[feedbackId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldTrip]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldTrip](
	[tripId] [int] IDENTITY(1,1) NOT NULL,
	[tripName] [nvarchar](255) NOT NULL,
	[description] [nvarchar](max) NULL,
	[Details] [nvarchar](max) NOT NULL,
	[registrationDeadline] [date] NULL,
	[startDate] [date] NULL,
	[endDate] [date] NULL,
	[locationId] [int] NULL,
	[status] [nvarchar](20) NULL,
	[fee] [decimal](10, 2) NULL,
	[numberOfParticipants] [int] NULL,
	[host] [nvarchar](100) NULL,
	[inCharge] [nvarchar](100) NULL,
	[note] [nvarchar](max) NULL,
	[review] [nvarchar](max) NULL,
	[numberOfParticipantsLimit] [int] NULL,
 CONSTRAINT [PK__FieldTri__C1BEA5A2CBA40722] PRIMARY KEY CLUSTERED 
(
	[tripId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldtripDaybyDay]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldtripDaybyDay](
	[tripId] [int] NOT NULL,
	[daybyDayID] [int] IDENTITY(1,1) NOT NULL,
	[day] [int] NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[pictureId] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldtripGettingThere]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldtripGettingThere](
	[tripId] [int] NOT NULL,
	[gettingThereId] [int] IDENTITY(1,1) NOT NULL,
	[gettingThereText] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldtripInclusions]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldtripInclusions](
	[tripId] [int] NOT NULL,
	[inclusionId] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[inclusionText] [nvarchar](max) NOT NULL,
	[type] [nvarchar](50) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldtripMedia]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldtripMedia](
	[pictureId] [int] IDENTITY(1,1) NOT NULL,
	[tripId] [int] NULL,
	[description] [nvarchar](max) NULL,
	[image] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[pictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldTripOverview]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldTripOverview](
	[tripId] [int] NOT NULL,
	[title] [nvarchar](100) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[duration] [int] NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
	[destination] [nvarchar](255) NOT NULL,
	[registrationDeadline] [date] NOT NULL,
	[startDate] [date] NOT NULL,
	[endDate] [date] NOT NULL,
	[pictureId] [int] NULL,
	[userReview] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldTripParticipants]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldTripParticipants](
	[tripId] [int] NOT NULL,
	[memberId] [nvarchar](50) NOT NULL,
	[participantNo] [nvarchar](50) NOT NULL,
	[checkInStatus] [varchar](50) NOT NULL,
 CONSTRAINT [PK_FieldTripParticipants] PRIMARY KEY CLUSTERED 
(
	[tripId] ASC,
	[memberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FieldtripRates]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldtripRates](
	[tripId] [int] NOT NULL,
	[rateId] [int] IDENTITY(1,1) NOT NULL,
	[rateType] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gallery]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gallery](
	[pictureId] [int] NOT NULL,
	[description] [nvarchar](max) NULL,
	[userId] [int] NOT NULL,
	[image] [varchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Location]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[locationId] [int] IDENTITY(1,1) NOT NULL,
	[locationName] [nvarchar](255) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__Location__C6555721B7AAE234] PRIMARY KEY CLUSTERED 
(
	[locationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meeting]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meeting](
	[meetingId] [int] IDENTITY(1,1) NOT NULL,
	[meetingName] [nvarchar](255) NOT NULL,
	[description] [nvarchar](max) NULL,
	[registrationDeadline] [datetime] NULL,
	[startDate] [datetime] NULL,
	[endDate] [datetime] NULL,
	[numberOfParticipants] [int] NULL,
	[host] [nvarchar](100) NULL,
	[incharge] [nvarchar](100) NULL,
	[locationId] [int] NULL,
	[status] [nvarchar](20) NULL,
	[note] [nvarchar](max) NULL,
	[numberOfParticipantsLimit] [int] NULL,
 CONSTRAINT [PK__Meeting__1234DA4418E1FF0D] PRIMARY KEY CLUSTERED 
(
	[meetingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingMedia]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingMedia](
	[pictureId] [int] IDENTITY(1,1) NOT NULL,
	[meetingId] [int] NULL,
	[description] [nvarchar](max) NULL,
	[image] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[pictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingParticipant]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingParticipant](
	[meetingId] [int] NOT NULL,
	[memberId] [nvarchar](50) NOT NULL,
	[participantNo] [nvarchar](50) NOT NULL,
	[checkInStatus] [varchar](50) NOT NULL,
 CONSTRAINT [PK_MeetingParticipant] PRIMARY KEY CLUSTERED 
(
	[meetingId] ASC,
	[memberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Member]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[memberId] [nvarchar](50) NOT NULL,
	[fullName] [nvarchar](255) NULL,
	[userName] [nvarchar](50) NULL,
	[email] [nvarchar](255) NULL,
	[gender] [nvarchar](10) NULL,
	[role] [nvarchar](50) NULL,
	[address] [nvarchar](max) NULL,
	[phone] [nvarchar](20) NULL,
	[description] [nvarchar](max) NULL,
	[status] [nvarchar](20) NULL,
	[clubId] [int] NULL,
 CONSTRAINT [PK__Member__0CF04B18EBA8FD9D] PRIMARY KEY CLUSTERED 
(
	[memberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[News]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[newsId] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](255) NOT NULL,
	[category] [nvarchar](50) NOT NULL,
	[newsContent] [nvarchar](max) NOT NULL,
	[uploadDate] [datetime] NOT NULL,
	[status] [nvarchar](20) NOT NULL,
	[picture] [varchar](max) NULL,
	[filepdf] [varchar](max) NULL,
	[userId] [int] NOT NULL,
 CONSTRAINT [PK__News__5218041EBB60A1A2] PRIMARY KEY CLUSTERED 
(
	[newsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[transactionId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NULL,
	[transactionType] [nvarchar](255) NULL,
	[value] [decimal](10, 2) NULL,
	[paymentDate] [date] NULL,
	[transactionDate] [date] NULL,
	[status] [nvarchar](255) NULL,
	[docNo] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[transactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 4/13/2024 9:43:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[userId] [int] NOT NULL,
	[clubId] [int] NULL,
	[imagePath] [nvarchar](255) NULL,
	[memberId] [nvarchar](50) NULL,
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
GO
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (1, N'1', N'Mèo', 1600, 2, N'A colorful and melodious bird with a distinctive red patch on its cheeks.', N'Red, Black, and White', CAST(N'2023-09-01' AS Date), N'red_whiskered_bulbul.jpg', N'Active', N'Northern India')
GO
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (2, N'2', N'Chíp', 1500, 1, N'A young Red-whiskered Bulbul, still acquiring its adult plumage.', N'Brown and White', CAST(N'2023-09-02' AS Date), N'baby_red_whiskered_bulbul.jpg', N'Active', N'Nepal')
GO
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (3, N'3', N'Lúa', 1750, 4, N'A mature Red-whiskered Bulbul singing melodiously in a garden.', N'Red, Black, and White', CAST(N'2023-09-03' AS Date), N'singing_bulbul.jpg', N'Active', N'Nepal')
GO
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (4, N'4', N'Gạo', 1550, 3, N'A Red-whiskered Bulbul perched on a branch with a backdrop of lush green foliage.', N'Red, Black, and White', CAST(N'2023-09-04' AS Date), N'bulbul_in_foliage.jpg', N'Active', N'South-Eastern China')
GO
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (5, N'5', N'Sóc', 1650, 2, N'A Red-whiskered Bulbul pair in a courtship display.', N'Red, Black, and White', CAST(N'2023-09-05' AS Date), N'bulbul_courtship.jpg', N'Active', N'South-Western Thailand')
GO
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (6, N'6', N'Bác', 1700, 3, N'A Red-whiskered Bulbul feeding on fruits in a tree.', N'Red, Black, and White', CAST(N'2023-09-06' AS Date), N'bulbul_feeding.jpg', N'Active', N'Northern India')
GO
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (7, N'7', N'Mơ', 1600, 2, N'A Red-whiskered Bulbul bathing in a bird bath.', N'Red, Black, and White', CAST(N'2023-09-07' AS Date), N'bulbul_bathing.jpg', N'Active', N'Northern Myanmar')
GO
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (8, N'8', N'Đậu', 1550, 1, N'A juvenile Red-whiskered Bulbul exploring its surroundings.', N'Brown and White', CAST(N'2023-09-08' AS Date), N'young_bulbul.jpg', N'Active', N'South-Eastern China')
GO
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (9, N'9', N'Bảy', 1800, 5, N'A Red-whiskered Bulbul building a nest in a tree.', N'Red, Black, and White', CAST(N'2023-09-09' AS Date), N'bulbul_nest.jpg', N'Active', N'South-Western Thailand')
GO
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (10, N'10', N'Táo', 1650, 3, N'A Red-whiskered Bulbul perched on a wire with a backdrop of clear blue sky.', N'Red, Black, and White', CAST(N'2023-09-10' AS Date), N'bulbul_on_wire.jpg', N'Active', N'South-Eastern China')
GO
SET IDENTITY_INSERT [dbo].[Bird] OFF
GO
SET IDENTITY_INSERT [dbo].[BirdMedia] ON 
GO
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (1, 1, N'Beautiful Red Bulbul in flight', N'/images/red_bulbul_flight.jpg')
GO
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (2, 1, N'Close-up of Red Bulbul feeding', N'/images/red_bulbul_feeding.jpg')
GO
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (3, 2, N'Red Bulbul pair on a branch', N'/images/red_bulbul_pair.jpg')
GO
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (4, 2, N'Red Bulbul bathing in a pond', N'/images/red_bulbul_bathing.jpg')
GO
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (5, 3, N'Juvenile Red Bulbul exploring', N'/images/red_bulbul_juvenile.jpg')
GO
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (6, 3, N'Red Bulbul with nesting material', N'/images/red_bulbul_nesting.jpg')
GO
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (7, 4, N'Red Bulbul singing on a tree', N'/images/red_bulbul_singing.jpg')
GO
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (8, 4, N'Red Bulbul in its natural habitat', N'/images/red_bulbul_habitat.jpg')
GO
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (9, 5, N'Red Bulbul family in the morning', N'/images/red_bulbul_family.jpg')
GO
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (10, 5, N'Red Bulbul perched on a fence', N'/images/red_bulbul_perched.jpg')
GO
SET IDENTITY_INSERT [dbo].[BirdMedia] OFF
GO
SET IDENTITY_INSERT [dbo].[Blog] ON 
GO
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (1, 1, N'Observations of Red-Whiskered Bulbuls in Central Park', N'Observations', CAST(N'2023-03-01T00:00:00.000' AS DateTime), 15, N'central_park_blog.jpg', N'Active')
GO
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (2, 2, N'Tips for Attracting Bulbuls to Your Garden', N'Gardening', CAST(N'2023-03-05T00:00:00.000' AS DateTime), 20, N'garden_tips_blog.jpg', N'Active')
GO
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (3, 3, N'The Beauty of Bulbul Nests: A Photo Journey', N'Photography', CAST(N'2023-03-10T00:00:00.000' AS DateTime), 25, N'nest_photo_blog.jpg', N'Active')
GO
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (4, 4, N'Red Bulbul Conservation Efforts in Everglades National Park', N'Conservation', CAST(N'2023-03-15T00:00:00.000' AS DateTime), 18, N'conservation_blog.jpg', N'Active')
GO
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (5, 5, N'Spotting Rare Bulbul Varieties in Griffith Park', N'Spotting', CAST(N'2023-03-20T00:00:00.000' AS DateTime), 22, N'rare_bulbuls_blog.jpg', N'Active')
GO
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (6, 6, N'A Day of Birdwatching at Discovery Park', N'Birdwatching', CAST(N'2023-03-25T00:00:00.000' AS DateTime), 17, N'birdwatching_day_blog.jpg', N'Active')
GO
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (7, 7, N'Exploring Stanford University Campus: Birdwatcher’s Paradise', N'Birdwatching', CAST(N'2023-03-30T00:00:00.000' AS DateTime), 23, N'stanford_campus_blog.jpg', N'Active')
GO
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (8, 8, N'Winter Bulbul Migration Patterns at South Mountain Park', N'Migration', CAST(N'2023-04-05T00:00:00.000' AS DateTime), 19, N'migration_patterns_blog.jpg', N'Active')
GO
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (9, 9, N'Piedmont Park: A Haven for Red Bulbuls', N'Habitat', CAST(N'2023-04-10T00:00:00.000' AS DateTime), 21, N'piedmont_park_blog.jpg', N'Active')
GO
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (10, 10, N'Fairmount Park Bird Festival: Highlights and Discoveries', N'Festival', CAST(N'2023-04-15T00:00:00.000' AS DateTime), 28, N'park_festival_blog.jpg', N'Active')
GO
SET IDENTITY_INSERT [dbo].[Blog] OFF
GO
SET IDENTITY_INSERT [dbo].[ClubInformation] ON 
GO
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [description]) VALUES (1, 1, N'Situated in the bustling capital city of Vietnam, Hanoi Chào mào Club benefits from a vibrant urban environment with access to various resources and facilities for bird enthusiasts. Hanoi is known for its rich cultural heritage, historical landmarks, and diverse culinary scene, providing an enriching backdrop for the club''s activities.')
GO
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [description]) VALUES (2, 2, N'Located in Hai Phong, a major port city in northeastern Vietnam, this club enjoys proximity to both urban amenities and natural landscapes. Hai Phong boasts a dynamic economy, beautiful coastal areas, and cultural attractions, offering members a blend of urban conveniences and natural beauty for birdwatching and gatherings.')
GO
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [description]) VALUES (3, 3, N'Positioned in Da Nang, a coastal city in central Vietnam, the club benefits from its strategic location along the picturesque coastline. Da Nang is renowned for its stunning beaches, Marble Mountains, and vibrant city life, providing members with opportunities for birdwatching amidst scenic landscapes and cultural attractions.')
GO
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [description]) VALUES (4, 4, N'Situated in the bustling metropolis of Ho Chi Minh City, this club is at the heart of Vietnam''s economic and cultural hub. Formerly known as Saigon, the city offers a dynamic blend of modern skyscrapers, historic landmarks, and bustling markets. Members can enjoy access to a wide range of amenities, entertainment venues, and urban parks for birdwatching activities.')
GO
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [description]) VALUES (5, 5, N'Located in Can Tho, the largest city in the Mekong Delta region, this club is surrounded by lush rice paddies, winding rivers, and floating markets. Can Tho is known for its vibrant agricultural economy, traditional river life, and ecological diversity, providing members with unique birdwatching opportunities amidst the picturesque landscapes of the Mekong Delta.')
GO
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [description]) VALUES (6, 6, N'Positioned in Nha Trang, a coastal resort city in southern Vietnam, this club enjoys a scenic setting along the turquoise waters of the South China Sea. Nha Trang is famous for its pristine beaches, vibrant coral reefs, and island-hopping tours, offering members a tropical paradise for birdwatching adventures amidst stunning natural scenery.')
GO
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [description]) VALUES (7, 7, N'Located in Dak Lak province in the Central Highlands of Vietnam, this club is surrounded by lush forests, majestic waterfalls, and scenic coffee plantations. Dak Lak is renowned for its rich ethnic culture, diverse wildlife, and serene natural landscapes, providing members with a tranquil setting for birdwatching and conservation efforts.')
GO
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [description]) VALUES (8, 8, N'Situated in Binh Dinh province along the central coast of Vietnam, this club benefits from its proximity to Quy Nhon, a charming seaside town known for its pristine beaches and historical sites. Binh Dinh offers members a serene coastal retreat with opportunities for birdwatching, beachcombing, and cultural exploration amidst scenic coastal landscapes.')
GO
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [description]) VALUES (9, 9, N'Located in Binh Phuoc province in southeastern Vietnam, this club is surrounded by lush forests, scenic waterways, and fertile agricultural land. Binh Phuoc is renowned for its diverse ecosystem, including protected national parks and wildlife reserves, providing members with opportunities for birdwatching and ecological conservation efforts.')
GO
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [description]) VALUES (10, 10, N'Positioned in Bac Giang province in northern Vietnam, this club enjoys a tranquil setting amidst rolling hills, verdant rice fields, and picturesque rural landscapes. Bac Giang is known for its rich cultural heritage, ancient pagodas, and traditional villages, providing members with a peaceful retreat for birdwatching and cultural immersion in the heart of northern Vietnam.')
GO
SET IDENTITY_INSERT [dbo].[ClubInformation] OFF
GO
SET IDENTITY_INSERT [dbo].[ClubLocation] ON 
GO
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (1, N'Câu lạc bộ Chào mào Hà Nội', N'This is one of the famous clubs in the capital city of Hanoi, focusing on breeding Bulbul birds and organizing community activities, competitions among members.
Hai Phong Chào mào Club: Located in the bustling port city of Hai Phong, this club has a vibrant community passionate about raising Chào mào birds and regularly organizes events and exhibitions about Bulbul birds.', 3)
GO
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (2, N'Câu lạc bộ Chào mào Hải Phòng
', N'Located in the bustling port city of Hai Phong, this club has a vibrant community passionate about raising Bulbul birds and regularly organizes events and exhibitions about Bulbul birds.', 4)
GO
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (3, N'Câu lạc bộ Chào mào Đà Nẵng
', N'Da Nang is not only an attractive tourist destination but also has a large community of Bulbul enthusiasts. This club often organizes meetings, shares experiences in bird raising.', 5)
GO
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (4, N'Câu lạc bộ Chào mào Hồ Chí Minh
', N'Being one of the oldest and most developed clubs in Ho Chi Minh City, where many bird enthusiasts gather and host large events and competitions about Bulbul birds.', 6)
GO
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (5, N'Câu lạc bộ Chào mào Cần Thơ
', N'Located in the heart of the South, this club regularly organizes seminars, training courses, and exhibitions about Bulbul birds.', 7)
GO
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (6, N'Câu lạc bộ Chào mào Nha Trang
', N'Located in the coastal city of Nha Trang, this club often hosts exchange programs, discussions, and introductions about Bulbul birds.', 8)
GO
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (7, N'Câu lạc bộ Chào mào Đắk Lắk
', N'In Dak Lak province, where there are many rich forests, this club is a destination for Bulbul enthusiasts from all over the Central region.', 9)
GO
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (8, N'Câu lạc bộ Chào mào Bình Định
', N'Binh Dinh is not only famous for Quy Nhon beach but also has a vibrant Bulbul community, with interesting and lively activities.', 10)
GO
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (9, N'Câu lạc bộ Chào mào Bình Phước
', N'In the southwestern region, this club plays an important role in conserving and developing local Bulbul bird species.', 11)
GO
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (10, N'Câu lạc bộ Chào mào Bắc Giang
', N'Located in the northern region, this club has a passionate community of Bulbul enthusiasts and regularly organizes events, seminars about Bulbul birds.', 12)
GO
SET IDENTITY_INSERT [dbo].[ClubLocation] OFF
GO
SET IDENTITY_INSERT [dbo].[Comment] ON 
GO
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (2, 1, 5, N'Great insights into the behaviors of Red-Whiskered Bulbuls!', CAST(N'2023-03-02' AS Date), 1)
GO
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (3, 1, 4, N'I love observing birds in Central Park. Thanks for sharing!', CAST(N'2023-03-03' AS Date), 2)
GO
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (4, 2, 3, N'Your gardening tips worked wonders in attracting bulbuls to my garden.', CAST(N'2023-03-06' AS Date), 3)
GO
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (5, 2, 5, N'Beautiful photos! Bulbuls are truly fascinating creatures.', CAST(N'2023-03-07' AS Date), 4)
GO
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (6, 3, 4, N'The photo journey of bulbul nests is heartwarming.', CAST(N'2023-03-11' AS Date), 5)
GO
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (7, 3, 5, N'Nature''s wonders captured in these pictures. Amazing!', CAST(N'2023-03-12' AS Date), 6)
GO
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (8, 4, 4, N'Conservation efforts are crucial. Thank you for raising awareness!', CAST(N'2023-03-16' AS Date), 7)
GO
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (9, 4, 3, N'Shy or not, every bird plays a vital role in the ecosystem.', CAST(N'2023-03-17' AS Date), 8)
GO
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (10, 5, 5, N'Rare varieties are always a joy to spot. Thanks for the guide!', CAST(N'2023-03-21' AS Date), 9)
GO
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (11, 5, 4, N'The festival was a blast! Looking forward to more events.', CAST(N'2023-03-22' AS Date), 10)
GO
SET IDENTITY_INSERT [dbo].[Comment] OFF
GO
SET IDENTITY_INSERT [dbo].[Contest] ON 
GO
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsLimit]) VALUES (1, N'Thách Thức Âm Nhạc Tinh Tế
', N'The competition focuses on the delicacy and uniqueness of the melodious chirps of Bulbul birds, encouraging contestants to demonstrate creativity and individual style.', CAST(N'2023-10-15T00:00:00.000' AS DateTime), 13, N'Active', CAST(N'2023-11-01' AS Date), CAST(N'2023-11-15' AS Date), 0, 20, CAST(20.00 AS Decimal(10, 2)), CAST(50.00 AS Decimal(12, 2)), N'TRƯỜNG CHIM  Đức Thái', N'TRƯỜNG CHIM  Đức Thái', N'Ensure that the competition environment does not induce excessive stress or pressure on the contestants and respect the diversity of singing styles.', N'Good', 8, 1, 100)
GO
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsLimit]) VALUES (2, N'Cuộc Thi Sắc Đẹp Lông Vũ
', N'Birds participating in the competition are evaluated based on the beauty of their feathers, overall shape, and coloration.', CAST(N'2023-11-05T00:00:00.000' AS DateTime), 14, N'Active', CAST(N'2023-11-20' AS Date), CAST(N'2023-12-05' AS Date), 0, 20, CAST(15.00 AS Decimal(10, 2)), CAST(100.00 AS Decimal(12, 2)), N'TRƯỜNG CHIM  Huynh Đệ', N'TRƯỜNG CHIM  Huynh Đệ', N'Ensure that the evaluation process does not compromise the health and natural state of the birds, and respect the diversity of plumage types.', N'Good', 8, 2, 100)
GO
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsLimit]) VALUES (3, N'Trận Chiến Lồng Đẹp Mắt
', N'The competition focuses on the agility and interaction of Bulbul birds in the cage environment, assessing their movement skills and natural behaviors.', CAST(N'2023-10-20T00:00:00.000' AS DateTime), 15, N'Active', CAST(N'2023-11-10' AS Date), CAST(N'2023-11-20' AS Date), 0, 20, CAST(10.00 AS Decimal(10, 2)), CAST(75.00 AS Decimal(12, 2)), N'TRƯỜNG CHIM  Libi', N'TRƯỜNG CHIM  Libi', N'Ensure that the cages provide a safe and comfortable environment for the participating birds and do not induce stress or tension.', N'Good', 8, 3, 100)
GO
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsLimit]) VALUES (4, N'Cuộc Đấu Tiếng Hót Miền Bắc và Miền Nam
', N'The competition creates an opportunity for Bulbul birds from different regions to compete in showcasing the distinctive singing styles of the North or South.', CAST(N'2023-11-10T00:00:00.000' AS DateTime), 16, N'Active', CAST(N'2023-11-25' AS Date), CAST(N'2023-12-10' AS Date), 0, 20, CAST(25.00 AS Decimal(10, 2)), CAST(120.00 AS Decimal(12, 2)), N'CLB Chào mào Phù Trì', N'CLB Chào mào Phù Trì', N'Encourage respect and appreciation for the cultural diversity of different regions and avoid bias or favoritism towards any particular region.', N'Good', 8, 4, 100)
GO
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsLimit]) VALUES (5, N'Giải Đấu Sức Hút Vào Cao Trào
', N'The competition challenges contestants to demonstrate their ability to attract and interact with partners or the surrounding environment.', CAST(N'2023-11-15T00:00:00.000' AS DateTime), 14, N'Active', CAST(N'2023-12-01' AS Date), CAST(N'2023-12-15' AS Date), 0, 20, CAST(18.00 AS Decimal(10, 2)), CAST(80.00 AS Decimal(12, 2)), N'Team Thiên Cung', N'Team Thiên Cung', N'Ensure that participating birds are trained and nurtured responsibly and do not experience excessive stress or pressure.', N'Good', 8, 5, 100)
GO
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsLimit]) VALUES (6, N'Thử Thách Phong Cách Hót
', N'The competition focuses on evaluating and encouraging diversity and creativity in the Bulbul birds'' singing styles.', CAST(N'2023-10-25T00:00:00.000' AS DateTime), 17, N'Active', CAST(N'2023-11-15' AS Date), CAST(N'2023-11-30' AS Date), 0, 20, CAST(10.00 AS Decimal(10, 2)), CAST(60.00 AS Decimal(12, 2)), N'Hội Chim Chào Mào Đà Nẵng', N'Hội Chim Chào Mào Đà Nẵng', N'Encourage active participation and respect for the creative and unique aspects of each singing style.', N'Good', 8, 6, 100)
GO
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsLimit]) VALUES (7, N'Giải Đấu Tài Năng Trẻ
', N'The competition aims to encourage and develop the skills of young Bulbul birds through various challenges and qualifiers.', CAST(N'2023-10-20T00:00:00.000' AS DateTime), 13, N'Active', CAST(N'2023-11-05' AS Date), CAST(N'2023-11-15' AS Date), 0, 20, CAST(5.00 AS Decimal(10, 2)), CAST(150.00 AS Decimal(12, 2)), N'Hội Chim Chào Mào Chí Minh', N'Hội Chim Chào Mào Chí Minh', N'Ensure that participating in the competition does not compromise the health and natural development of young birds and encourage education and support from bird owners.', N'Good', 8, 7, 100)
GO
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsLimit]) VALUES (8, N'Hội Nghị Chào Mào Hoành Tráng
', N'The competition serves as a festive event, providing an opportunity for the Bulbul bird community to meet, exchange experiences, and enjoy entertainment activities.', CAST(N'2023-10-30T00:00:00.000' AS DateTime), 14, N'Active', CAST(N'2023-11-15' AS Date), CAST(N'2023-11-25' AS Date), 0, 20, CAST(12.00 AS Decimal(10, 2)), CAST(90.00 AS Decimal(12, 2)), N'CLB Chào Mào Văn Giang', N'CLB Chào Mào Văn Giang', N'Create a joyful and friendly atmosphere and encourage active participation from everyone, including those not directly involved in the competition.', N'Good', 8, 8, 100)
GO
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsLimit]) VALUES (9, N'Trận Chiến Độc Đáo: Chim Sao Chép Thần Thánh
', N'The competition focuses on evaluating and encouraging the mimicry and imitation skills of Bulbul birds, showcasing intelligence and flexibility.', CAST(N'2023-11-10T00:00:00.000' AS DateTime), 13, N'Active', CAST(N'2023-11-25' AS Date), CAST(N'2023-12-10' AS Date), 0, 20, CAST(20.00 AS Decimal(10, 2)), CAST(120.00 AS Decimal(12, 2)), N'CLB Chào Mào Phú Bình', N'CLB Chào Mào Phú Bình', N'Ensure that participating birds are trained and nurtured responsibly, and do not experience stress or excessive pressure.', N'Good', 8, 9, 100)
GO
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId], [numberOfParticipantsLimit]) VALUES (10, N'Bản Hòa Âm Đỉnh Cao
', N'The competition provides an opportunity for contestants to demonstrate talent and creativity by performing unique and refined singing styles.', CAST(N'2023-10-25T00:00:00.000' AS DateTime), 15, N'Active', CAST(N'2023-11-10' AS Date), CAST(N'2023-11-20' AS Date), 0, 20, CAST(8.00 AS Decimal(10, 2)), CAST(40.00 AS Decimal(12, 2)), N'Trường 72I', N'Trường 72I', N'Encourage active participation and diversity in the singing styles performed, and respect the balance between competition and social interaction.', N'Good', 8, 1, 100)
GO
SET IDENTITY_INSERT [dbo].[Contest] OFF
GO
SET IDENTITY_INSERT [dbo].[ContestMedia] ON 
GO
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (1, 1, N'Captivating moments from the Red Bulbul Contest', N'/contest_images/contest_image_1.jpg')
GO
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (2, 1, N'The winning bird in action', N'/contest_images/contest_image_2.jpg')
GO
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (3, 2, N'Contest participants in their natural habitat', N'/contest_images/contest_image_3.jpg')
GO
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (4, 2, N'Admiring the beauty of Red-Whiskered Bulbuls', N'/contest_images/contest_image_4.jpg')
GO
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (5, 3, N'Close-up of the winning bird', N'/contest_images/contest_image_5.jpg')
GO
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (6, 3, N'Judges evaluating the contestants', N'/contest_images/contest_image_6.jpg')
GO
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (7, 4, N'Diverse bird species showcased in the contest', N'/contest_images/contest_image_7.jpg')
GO
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (8, 4, N'Bird enthusiasts enjoying the contest', N'/contest_images/contest_image_8.jpg')
GO
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (9, 5, N'Birds in flight during the contest', N'/contest_images/contest_image_9.jpg')
GO
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (10, 5, N'Celebrating the successful conclusion of the contest', N'/contest_images/contest_image_10.jpg')
GO
SET IDENTITY_INSERT [dbo].[ContestMedia] OFF
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (1, 1, 1600, N'1', N'Checked-In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (1, 2, 1500, N'2', N'Checked-In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (1, 3, 1750, N'3', N'Checked-In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (2, 4, 1550, N'1', N'Checked-In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (2, 5, 1650, N'2', N'Checked-In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (3, 6, 1700, N'1', N'Checked-In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (3, 7, 1600, N'2', N'Checked-In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (4, 8, 1550, N'1', N'Checked-In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (4, 9, 1800, N'2', N'Checked-In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (5, 10, 1650, N'1', N'Checked-In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (1, 1, 1200, N'1', N'Checked In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (1, 2, 1350, N'2', N'Checked In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (1, 3, 1100, N'3', N'Not Checked In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (2, 4, 1250, N'1', N'Checked In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (2, 5, 1300, N'2', N'Not Checked In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (3, 6, 1400, N'1', N'Checked In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (3, 7, 1180, N'2', N'Checked In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (4, 8, 1320, N'1', N'Checked In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (4, 9, 1150, N'2', N'Not Checked In')
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (5, 10, 1280, N'1', N'Checked In')
GO
SET IDENTITY_INSERT [dbo].[ContestScore] ON 
GO
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (1, 1, 1, 201, CAST(85.00 AS Decimal(5, 2)), CAST(N'2023-03-01' AS Date), N'Impressive performance by the Robin.')
GO
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (2, 1, 2, 202, CAST(92.00 AS Decimal(5, 2)), CAST(N'2023-03-01' AS Date), N'Excellent display by the Blue Jay.')
GO
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (3, 2, 3, 203, CAST(78.00 AS Decimal(5, 2)), CAST(N'2023-03-05' AS Date), N'The bird showed some unique behaviors.')
GO
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (4, 2, 4, 204, CAST(89.00 AS Decimal(5, 2)), CAST(N'2023-03-05' AS Date), N'Great participation and vocalizations.')
GO
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (5, 3, 5, 205, CAST(94.00 AS Decimal(5, 2)), CAST(N'2023-03-10' AS Date), N'Outstanding plumage and singing.')
GO
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (6, 3, 6, 206, CAST(82.00 AS Decimal(5, 2)), CAST(N'2023-03-10' AS Date), N'Good overall performance in the contest.')
GO
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (7, 4, 7, 207, CAST(90.00 AS Decimal(5, 2)), CAST(N'2023-03-15' AS Date), N'Impressive agility and flight skills.')
GO
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (8, 4, 8, 208, CAST(75.00 AS Decimal(5, 2)), CAST(N'2023-03-15' AS Date), N'The bird was a bit shy during the contest.')
GO
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (9, 5, 9, 209, CAST(88.00 AS Decimal(5, 2)), CAST(N'2023-03-20' AS Date), N'Good participation with beautiful plumage.')
GO
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (10, 5, 10, 210, CAST(91.00 AS Decimal(5, 2)), CAST(N'2023-03-20' AS Date), N'Exceptional behavior and interaction with others.')
GO
SET IDENTITY_INSERT [dbo].[ContestScore] OFF
GO
SET IDENTITY_INSERT [dbo].[Feedback] ON 
GO
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (1, 1, N'Fantastic Bulbul Singing Contest', N'The singing contest was amazing. The Red-whiskered Bulbuls'' performances were outstanding.', CAST(N'2023-11-05T00:00:00.000' AS DateTime), N'Contest', N'Positive')
GO
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (2, 2, N'Great Meeting with Bird Enthusiasts', N'Had a wonderful time at the meeting. Met some passionate bird lovers.', CAST(N'2023-10-15T00:00:00.000' AS DateTime), N'Meeting', N'Positive')
GO
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (3, 3, N'Bulbul Dance-Off Feedback', N'The dance competition was so much fun. The Bulbuls really know how to dance!', CAST(N'2023-11-20T00:00:00.000' AS DateTime), N'Contest', N'Positive')
GO
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (4, 4, N'Informative Birdwatching Meeting', N'The meeting provided valuable insights into birdwatching. Learned a lot!', CAST(N'2023-10-30T00:00:00.000' AS DateTime), N'Meeting', N'Positive')
GO
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (5, 5, N'Nesting Championship', N'The nest-building competition was a bit challenging, but so rewarding!', CAST(N'2023-11-10T00:00:00.000' AS DateTime), N'Contest', N'Positive')
GO
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (6, 6, N'Meeting Feedback', N'The meeting was informative, but it could be improved with more interactive sessions.', CAST(N'2023-10-25T00:00:00.000' AS DateTime), N'Meeting', N'Mixed')
GO
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (7, 7, N'Bulbul Art Contest', N'Enjoyed the art contest. So many creative Bulbul-themed artworks!', CAST(N'2023-11-15T00:00:00.000' AS DateTime), N'Contest', N'Positive')
GO
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (8, 8, N'Singing Contest Disappointment', N'The singing contest didn''t meet my expectations. Some performances were off-key.', CAST(N'2023-11-05T00:00:00.000' AS DateTime), N'Contest', N'Negative')
GO
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (9, 9, N'Birdwatching Meeting Suggestion', N'The meeting was good, but it would be better if it had more practical birdwatching tips.', CAST(N'2023-10-15T00:00:00.000' AS DateTime), N'Meeting', N'Mixed')
GO
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (10, 10, N'Plumage Parade Contest', N'The colorful plumage contest was a visual treat. The Bulbuls looked stunning!', CAST(N'2023-11-30T00:00:00.000' AS DateTime), N'Contest', N'Positive')
GO
SET IDENTITY_INSERT [dbo].[Feedback] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldTrip] ON 
GO
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review], [numberOfParticipantsLimit]) VALUES (22, N'Chuyến Thăm Chim Chào Mào Sâu Rừng
', N'Explore deep into the jungle to observe the elusive Bulbul birds in their natural habitat, amidst lush foliage and diverse wildlife.', N'Participants will embark on a guided trek led by experienced naturalists, venturing into remote jungle areas known for Bulbul bird sightings.', CAST(N'2023-01-15' AS Date), CAST(N'2023-02-01' AS Date), CAST(N'2023-02-03' AS Date), 3, N'Open', CAST(30.00 AS Decimal(10, 2)), 20, N'John Doe', N'Jane Smith', N'Be prepared for rugged terrain and humid conditions, and respect the natural environment by minimizing disturbance to wildlife and vegetation.', N'This field trip offered a thrilling adventure into the heart of the jungle, providing a rare opportunity to observe Bulbul birds in their natural habitat. The guided trek was well-organized, and we were able to spot several Bulbul species amidst the dense foliage. The experience was unforgettable, and I gained a deeper appreciation for the beauty and diversity of jungle ecosystems.', 100)
GO
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review], [numberOfParticipantsLimit]) VALUES (23, N'Hành Trình Quan Sát Chim Chào Mào Hoang Dã
', N'Embark on an expedition to observe Bulbul birds in their wild habitats, including forests, grasslands, and wetlands.', N'The trip includes visits to various natural habitats favored by Bulbul birds, with opportunities for birdwatching and photography.', CAST(N'2023-02-28' AS Date), CAST(N'2023-03-15' AS Date), CAST(N'2023-03-17' AS Date), 3, N'Open', CAST(40.00 AS Decimal(10, 2)), 15, N'Emily White', N'Robert Johnson', N'Bring binoculars, cameras, and field guides for bird identification, and follow ethical guidelines for wildlife observation to avoid disturbing the birds.', N'The expedition was a fantastic opportunity to observe Bulbul birds in various wild habitats, including forests and wetlands. Our guides were knowledgeable and helped us identify different bird species, enhancing the overall experience. The trip was well-paced, allowing for ample time to appreciate the natural surroundings and capture stunning photographs of the birds.', 100)
GO
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review], [numberOfParticipantsLimit]) VALUES (24, N'Điều Tra Chim Chào Mào Vùng Cao
', N'Conduct a survey of Bulbul birds in highland regions, exploring mountainous terrain and montane forests.', N'Participants will hike to elevated areas known for Bulbul bird populations, conducting observations and collecting data on their distribution and behavior.', CAST(N'2023-03-20' AS Date), CAST(N'2023-04-10' AS Date), CAST(N'2023-04-12' AS Date), 3, N'Canceled', CAST(25.00 AS Decimal(10, 2)), 10, N'Michael Green', N'Sarah Brown', N'Prepare for altitude and weather changes, and adhere to safety guidelines for hiking in mountainous areas.', N'This survey provided valuable insights into the distribution and behavior of Bulbul birds in highland regions. The hike was challenging but rewarding, and we were able to document several Bulbul sightings at elevated altitudes. The trip was educational and contributed to our understanding of how Bulbul birds adapt to mountainous environments.', 100)
GO
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review], [numberOfParticipantsLimit]) VALUES (25, N'Dạo Chơi Chim Chào Mào Trong Rừng Nguyên Sinh
', N'Take a leisurely stroll through pristine forests to observe Bulbul birds amidst towering trees and rich biodiversity.', N'Take a leisurely stroll through pristine forests to observe Bulbul birds amidst towering trees and rich biodiversity.', CAST(N'2023-04-25' AS Date), CAST(N'2023-05-10' AS Date), CAST(N'2023-05-12' AS Date), 4, N'Open', CAST(35.00 AS Decimal(10, 2)), 25, N'Daniel Taylor', N'Olivia Thompson', N'Stay on designated trails to minimize impact on fragile ecosystems, and be respectful of wildlife and plant life encountered along the way.', N'The leisurely stroll through pristine forests was a peaceful and rejuvenating experience. We encountered numerous Bulbul birds along the way and marveled at their natural behaviors. The trip offered a perfect blend of relaxation and exploration, allowing us to reconnect with nature and escape the hustle and bustle of city life.', 100)
GO
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review], [numberOfParticipantsLimit]) VALUES (26, N'Khám Phá Chim Chào Mào Tại Khu Bảo Tồn Thiên Nhiên
', N'Explore nature reserves dedicated to the conservation of Bulbul birds and their habitats, learning about ongoing conservation efforts.', N' Participants will visit protected areas known for Bulbul bird populations, accompanied by park rangers or conservationists who will provide insights into habitat management and conservation practices.', CAST(N'2023-05-30' AS Date), CAST(N'2023-06-15' AS Date), CAST(N'2023-06-17' AS Date), 5, N'Open', CAST(50.00 AS Decimal(10, 2)), 15, N'William Martin', N'Sophia Davis', N'Follow park regulations and guidelines, and learn about the threats facing Bulbul birds and the importance of their conservation.', N'Exploring nature reserves dedicated to Bulbul bird conservation was both enlightening and inspiring. Learning about ongoing conservation efforts was eye-opening, and witnessing Bulbul birds in their protected habitats was a humbling experience. The trip left a lasting impression and motivated me to support conservation initiatives.', 100)
GO
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review], [numberOfParticipantsLimit]) VALUES (27, N'Cuộc Hành Trình theo Dấu Chân Chim Chào Mào
', N'Trace the footsteps of Bulbul birds through diverse habitats, learning about their ecological roles and behaviors.', N'The journey will take participants through various habitats frequented by Bulbul birds, with opportunities to observe feeding, nesting, and social interactions.', CAST(N'2023-06-25' AS Date), CAST(N'2023-07-10' AS Date), CAST(N'2023-07-12' AS Date), 6, N'Open', CAST(30.00 AS Decimal(10, 2)), 12, N'Daniel Baker', N'Emma Turner', N'Take notes on bird sightings and behaviors to contribute to citizen science projects or local conservation efforts.', N'Following the footsteps of Bulbul birds through diverse habitats was a fascinating journey. We gained valuable insights into their ecological roles and observed their behaviors up close. The trip was educational and instilled a sense of responsibility towards protecting these magnificent creatures and their habitats.', 100)
GO
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review], [numberOfParticipantsLimit]) VALUES (28, N'Trải Nghiệm Chim Chào Mào và Thiên Nhiên Rừng Xanh
', N'Immerse yourself in the beauty of green forests while observing Bulbul birds and experiencing the tranquility of nature.', N'The trip includes guided walks through forested areas, birdwatching sessions, and relaxation amidst the sights and sounds of the forest.', CAST(N'2023-07-20' AS Date), CAST(N'2023-08-05' AS Date), CAST(N'2023-08-07' AS Date), 7, N'Open', CAST(45.00 AS Decimal(10, 2)), 8, N'Alex Clark', N'Mia Harris', N'Practice mindfulness and appreciation for the natural world, and take time to observe and reflect on the interconnectedness of all living things.', N'This nature experience provided a much-needed escape into the tranquility of green forests. Observing Bulbul birds in their natural habitat was a therapeutic experience, and the serene surroundings offered a sense of peace and harmony. The trip was a reminder of the beauty and resilience of nature.', 100)
GO
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review], [numberOfParticipantsLimit]) VALUES (29, N'Điều Tra Chim Chào Mào Sông Nước Mênh Mông
', N'Venture into expansive wetland habitats to study Bulbul birds and their adaptations to aquatic environments.', N'Participants will travel by boat or kayak to explore wetland areas known for Bulbul bird populations, observing their foraging behaviors and interactions with other wetland species.', CAST(N'2023-08-25' AS Date), CAST(N'2023-09-10' AS Date), CAST(N'2023-09-12' AS Date), 8, N'Open', CAST(55.00 AS Decimal(10, 2)), 18, N'Sophie Roberts', N'Jack Anderson', N'Respect the delicate balance of wetland ecosystems and avoid disturbing nesting sites or disrupting natural processes.', N'Exploring vast wetland habitats was an unforgettable adventure filled with birdwatching and exploration. We encountered a rich diversity of bird species, including Bulbul birds, and learned about the importance of wetlands for biodiversity conservation. The trip was both educational and awe-inspiring.', 100)
GO
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review], [numberOfParticipantsLimit]) VALUES (30, N'Khám Phá Sinh Cảnh và Chim Chào Mào Trong Vùng Đất Trống
', N'Discover vast open landscapes and observe Bulbul birds thriving in grasslands, savannas, and agricultural areas.', N'The trip offers opportunities to explore diverse habitats outside of forests, with a focus on observing Bulbul birds in open environments.', CAST(N'2023-09-20' AS Date), CAST(N'2023-10-05' AS Date), CAST(N'2023-10-07' AS Date), 9, N'Open', CAST(60.00 AS Decimal(10, 2)), 10, N'Benjamin Turner', N'Ava Miller', N'Appreciate the beauty and importance of open landscapes for bird diversity and ecosystem health, and learn about sustainable land management practices.', N'Exploring open landscapes and observing Bulbul birds was a delightful experience. The trip offered breathtaking scenery and provided insights into the unique adaptations of Bulbul birds to different environments. It was a fantastic opportunity to connect with nature and appreciate the beauty of open landscapes.', 100)
GO
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review], [numberOfParticipantsLimit]) VALUES (31, N'Dạo Chơi Chim Chào Mào Ở Công Viên Quốc Gia
', N'Enjoy birdwatching and nature exploration in national parks known for their biodiversity and Bulbul bird populations.', N'Participants will visit designated birdwatching areas within national parks, guided by park staff or experienced birdwatchers.', CAST(N'2023-10-15' AS Date), CAST(N'2023-11-01' AS Date), CAST(N'2023-11-03' AS Date), 10, N'Open', CAST(35.00 AS Decimal(10, 2)), 15, N'Liam Johnson', N'Ella Davis', N'Support conservation efforts by visiting national parks responsibly, respecting park rules, and raising awareness about the importance of protecting natural habitats.', N'Exploring national parks and observing Bulbul birds was a memorable experience. The trip provided valuable opportunities for birdwatching and nature appreciation, and the guidance of park staff enhanced the overall experience. It was a wonderful way to support conservation efforts while enjoying the beauty of protected natural areas.', 100)
GO
SET IDENTITY_INSERT [dbo].[FieldTrip] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldtripDaybyDay] ON 
GO
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (22, 2, 2, N'Day 1: Arrival at Jungle Reserve, set up camp, introductory session about Bulbul birds.
Day 2: Guided hike through the jungle, birdwatching sessions, evening campfire and storytelling.', N'img001.jpg')
GO
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (23, 3, 3, N'Day 1: Departure to various habitats, birdwatching in forests.
Day 2: Explore grasslands and wetlands, identify different species of Bulbul birds.
Day 3: Final birdwatching sessions, return to base camp, closing ceremony.', N'img002.jpg')
GO
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (24, 4, 4, N'Day 1: Travel to Highland Mountains, setup base camp.
Day 2: Conduct bird surveys in different altitudes, collect data.
Day 3: Continue surveys, data analysis session in the evening.
Day 4: Wrap-up, pack up, and return journey.', N'img003.jpg')
GO
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (25, 5, 1, N'Day 1: Guided walk through pristine forests, birdwatching sessions.', N'img004.jpg')
GO
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (26, 6, 2, N'Day 1: Arrival at Nature Reserves, guided tour of reserve facilities.
Day 2: Birdwatching excursions in different zones of the reserve.', N'img005.jpg')
GO
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (27, 7, 2, N'Day 1: Introduction to different habitats, begin tracking Bulbul birds.
Day 2: Explore various habitats, study bird behaviors.', N'img006.jpg')
GO
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (28, 8, 1, N'Day 1: Nature walk in Green Forest Reserve, birdwatching sessions.', N'img007.jpg')
GO
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (29, 9, 3, N'Day 1: Arrival at Wetland Reserves, setup camp.
Day 2: Bird surveys in wetland habitats, data collection.
Day 3: Final surveys, wrap-up session.', N'img008.jpg')
GO
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (30, 10, 2, N'Day 1: Departure to Open Landscapes, birdwatching sessions in grasslands.
Day 2: Explore savannas and agricultural areas, observe Bulbul birds in their natural habitats.', N'img009.jpg')
GO
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (31, 11, 2, N'Day 1: Arrival at National Parks, guided tour of park facilities.
Day 2: Birdwatching excursions in different zones of the park.', N'img010.jpg')
GO
SET IDENTITY_INSERT [dbo].[FieldtripDaybyDay] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldtripGettingThere] ON 
GO
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (22, 11, N'Off-road vehicles or 4x4 trucks may be used to access remote jungle areas, followed by hiking or trekking on foot.')
GO
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (23, 12, N'Transportation could involve buses or vans to reach different habitats, with additional travel by foot or boat depending on the terrain.')
GO
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (24, 13, N'Transportation might involve vans or SUVs for mountainous terrain, followed by hiking or trekking to higher elevations.')
GO
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (25, 14, N'Transportation could involve vans or buses to reach forested areas, with guided walks or hikes within the forest on foot.')
GO
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (26, 15, N'Transportation may include vans or buses for travel to nature reserves, followed by guided tours or walks within the reserve.')
GO
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (27, 16, N'Transportation options could involve vans or buses to reach various habitats, with guided walks or hikes on foot to follow the bird''s footsteps.')
GO
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (28, 17, N'Transportation may involve vans or buses for travel to forested areas, followed by guided walks or hikes on foot within the forest.')
GO
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (29, 18, N'Transportation could include boats or kayaks to navigate wetland areas, with additional travel by foot or vehicle to reach specific observation points.')
GO
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (30, 19, N'Transportation options may involve vans or buses for travel to open landscapes, with guided walks or hikes on foot within the area.')
GO
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (31, 20, N'Transportation could involve vans or buses for travel to national parks, with guided tours or walks within the park on foot or via designated transportation routes.')
GO
SET IDENTITY_INSERT [dbo].[FieldtripGettingThere] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldtripInclusions] ON 
GO
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (22, 1, N'Camping gear', N'Tents, sleeping bags, cooking equipment', N'Equipment')
GO
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (23, 2, N'Binoculars and spotting scopes', N'Spotting', N'Equipment')
GO
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (24, 3, N'Altitude sickness medication', N'Medication provided for safety', N'Health')
GO
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (25, 4, N'Field guides on local flora and fauna', N'Enrich your experience', N'Reference')
GO
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (26, 5, N'Park entrance fees', N'Included in the package', N'Admission')
GO
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (27, 6, N'Bird checklist', N'Help you identify species', N'Reference')
GO
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (28, 7, N'Nature journal', N'Recording observations and memories', N'Reference')
GO
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (29, 8, N'Waterproof gear', N'Keep you dry', N'Equipment')
GO
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (30, 9, N'Sunscreen and hats', N'Sun protection', N'Health')
GO
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (31, 10, N'Park maps', N'Navigation and exploration', N'Reference')
GO
SET IDENTITY_INSERT [dbo].[FieldtripInclusions] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldtripMedia] ON 
GO
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (1, 22, N'Scenic views from the Red Bulbul Field Trip', N'/fieldtrip_images/fieldtrip_image_1.jpg')
GO
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (2, 22, N'Participants enjoying the nature walk', N'/fieldtrip_images/fieldtrip_image_2.jpg')
GO
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (3, 23, N'Exploring diverse bird habitats during the field trip', N'/fieldtrip_images/fieldtrip_image_3.jpg')
GO
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (4, 23, N'Birdwatching in the early morning', N'/fieldtrip_images/fieldtrip_image_4.jpg')
GO
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (5, 24, N'Learning about bird behavior from experts', N'/fieldtrip_images/fieldtrip_image_5.jpg')
GO
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (6, 24, N'Capturing the beauty of rare bird species', N'/fieldtrip_images/fieldtrip_image_6.jpg')
GO
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (7, 25, N'Field trip attendees observing bird nesting sites', N'/fieldtrip_images/fieldtrip_image_7.jpg')
GO
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (8, 25, N'Bird enthusiasts documenting their findings', N'/fieldtrip_images/fieldtrip_image_8.jpg')
GO
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (9, 26, N'Field trip group exploring a new birding destination', N'/fieldtrip_images/fieldtrip_image_9.jpg')
GO
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (10, 27, N'Memorable moments from the field trip', N'/fieldtrip_images/fieldtrip_image_10.jpg')
GO
SET IDENTITY_INSERT [dbo].[FieldtripMedia] OFF
GO
INSERT [dbo].[FieldTripOverview] ([tripId], [title], [description], [duration], [price], [destination], [registrationDeadline], [startDate], [endDate], [pictureId], [userReview]) VALUES (22, N'Chuyến Thăm Chim Chào Mào Sâu Rừng
', N'Embark on an adventure deep into the jungle to explore the habitat of Bulbul birds.', 2, CAST(200.00 AS Decimal(10, 2)), N'Jungle Reserve', CAST(N'2024-05-01' AS Date), CAST(N'2024-05-15' AS Date), CAST(N'2024-05-16' AS Date), 1, N'An exhilarating journey through the heart of the jungle.')
GO
INSERT [dbo].[FieldTripOverview] ([tripId], [title], [description], [duration], [price], [destination], [registrationDeadline], [startDate], [endDate], [pictureId], [userReview]) VALUES (23, N'Hành Trình Quan Sát Chim Chào Mào Hoang Dã
', N'Join an expedition to observe Bulbul birds in various wild habitats, including forests, grasslands, and wetlands.', 3, CAST(250.00 AS Decimal(10, 2)), N'Various habitats', CAST(N'2024-06-01' AS Date), CAST(N'2024-06-20' AS Date), CAST(N'2024-06-23' AS Date), 2, N'A fantastic opportunity to observe Bulbul birds in their natural habitats.')
GO
INSERT [dbo].[FieldTripOverview] ([tripId], [title], [description], [duration], [price], [destination], [registrationDeadline], [startDate], [endDate], [pictureId], [userReview]) VALUES (24, N'Điều Tra Chim Chào Mào Vùng Cao
', N'Conduct a survey of Bulbul birds in highland regions, exploring mountainous terrain and montane forests.', 4, CAST(300.00 AS Decimal(10, 2)), N'Highland Mountains', CAST(N'2024-07-01' AS Date), CAST(N'2024-07-25' AS Date), CAST(N'2024-07-28' AS Date), 3, N'Gain insights into the distribution and behavior of Bulbul birds in highland areas.')
GO
INSERT [dbo].[FieldTripOverview] ([tripId], [title], [description], [duration], [price], [destination], [registrationDeadline], [startDate], [endDate], [pictureId], [userReview]) VALUES (25, N'Dạo Chơi Chim Chào Mào Trong Rừng Nguyên Sinh
', N'Take a leisurely stroll through pristine forests to observe Bulbul birds amidst towering trees and rich biodiversity.', 1, CAST(150.00 AS Decimal(10, 2)), N'Pristine Forest Reserve', CAST(N'2024-08-01' AS Date), CAST(N'2024-08-10' AS Date), CAST(N'2024-08-10' AS Date), 4, N'A peaceful and rejuvenating experience in the heart of nature.')
GO
INSERT [dbo].[FieldTripOverview] ([tripId], [title], [description], [duration], [price], [destination], [registrationDeadline], [startDate], [endDate], [pictureId], [userReview]) VALUES (26, N'Khám Phá Chim Chào Mào Tại Khu Bảo Tồn Thiên Nhiên
', N'Explore nature reserves dedicated to the conservation of Bulbul birds and their habitats.', 2, CAST(200.00 AS Decimal(10, 2)), N'Nature Reserves', CAST(N'2024-09-01' AS Date), CAST(N'2024-09-12' AS Date), CAST(N'2024-09-13' AS Date), 5, N'Learn about conservation efforts and witness Bulbul birds in protected habitats.')
GO
INSERT [dbo].[FieldTripOverview] ([tripId], [title], [description], [duration], [price], [destination], [registrationDeadline], [startDate], [endDate], [pictureId], [userReview]) VALUES (27, N'Cuộc Hành Trình theo Dấu Chân Chim Chào Mào
', N'Trace the footsteps of Bulbul birds through diverse habitats, learning about their ecological roles and behaviors.', 2, CAST(180.00 AS Decimal(10, 2)), N'Various habitats', CAST(N'2024-10-01' AS Date), CAST(N'2024-10-18' AS Date), CAST(N'2024-10-19' AS Date), 6, N'Gain insights into the behavior and adaptations of Bulbul birds.')
GO
INSERT [dbo].[FieldTripOverview] ([tripId], [title], [description], [duration], [price], [destination], [registrationDeadline], [startDate], [endDate], [pictureId], [userReview]) VALUES (28, N'Trải Nghiệm Chim Chào Mào và Thiên Nhiên Rừng Xanh
', N'Immerse yourself in the beauty of green forests while observing Bulbul birds and experiencing the tranquility of nature.', 1, CAST(120.00 AS Decimal(10, 2)), N'Green Forest Reserve', CAST(N'2024-11-01' AS Date), CAST(N'2024-11-07' AS Date), CAST(N'2024-11-07' AS Date), 7, N'A serene and therapeutic experience amidst nature.')
GO
INSERT [dbo].[FieldTripOverview] ([tripId], [title], [description], [duration], [price], [destination], [registrationDeadline], [startDate], [endDate], [pictureId], [userReview]) VALUES (29, N'Điều Tra Chim Chào Mào Sông Nước Mênh Mông
', N'Venture into expansive wetland habitats to study Bulbul birds and their adaptations to aquatic environments.', 3, CAST(250.00 AS Decimal(10, 2)), N'Wetland Reserves', CAST(N'2024-12-01' AS Date), CAST(N'2024-12-20' AS Date), CAST(N'2024-12-23' AS Date), 8, N'Discover the rich biodiversity of wetland ecosystems.')
GO
INSERT [dbo].[FieldTripOverview] ([tripId], [title], [description], [duration], [price], [destination], [registrationDeadline], [startDate], [endDate], [pictureId], [userReview]) VALUES (30, N'Khám Phá Sinh Cảnh và Chim Chào Mào Trong Vùng Đất Trống
', N'Discover vast open landscapes and observe Bulbul birds thriving in grasslands, savannas, and agricultural areas.', 2, CAST(200.00 AS Decimal(10, 2)), N'Open Landscapes', CAST(N'2025-01-01' AS Date), CAST(N'2025-01-15' AS Date), CAST(N'2025-01-16' AS Date), 9, N'Appreciate the beauty of open landscapes and their importance for bird diversity.')
GO
INSERT [dbo].[FieldTripOverview] ([tripId], [title], [description], [duration], [price], [destination], [registrationDeadline], [startDate], [endDate], [pictureId], [userReview]) VALUES (31, N'Bulbul Bird Excursion in National Parks', N'Enjoy birdwatching and nature exploration in national parks known for their biodiversity and Bulbul bird populations.', 2, CAST(220.00 AS Decimal(10, 2)), N'National Parks', CAST(N'2025-02-01' AS Date), CAST(N'2025-02-10' AS Date), CAST(N'2025-02-11' AS Date), 10, N'Support conservation efforts while enjoying the beauty of protected natural areas.')
GO
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'1', N'1', N'Checked-In')
GO
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'2', N'2', N'Checked-In')
GO
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (22, N'3', N'3', N'Checked-In')
GO
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (23, N'4', N'1', N'Checked-In')
GO
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (23, N'5', N'2', N'Checked-In')
GO
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (24, N'6', N'1', N'Checked-In')
GO
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (24, N'7', N'2', N'Checked-In')
GO
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (24, N'8', N'3', N'Checked-In')
GO
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (25, N'9', N'1', N'Checked-In')
GO
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo], [checkInStatus]) VALUES (26, N'10', N'1', N'Checked-In')
GO
SET IDENTITY_INSERT [dbo].[FieldtripRates] ON 
GO
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType]) VALUES (22, 1, N'Regular')
GO
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType]) VALUES (23, 2, N'Member')
GO
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType]) VALUES (24, 3, N'Regular')
GO
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType]) VALUES (25, 4, N'Member')
GO
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType]) VALUES (26, 5, N'Regular')
GO
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType]) VALUES (27, 6, N'Member')
GO
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType]) VALUES (28, 7, N'Regular')
GO
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType]) VALUES (29, 8, N'Regular')
GO
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType]) VALUES (30, 9, N'Regular')
GO
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType]) VALUES (31, 10, N'Regular')
GO
SET IDENTITY_INSERT [dbo].[FieldtripRates] OFF
GO
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Beautiful Red Bulbul in Flight', 1, N'red_bulbul_flight.jpg')
GO
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Colorful Plumage of the Red Bulbul', 2, N'plumage_colorful.jpg')
GO
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Close-up of a Red Bulbul Nest', 3, N'nest_closeup.jpg')
GO
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Red Bulbul Perched on a Branch', 4, N'perched_on_branch.jpg')
GO
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Flock of Red Bulbuls in the Morning Sun', 5, N'morning_sun_flock.jpg')
GO
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Red Bulbul Feeding its Chicks', 6, N'feeding_chicks.jpg')
GO
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Red Bulbul Preening its Feathers', 7, N'preening_feathers.jpg')
GO
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Artistic Shot of a Red Bulbul', 8, N'artistic_shot.jpg')
GO
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Red Bulbul Pair Building a Nest', 9, N'building_nest.jpg')
GO
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Red Bulbul in its Natural Habitat', 10, N'natural_habitat.jpg')
GO
SET IDENTITY_INSERT [dbo].[Location] ON 
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (3, N'67, Phố Cổ, Quận Hoàn Kiếm, Hà Nội', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (4, N'89, Hùng Vương, Quận Hồng Bàng, Hải Phòng', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (5, N'45, Trần Phú, Quận Hải Châu, Đà Nẵng', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (6, N'112, Nguyễn Huệ, Quận 1, TP. Hồ Chí Minh', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (7, N'31, Hai Bà Trưng, Quận Ninh Kiều, Cần Thơ', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (8, N'76, Trần Phú, Thành phố Nha Trang, Khánh Hòa', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (9, N'14, Lê Duẩn, Quận Thành Thái, Buôn Ma Thuột, Đắk Lắk', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (10, N'50, Quang Trung, Quận Lê Lợi, Thành phố Quy Nhơn, Bình Định', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (11, N'23, Lý Thường Kiệt, Thị xã Đồng Xoài, Bình Phước', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (12, N'98, Hồng Thái, Thành phố Bắc Giang, Bắc Giang', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (13, N'116, Láng Thượng, Đống Đa, Hà Nội', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (14, N'23, Thung Lũng Xanh, Quận 7, Thành phố Hồ Chí Minh', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (15, N'105, Trần Hưng Đạo, Quận 1, Thành phố Hồ Chí Minh', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (16, N'78, Nguyễn Văn Linh, Quận 2, Thành phố Hồ Chí Minh', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (17, N'42, Lê Lợi, Quận 3, Thành phố Hồ Chí Minh', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (18, N'15, Hoàng Văn Thụ, Quận 10, Thành phố Hồ Chí Minh', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (19, N'29, Nguyễn Sinh Sắc, Quận Liên Chiểu, Thành phố Đà Nẵng', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (20, N'56, Trần Phú, Quận Hải Châu, Thành phố Đà Nẵng', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (21, N'37, Nguyễn Văn Cừ, Quận Cẩm Lệ, Thành phố Đà Nẵng', N'')
GO
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (22, N'91, Lê Duẩn, Quận Sơn Trà, Thành phố Đà Nẵng', N'')
GO
SET IDENTITY_INSERT [dbo].[Location] OFF
GO
SET IDENTITY_INSERT [dbo].[Meeting] ON 
GO
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsLimit]) VALUES (1, N'Hội Ngộ Người Yêu Chim Chào Mào
', N'This meeting serves as a platform for Bulbul bird lovers to come together, share experiences, and discuss their passion for these birds.', CAST(N'2023-10-10T00:00:00.000' AS DateTime), CAST(N'2023-11-01T00:00:00.000' AS DateTime), CAST(N'2023-11-02T00:00:00.000' AS DateTime), 50, N'LB Chào mào Hải Dương ', N'LB Chào mào Hải Dương ', 3, N'Open', N'Foster an inclusive and welcoming atmosphere where participants feel comfortable sharing their knowledge and enthusiasm for Bulbul birds.', 100)
GO
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsLimit]) VALUES (2, N'Hội Nghị Người Hâm Mộ Chào Mào
', N'The conference brings together enthusiasts of Bulbul birds to explore topics related to their behavior, conservation, and welfare.', CAST(N'2023-10-15T00:00:00.000' AS DateTime), CAST(N'2023-11-05T00:00:00.000' AS DateTime), CAST(N'2023-11-05T00:00:00.000' AS DateTime), 30, N'CLB Chim Cảnh Cầu Đuống', N'CLB Chim Cảnh Cầu Đuống', 4, N'Open', N'Ensure that presentations and discussions are informative and engaging, catering to both experienced enthusiasts and newcomers to the field.', 100)
GO
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsLimit]) VALUES (3, N'Hội Thảo Người Đam Mê Chim Chào Mào
', N'This seminar is designed for individuals passionate about Bulbul birds to learn from experts, share insights, and discuss challenges.', CAST(N'2023-11-05T00:00:00.000' AS DateTime), CAST(N'2023-12-01T00:00:00.000' AS DateTime), CAST(N'2023-12-01T00:00:00.000' AS DateTime), 100, N'CLB Chào mào Quan Nhân', N'CLB Chào mào Quan Nhân', 6, N'Open', N'Encourage active participation and provide opportunities for networking and collaboration among attendees.', 100)
GO
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsLimit]) VALUES (4, N'Cuộc Họp Cộng Đồng Yêu Chim Chào Mào
', N'The community meeting aims to strengthen bonds among Bulbul bird enthusiasts, promote conservation efforts, and organize collective actions.', CAST(N'2023-10-20T00:00:00.000' AS DateTime), CAST(N'2023-11-15T00:00:00.000' AS DateTime), CAST(N'2023-11-15T00:00:00.000' AS DateTime), 75, N'CLB Chim Cảnh Đan Phượng', N'CLB Chim Cảnh Đan Phượng', 8, N'Open', N'Emphasize the importance of community involvement in bird conservation and encourage members to take an active role in local initiatives.', 100)
GO
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsLimit]) VALUES (5, N'Buổi Gặp Gỡ Chim Chào Mào
', N'This casual meetup provides an opportunity for Bulbul bird enthusiasts to socialize, exchange stories, and enjoy observing these birds.', CAST(N'2023-10-25T00:00:00.000' AS DateTime), CAST(N'2023-11-10T00:00:00.000' AS DateTime), CAST(N'2023-11-15T00:00:00.000' AS DateTime), 40, N'CLB Chào mào Đoàn kết', N'CLB Chào mào Đoàn kết', 3, N'Open', N'Keep the atmosphere relaxed and informal to encourage interaction and camaraderie among participants.', 100)
GO
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsLimit]) VALUES (6, N'Hội Nghị Thú Vị Về Chim Chào Mào
', N'The symposium offers an exciting platform for discussions, presentations, and workshops on various aspects of Bulbul bird biology and ecology.', CAST(N'2023-11-01T00:00:00.000' AS DateTime), CAST(N'2023-12-10T00:00:00.000' AS DateTime), CAST(N'2023-12-10T00:00:00.000' AS DateTime), 60, N'CLB Chào mào Trung Hòa', N'CLB Chào mào Trung Hòa', 6, N'Open', N'Plan diverse and engaging sessions to cater to different interests within the Bulbul bird enthusiast community.', 100)
GO
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsLimit]) VALUES (7, N'Hội Thảo Những Người Yêu Chim Chào Mào
', N'This workshop aims to educate and inspire participants about Bulbul birds through interactive sessions, demonstrations, and hands-on activities.', CAST(N'2023-11-10T00:00:00.000' AS DateTime), CAST(N'2023-12-05T00:00:00.000' AS DateTime), CAST(N'2023-12-05T00:00:00.000' AS DateTime), 35, N'CLB Chim cảnh Đông Anh', N'CLB Chim cảnh Đông Anh', 5, N'Open', N'Provide educational materials and resources to empower participants to become advocates for Bulbul bird conservation.', 100)
GO
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsLimit]) VALUES (8, N'Buổi Hội Thảo Chim Chào Mào
', N'The seminar focuses on sharing knowledge, research findings, and best practices related to the care and conservation of Bulbul birds.', CAST(N'2023-10-30T00:00:00.000' AS DateTime), CAST(N'2023-11-25T00:00:00.000' AS DateTime), CAST(N'2023-11-25T00:00:00.000' AS DateTime), 25, N'CLB chim cảnh Văn Chương ', N'CLB chim cảnh Văn Chương ', 8, N'Open', N'Invite expert speakers and encourage open discussions to facilitate learning and knowledge exchange among participants.', 100)
GO
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsLimit]) VALUES (9, N'Cuộc Họp Về Bảo Tồn Chim Chào Mào
', N'This meeting is dedicated to discussing strategies and initiatives for the conservation and protection of Bulbul bird populations and habitats.', CAST(N'2023-11-15T00:00:00.000' AS DateTime), CAST(N'2023-12-15T00:00:00.000' AS DateTime), CAST(N'2023-12-15T00:00:00.000' AS DateTime), 45, N'CLB Chào mào Dáng Xưa', N'CLB Chào mào Dáng Xưa', 7, N'Open', N'Emphasize the importance of collective action and collaboration among stakeholders to address conservation challenges effectively.
', 100)
GO
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [locationId], [status], [note], [numberOfParticipantsLimit]) VALUES (10, N'Hội Thảo Về Nuôi Chim Chào Mào
', N'The workshop provides practical guidance and tips on the responsible care, breeding, and welfare of Bulbul birds in captivity.', CAST(N'2023-11-20T00:00:00.000' AS DateTime), CAST(N'2023-12-20T00:00:00.000' AS DateTime), CAST(N'2023-12-20T00:00:00.000' AS DateTime), 70, N'CLB Chim Cảnh Xuân Mai', N'CLB Chim Cảnh Xuân Mai', 9, N'Open', N'Ensure that participants receive accurate and ethical advice to promote the well-being of Bulbul birds kept in captivity.', 100)
GO
SET IDENTITY_INSERT [dbo].[Meeting] OFF
GO
SET IDENTITY_INSERT [dbo].[MeetingMedia] ON 
GO
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (1, 1, N'Memorable moments from the Red Bulbul Club Meeting', N'/meeting_images/meeting_image_1.jpg')
GO
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (2, 1, N'Guest speaker sharing insights about bird conservation', N'/meeting_images/meeting_image_2.jpg')
GO
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (3, 2, N'Members engaged in a lively discussion', N'/meeting_images/meeting_image_3.jpg')
GO
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (4, 2, N'Exploring new ideas for the bird club', N'/meeting_images/meeting_image_4.jpg')
GO
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (5, 3, N'Highlights from the annual meeting', N'/meeting_images/meeting_image_5.jpg')
GO
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (6, 3, N'Club members sharing their experiences', N'/meeting_images/meeting_image_6.jpg')
GO
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (7, 4, N'Bird enthusiasts showcasing their latest findings', N'/meeting_images/meeting_image_7.jpg')
GO
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (8, 4, N'Learning about rare bird species', N'/meeting_images/meeting_image_8.jpg')
GO
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (9, 5, N'Interactive session on bird photography techniques', N'/meeting_images/meeting_image_9.jpg')
GO
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (10, 5, N'Networking and socializing at the club meeting', N'/meeting_images/meeting_image_10.jpg')
GO
SET IDENTITY_INSERT [dbo].[MeetingMedia] OFF
GO
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (1, N'1', N'1', N'Checked-In')
GO
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (1, N'2', N'2', N'Checked-In')
GO
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (1, N'3', N'3', N'Checked-In')
GO
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (2, N'4', N'1', N'Checked-In')
GO
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (2, N'5', N'2', N'Checked-In')
GO
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (3, N'6', N'1', N'Checked-In')
GO
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (3, N'7', N'2', N'Checked-In')
GO
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (4, N'8', N'1', N'Checked-In')
GO
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (4, N'9', N'2', N'Checked-In')
GO
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo], [checkInStatus]) VALUES (5, N'10', N'1', N'Checked-In')
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'1', N'John Doe', N'john_doe', N'john.doe@email.com', N'Male', N'Member', N'123 Main St, City', N'555-1234', N'Passionate birdwatcher', N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'10', N'Sophia Davis', N'sophia_d', N'sophia.d@email.com', N'Female', N'Member', N'777 Oak Ave, City', N'555-3456', N'Wildlife advocate', N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'11', N'Đào Minh Hòa', N'Hòa', N'dm.hoa01@gmail.com', N'Male', N'Member', N'149/9 Nhiêu Tứ, Phường 7, Quận Phú Nhuận', N'0906693410', N'Member', N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'12', N'Trương Minh Huấn', N'Huấn', N'tmh@gmail.com', N'Male', N'Member', N'Xã Tam Bố, Huyện Di Linh, Lâm Đồng', N'059 059 3641', NULL, N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'13', N'Dương Thanh Tuấn', N'Tuấn', N'duongthanhtuan33@hotmail.com', N'Male', N'Member', N'Xã Sà Dề Phìn, Huyện Sìn Hồ, Lai Châu', N'084 346 1580', NULL, N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'14', N'Lê Ðức Hoàng', N'Hoàng', N'leduchoanggg987@facebook.com', N'Male', N'Member', N'Xã Hòa Nghĩa, Huyện Chợ Lách, Bến Tre', N'086 932 6514', NULL, N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'15', N'Mạc Xuân Hãn', N'Hãn', N'macxuanhan836@facebook.com', N'Male', N'Member', N'Phường Hưng Phú, Quận Cái Răng, Cần Thơ', N'078 086 3257
', NULL, N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'16', N'Đoàn Trí Hữu', N'Hữu', N'doantrihuu22@gmail.com', N'Male', N'Member', N'Xã Vũ Hoà, Huyện Đức Linh, Bình Thuận', N'086 738 1069', NULL, N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'17', N'Nguyễn Hữu Lễ', N'Lễ', N'nguyenhuule352@facebook.com', N'Male', N'Member', N'Xã Trùng Khánh, Huyện Gia Lộc, Hải Dương', N'038 780 5941', NULL, N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'18', N'Văn Ngọc Cường', N'Cường', N'vanngoccuong972@gmail.com', N'Male', N'Member', N'Xã Phi Tô, Huyện Lâm Hà, Lâm Đồng', N'094 501 6438', NULL, N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'19', N'Phan Hiểu Minh', N'Minh', N'phanhieuminh95@facebook.com', N'Male', N'Member', N'Xã Mường Và, Huyện Sốp Cộp, Sơn La', N'078 916 4308', NULL, N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'2', N'Jane Smith', N'jane_smith', N'jane.smith@email.com', N'Female', N'Member', N'456 Oak Ave, Town', N'555-5678', N'Nature photographer', N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'3', N'Robert Johnson', N'robert_j', N'robert.j@email.com', N'Male', N'Member', N'789 Pine Blvd, Village', N'555-9876', N'Avian researcher', N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'4', N'Emily White', N'emily_white', N'emily.white@email.com', N'Female', N'Member', N'101 Cedar St, City', N'555-5432', N'Nature educator', N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'5', N'Michael Green', N'michael_g', N'michael.g@email.com', N'Male', N'Member', N'222 Elm St, Town', N'555-1122', N'Wildlife conservationist', N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'6', N'Sarah Black', N'sarah_b', N'sarah.b@email.com', N'Female', N'Member', N'333 Oak Ave, Village', N'555-3344', N'Nature enthusiast', N'Inactive', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'7', N'Daniel Brown', N'daniel_b', N'daniel.b@email.com', N'Male', N'Member', N'444 Pine Blvd, City', N'555-8765', N'Birdwatching enthusiast', N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'8', N'Olivia Taylor', N'olivia_t', N'olivia.t@email.com', N'Female', N'Member', N'555 Cedar St, Town', N'555-7890', N'Bird photographer', N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'9', N'William Miller', N'william_m', N'william.m@email.com', N'Male', N'Member', N'666 Elm St, Village', N'555-6543', N'Ornithologist', N'Active', 1)
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'caca31b7-5f87-440f-96d4-84655e91926b', N'Daniel Echocraft', N'DanielEcho', N'danielecho@gmail.com', N'Male', NULL, NULL, N'0434282329', NULL, N'Active', NULL)
GO
SET IDENTITY_INSERT [dbo].[News] ON 
GO
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (1, N'New Bird Species Discovered', N'Discovery', N'Exciting news about a new bird species found in our region.', CAST(N'2023-01-15T00:00:00.000' AS DateTime), N'Published', N'image1.jpg', N'document1.pdf', 1)
GO
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (2, N'Upcoming Bird Watching Event', N'Event', N'Join us for a bird watching event on the upcoming weekend.', CAST(N'2023-02-10T00:00:00.000' AS DateTime), N'Published', N'image2.jpg', N'document2.pdf', 2)
GO
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (3, N'Conservation Success Story', N'Conservation', N'Our efforts in bird conservation have shown positive results.', CAST(N'2023-03-25T00:00:00.000' AS DateTime), N'Draft', N'image3.jpg', N'document3.pdf', 3)
GO
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (4, N'Bird Photography Contest Winners', N'Contest', N'Announcing the winners of our recent bird photography contest.', CAST(N'2023-04-05T00:00:00.000' AS DateTime), N'Published', N'image4.jpg', N'document4.pdf', 4)
GO
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (5, N'Important Bird Migration Update', N'Migration', N'Stay informed about the latest bird migration patterns in our region.', CAST(N'2023-05-20T00:00:00.000' AS DateTime), N'Published', N'image5.jpg', N'document5.pdf', 5)
GO
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (6, N'Volunteer Opportunities', N'Volunteer', N'Get involved in bird-related conservation projects. Volunteer with us!', CAST(N'2023-06-15T00:00:00.000' AS DateTime), N'Published', N'image6.jpg', N'document6.pdf', 6)
GO
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (7, N'Bird Club Achievements', N'Achievement', N'Celebrating the achievements and milestones of our bird club members.', CAST(N'2023-07-02T00:00:00.000' AS DateTime), N'Published', N'image7.jpg', N'document7.pdf', 7)
GO
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (8, N'Educational Workshop on Bird Identification', N'Workshop', N'Learn the basics of bird identification in our upcoming workshop.', CAST(N'2023-08-15T00:00:00.000' AS DateTime), N'Draft', N'image8.jpg', N'document8.pdf', 8)
GO
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (9, N'Bird Club Newsletter - Summer Edition', N'Newsletter', N'Stay updated with the latest news and events in our summer newsletter.', CAST(N'2023-09-10T00:00:00.000' AS DateTime), N'Published', N'image9.jpg', N'document9.pdf', 9)
GO
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (10, N'Bird Watching Tips for Beginners', N'Tips', N'New to bird watching? Check out our tips for beginners to get started.', CAST(N'2023-10-01T00:00:00.000' AS DateTime), N'Published', N'image10.jpg', N'document10.pdf', 10)
GO
SET IDENTITY_INSERT [dbo].[News] OFF
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 
GO
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (1, 1, N'Membership Renewal', CAST(50.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-01-02' AS Date), N'Completed', N'DOC001')
GO
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (2, 2, N'Event Registration', CAST(20.00 AS Decimal(10, 2)), CAST(N'2023-02-15' AS Date), CAST(N'2023-02-16' AS Date), N'Completed', N'DOC002')
GO
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (3, 3, N'Donation', CAST(30.00 AS Decimal(10, 2)), CAST(N'2023-03-10' AS Date), CAST(N'2023-03-11' AS Date), N'Pending', N'DOC003')
GO
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (4, 4, N'Membership Renewal', CAST(50.00 AS Decimal(10, 2)), CAST(N'2023-04-05' AS Date), CAST(N'2023-04-06' AS Date), N'Completed', N'DOC004')
GO
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (5, 5, N'Event Registration', CAST(25.00 AS Decimal(10, 2)), CAST(N'2023-05-20' AS Date), CAST(N'2023-05-21' AS Date), N'Completed', N'DOC005')
GO
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (6, 6, N'Donation', CAST(40.00 AS Decimal(10, 2)), CAST(N'2023-06-15' AS Date), CAST(N'2023-06-16' AS Date), N'Completed', N'DOC006')
GO
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (7, 7, N'Membership Renewal', CAST(50.00 AS Decimal(10, 2)), CAST(N'2023-07-01' AS Date), CAST(N'2023-07-02' AS Date), N'Pending', N'DOC007')
GO
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (8, 8, N'Event Registration', CAST(30.00 AS Decimal(10, 2)), CAST(N'2023-08-10' AS Date), CAST(N'2023-08-11' AS Date), N'Completed', N'DOC008')
GO
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (9, 9, N'Donation', CAST(35.00 AS Decimal(10, 2)), CAST(N'2023-09-05' AS Date), CAST(N'2023-09-06' AS Date), N'Pending', N'DOC009')
GO
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (10, 10, N'Membership Renewal', CAST(50.00 AS Decimal(10, 2)), CAST(N'2023-10-20' AS Date), CAST(N'2023-10-21' AS Date), N'Completed', N'DOC010')
GO
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (0, NULL, NULL, N'caca31b7-5f87-440f-96d4-84655e91926b', N'DanielEcho', N'123456', N'Member')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (1, 1, N'~/images/avatar.png', N'1', N'John Doe', N'password123', N'Manager')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (2, 1, N'~/images/avatar.png', N'2', N'Jane Smith', N'securepass', N'Admin')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (3, 2, N'~/images/avatar.png', N'3', N'Robert Johnson', N'pass123', N'Manager')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (4, 3, N'~/images/avatar.png', N'4', N'Emily White', N'secretword', N'Staff')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (5, 4, N'~/images/avatar.png', N'5', N'Michael Green', N'myp@ssw0rd', N'Staff')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (6, 5, N'~/images/avatar.png', N'6', N'Sarah Black', N'strongpassword', N'Member')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (7, 6, N'~/images/avatar.png', N'7', N'Daniel Brown', N'mypass123', N'Member')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (8, 7, N'~/images/avatar.png', N'8', N'Olivia Taylor', N'letmein', N'Staff')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (9, 8, N'~/images/avatar.png', N'9', N'William Miller', N'password456', N'Member')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (10, 9, N'~/images/avatar.png', N'10', N'Sophia Davis', N'secure123', N'Member')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (11, 10, N'~/images/avatar.png', N'11', N'Hòa', N'Test123', N'Admin')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (12, 11, N'~/images/avatar.png', N'12', N'Huấn', N'huan123', N'Member')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (13, 1, N'~/images/avatar.png', N'13', N'Tuấn', N'tuanld', N'Member')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (14, 2, N'~/images/avatar.png', N'14', N'Hoàng', N'hoangchaomao', N'Member')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (15, 1, N'~/images/avatar.png', N'15', N'Hãn', N'hanhan', N'Member')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (16, 3, N'~/images/avatar.png', N'16', N'Hữu', N'trihuuvotri', N'Member')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (17, 4, N'~/images/avatar.png', N'17', N'Lễ', N'lenguyen', N'Member')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (18, 1, N'~/images/avatar.png', N'18', N'Cường', N'vancuong68', N'Member')
GO
INSERT [dbo].[Users] ([userId], [clubId], [imagePath], [memberId], [userName], [password], [role]) VALUES (19, 5, N'~/images/avatar.png', N'19', N'Minh', N'minhnguyen87', N'Member')
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
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Users]
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
ALTER TABLE [dbo].[ContestParticipants]  WITH CHECK ADD  CONSTRAINT [FK__TournamentP__TID__0D7A0286] FOREIGN KEY([contestId])
REFERENCES [dbo].[Contest] ([contestId])
GO
ALTER TABLE [dbo].[ContestParticipants] CHECK CONSTRAINT [FK__TournamentP__TID__0D7A0286]
GO
ALTER TABLE [dbo].[ContestScore]  WITH CHECK ADD  CONSTRAINT [FK_ContestScore_Bird] FOREIGN KEY([birdId])
REFERENCES [dbo].[Bird] ([birdId])
GO
ALTER TABLE [dbo].[ContestScore] CHECK CONSTRAINT [FK_ContestScore_Bird]
GO
ALTER TABLE [dbo].[Feedback]  WITH CHECK ADD  CONSTRAINT [FK_Feedback_Users] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Feedback] CHECK CONSTRAINT [FK_Feedback_Users]
GO
ALTER TABLE [dbo].[FieldtripDaybyDay]  WITH CHECK ADD  CONSTRAINT [FK_FieldtripDaybyDay_FieldTrip] FOREIGN KEY([tripId])
REFERENCES [dbo].[FieldTrip] ([tripId])
GO
ALTER TABLE [dbo].[FieldtripDaybyDay] CHECK CONSTRAINT [FK_FieldtripDaybyDay_FieldTrip]
GO
ALTER TABLE [dbo].[FieldtripGettingThere]  WITH CHECK ADD  CONSTRAINT [FK_FieldtripGettingThere_FieldTrip] FOREIGN KEY([tripId])
REFERENCES [dbo].[FieldTrip] ([tripId])
GO
ALTER TABLE [dbo].[FieldtripGettingThere] CHECK CONSTRAINT [FK_FieldtripGettingThere_FieldTrip]
GO
ALTER TABLE [dbo].[FieldtripInclusions]  WITH CHECK ADD  CONSTRAINT [FK_FieldtripInclusions_FieldTrip] FOREIGN KEY([tripId])
REFERENCES [dbo].[FieldTrip] ([tripId])
GO
ALTER TABLE [dbo].[FieldtripInclusions] CHECK CONSTRAINT [FK_FieldtripInclusions_FieldTrip]
GO
ALTER TABLE [dbo].[FieldtripMedia]  WITH CHECK ADD  CONSTRAINT [FK_FieldTrip] FOREIGN KEY([tripId])
REFERENCES [dbo].[FieldTrip] ([tripId])
GO
ALTER TABLE [dbo].[FieldtripMedia] CHECK CONSTRAINT [FK_FieldTrip]
GO
ALTER TABLE [dbo].[FieldTripOverview]  WITH CHECK ADD  CONSTRAINT [FK_FieldTripOverview_FieldTrip] FOREIGN KEY([tripId])
REFERENCES [dbo].[FieldTrip] ([tripId])
GO
ALTER TABLE [dbo].[FieldTripOverview] CHECK CONSTRAINT [FK_FieldTripOverview_FieldTrip]
GO
ALTER TABLE [dbo].[FieldTripParticipants]  WITH CHECK ADD  CONSTRAINT [FK__FieldTripPa__FID__1332DBDC] FOREIGN KEY([tripId])
REFERENCES [dbo].[FieldTrip] ([tripId])
GO
ALTER TABLE [dbo].[FieldTripParticipants] CHECK CONSTRAINT [FK__FieldTripPa__FID__1332DBDC]
GO
ALTER TABLE [dbo].[FieldTripParticipants]  WITH CHECK ADD  CONSTRAINT [FK_FieldTripParticipants_Member] FOREIGN KEY([memberId])
REFERENCES [dbo].[Member] ([memberId])
GO
ALTER TABLE [dbo].[FieldTripParticipants] CHECK CONSTRAINT [FK_FieldTripParticipants_Member]
GO
ALTER TABLE [dbo].[FieldtripRates]  WITH CHECK ADD  CONSTRAINT [FK_FieldtripRates_FieldTrip] FOREIGN KEY([tripId])
REFERENCES [dbo].[FieldTrip] ([tripId])
GO
ALTER TABLE [dbo].[FieldtripRates] CHECK CONSTRAINT [FK_FieldtripRates_FieldTrip]
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
