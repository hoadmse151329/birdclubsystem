SET IDENTITY_INSERT [dbo].[Bird] ON 

INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (1, N'1', N'Skylar', 1600, 2, N'A colorful and melodious bird with a distinctive red patch on its cheeks.', N'Red, Black, and White', CAST(N'2023-09-01' AS Date), N'red_whiskered_bulbul.jpg', N'Active', N'Northern India')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (2, N'2', N'Ruby', 1500, 1, N'A young Red-whiskered Bulbul, still acquiring its adult plumage.', N'Brown and White', CAST(N'2023-09-02' AS Date), N'baby_red_whiskered_bulbul.jpg', N'Active', N'Nepal')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (3, N'3', N'Indigo', 1750, 4, N'A mature Red-whiskered Bulbul singing melodiously in a garden.', N'Red, Black, and White', CAST(N'2023-09-03' AS Date), N'singing_bulbul.jpg', N'Active', N'Nepal')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (4, N'4', N'Merlin', 1550, 3, N'A Red-whiskered Bulbul perched on a branch with a backdrop of lush green foliage.', N'Red, Black, and White', CAST(N'2023-09-04' AS Date), N'bulbul_in_foliage.jpg', N'Active', N'South-Eastern China')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (5, N'5', N'Willow', 1650, 2, N'A Red-whiskered Bulbul pair in a courtship display.', N'Red, Black, and White', CAST(N'2023-09-05' AS Date), N'bulbul_courtship.jpg', N'Active', N'South-Western Thailand')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (6, N'6', N'Jasper', 1700, 3, N'A Red-whiskered Bulbul feeding on fruits in a tree.', N'Red, Black, and White', CAST(N'2023-09-06' AS Date), N'bulbul_feeding.jpg', N'Active', N'Northern India')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (7, N'7', N'Luna', 1600, 2, N'A Red-whiskered Bulbul bathing in a bird bath.', N'Red, Black, and White', CAST(N'2023-09-07' AS Date), N'bulbul_bathing.jpg', N'Active', N'Northern Myanmar')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (8, N'8', N'Kiko', 1550, 1, N'A juvenile Red-whiskered Bulbul exploring its surroundings.', N'Brown and White', CAST(N'2023-09-08' AS Date), N'young_bulbul.jpg', N'Active', N'South-Eastern China')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (9, N'9', N'Zephyr', 1800, 5, N'A Red-whiskered Bulbul building a nest in a tree.', N'Red, Black, and White', CAST(N'2023-09-09' AS Date), N'bulbul_nest.jpg', N'Active', N'South-Western Thailand')
INSERT [dbo].[Bird] ([birdId], [memberId], [birdName], [ELO], [age], [description], [color], [addDate], [profilePic], [status], [origin]) VALUES (10, N'10', N'Mochi', 1650, 3, N'A Red-whiskered Bulbul perched on a wire with a backdrop of clear blue sky.', N'Red, Black, and White', CAST(N'2023-09-10' AS Date), N'bulbul_on_wire.jpg', N'Active', N'South-Eastern China')
SET IDENTITY_INSERT [dbo].[Bird] OFF
GO
SET IDENTITY_INSERT [dbo].[BirdMedia] ON 

INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (1, 1, N'Beautiful Red Bulbul in flight', N'/images/red_bulbul_flight.jpg')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (2, 1, N'Close-up of Red Bulbul feeding', N'/images/red_bulbul_feeding.jpg')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (3, 2, N'Red Bulbul pair on a branch', N'/images/red_bulbul_pair.jpg')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (4, 2, N'Red Bulbul bathing in a pond', N'/images/red_bulbul_bathing.jpg')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (5, 3, N'Juvenile Red Bulbul exploring', N'/images/red_bulbul_juvenile.jpg')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (6, 3, N'Red Bulbul with nesting material', N'/images/red_bulbul_nesting.jpg')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (7, 4, N'Red Bulbul singing on a tree', N'/images/red_bulbul_singing.jpg')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (8, 4, N'Red Bulbul in its natural habitat', N'/images/red_bulbul_habitat.jpg')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (9, 5, N'Red Bulbul family in the morning', N'/images/red_bulbul_family.jpg')
INSERT [dbo].[BirdMedia] ([pictureId], [birdId], [description], [image]) VALUES (10, 5, N'Red Bulbul perched on a fence', N'/images/red_bulbul_perched.jpg')
SET IDENTITY_INSERT [dbo].[BirdMedia] OFF
GO
SET IDENTITY_INSERT [dbo].[Blog] ON 

INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (1, 101, N'Observations of Red-Whiskered Bulbuls in Central Park', N'Observations', CAST(N'2023-03-01T00:00:00.000' AS DateTime), 15, N'central_park_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (2, 102, N'Tips for Attracting Bulbuls to Your Garden', N'Gardening', CAST(N'2023-03-05T00:00:00.000' AS DateTime), 20, N'garden_tips_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (3, 103, N'The Beauty of Bulbul Nests: A Photo Journey', N'Photography', CAST(N'2023-03-10T00:00:00.000' AS DateTime), 25, N'nest_photo_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (4, 104, N'Red Bulbul Conservation Efforts in Everglades National Park', N'Conservation', CAST(N'2023-03-15T00:00:00.000' AS DateTime), 18, N'conservation_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (5, 105, N'Spotting Rare Bulbul Varieties in Griffith Park', N'Spotting', CAST(N'2023-03-20T00:00:00.000' AS DateTime), 22, N'rare_bulbuls_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (6, 106, N'A Day of Birdwatching at Discovery Park', N'Birdwatching', CAST(N'2023-03-25T00:00:00.000' AS DateTime), 17, N'birdwatching_day_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (7, 107, N'Exploring Stanford University Campus: Birdwatcher’s Paradise', N'Birdwatching', CAST(N'2023-03-30T00:00:00.000' AS DateTime), 23, N'stanford_campus_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (8, 108, N'Winter Bulbul Migration Patterns at South Mountain Park', N'Migration', CAST(N'2023-04-05T00:00:00.000' AS DateTime), 19, N'migration_patterns_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (9, 109, N'Piedmont Park: A Haven for Red Bulbuls', N'Habitat', CAST(N'2023-04-10T00:00:00.000' AS DateTime), 21, N'piedmont_park_blog.jpg', N'Active')
INSERT [dbo].[Blog] ([blogId], [userId], [description], [category], [uploadDate], [vote], [image], [status]) VALUES (10, 110, N'Fairmount Park Bird Festival: Highlights and Discoveries', N'Festival', CAST(N'2023-04-15T00:00:00.000' AS DateTime), 28, N'park_festival_blog.jpg', N'Active')
SET IDENTITY_INSERT [dbo].[Blog] OFF
GO
SET IDENTITY_INSERT [dbo].[ClubInformation] ON 

INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [Description]) VALUES (1, 1, N'The Red Bulbul Bird Club is dedicated to the study and conservation of Red Bulbuls in their natural habitat.')
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [Description]) VALUES (2, 2, N'Our club meets regularly at the Forest Watchpoint to observe and document the behavior of Red Bulbuls.')
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [Description]) VALUES (3, 3, N'Join us at the Conservation Hub to actively participate in our bird conservation initiatives and projects.')
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [Description]) VALUES (4, 4, N'Discover the beauty of Red Bulbuls through our Photography Workshop Center and share your stunning captures with fellow members.')
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [Description]) VALUES (5, 5, N'The Education Center serves as a knowledge hub, providing insights into the ecology and biology of Red Bulbuls.')
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [Description]) VALUES (6, 6, N'Visit the Bulbul Reserve Center for a unique experience of observing Red Bulbuls in a controlled environment.')
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [Description]) VALUES (7, 7, N'Contribute to our Green Garden project, creating a bird-friendly environment to support local bird species.')
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [Description]) VALUES (8, 8, N'Embark on birdwatching hikes starting from the Hiking Trail Start/End Point and explore the natural habitats of Red Bulbuls.')
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [Description]) VALUES (9, 9, N'Get involved in avian research and scientific studies at our Research Base to contribute to the understanding of Red Bulbuls.')
INSERT [dbo].[ClubInformation] ([clubId], [clubLocationId], [Description]) VALUES (10, 10, N'Visit the Club Store for quality birdwatching equipment and merchandise to enhance your birdwatching experience.')
SET IDENTITY_INSERT [dbo].[ClubInformation] OFF
GO
SET IDENTITY_INSERT [dbo].[ClubLocation] ON 

INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (1, N'Main Clubhouse', N'The main hub for club activities and meetings', 1)
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (2, N'Forest Watchpoint', N'A scenic location with a view of the birdwatching area', 2)
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (3, N'Conservation Hub', N'Headquarters for conservation projects and initiatives', 3)
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (4, N'Photography Workshop Center', N'Dedicated space for photography workshops and events', 4)
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (5, N'Education Center', N'Promoting awareness and education about Red Bulbuls', 5)
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (6, N'Bulbul Reserve Center', N'Located within the bird reserve for research and observation', 6)
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (7, N'Green Garden', N'A community garden supporting local bird species', 7)
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (8, N'Hiking Trail Start/End Point', N'Beginning or ending point for birdwatching hikes', 8)
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (9, N'Research Base', N'Facility for avian research and scientific studies', 9)
INSERT [dbo].[ClubLocation] ([clubLocationId], [clubName], [description], [locationId]) VALUES (10, N'Club Store', N'The official store for birdwatching equipment and merchandise', 10)
SET IDENTITY_INSERT [dbo].[ClubLocation] OFF
GO
SET IDENTITY_INSERT [dbo].[Comment] ON 

INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (2, 1, 5, N'Great insights into the behaviors of Red-Whiskered Bulbuls!', CAST(N'2023-03-02' AS Date), 101)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (3, 1, 4, N'I love observing birds in Central Park. Thanks for sharing!', CAST(N'2023-03-03' AS Date), 102)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (4, 2, 3, N'Your gardening tips worked wonders in attracting bulbuls to my garden.', CAST(N'2023-03-06' AS Date), 103)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (5, 2, 5, N'Beautiful photos! Bulbuls are truly fascinating creatures.', CAST(N'2023-03-07' AS Date), 104)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (6, 3, 4, N'The photo journey of bulbul nests is heartwarming.', CAST(N'2023-03-11' AS Date), 105)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (7, 3, 5, N'Nature''s wonders captured in these pictures. Amazing!', CAST(N'2023-03-12' AS Date), 106)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (8, 4, 4, N'Conservation efforts are crucial. Thank you for raising awareness!', CAST(N'2023-03-16' AS Date), 107)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (9, 4, 3, N'Shy or not, every bird plays a vital role in the ecosystem.', CAST(N'2023-03-17' AS Date), 108)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (10, 5, 5, N'Rare varieties are always a joy to spot. Thanks for the guide!', CAST(N'2023-03-21' AS Date), 109)
INSERT [dbo].[Comment] ([commentId], [blogId], [vote], [description], [date], [userId]) VALUES (11, 5, 4, N'The festival was a blast! Looking forward to more events.', CAST(N'2023-03-22' AS Date), 110)
SET IDENTITY_INSERT [dbo].[Comment] OFF
GO
SET IDENTITY_INSERT [dbo].[Contest] ON 

INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId]) VALUES (1, N'Red-whiskered Bulbul Singing Showdown', N'Red-whiskered Bulbuls compete in a melodious singing competition.', CAST(N'2023-10-15T00:00:00.000' AS DateTime), 1, N'Active', CAST(N'2023-11-01' AS Date), CAST(N'2023-11-15' AS Date), 0, 20, CAST(20.00 AS Decimal(10, 2)), CAST(50.00 AS Decimal(12, 2)), N'Red-whiskered Bulbul Club', N'Alice Johnson', N'Open to all Red-whiskered Bulbuls.', N'Good', 50, 1)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId]) VALUES (2, N'Bulbul Beauty Pageant', N'Red-whiskered Bulbuls strut their stuff in a beauty contest.', CAST(N'2023-11-05T00:00:00.000' AS DateTime), 2, N'Active', CAST(N'2023-11-20' AS Date), CAST(N'2023-12-05' AS Date), 0, 20, CAST(15.00 AS Decimal(10, 2)), CAST(100.00 AS Decimal(12, 2)), N'Avian Elegance Society', N'Michael Johnson', N'Show off your plumage.', N'Good', 60, 1)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId]) VALUES (3, N'Bulbul''s Best Nest Challenge', N'Red-whiskered Bulbuls compete to build the sturdiest nests.', CAST(N'2023-10-20T00:00:00.000' AS DateTime), 3, N'Active', CAST(N'2023-11-10' AS Date), CAST(N'2023-11-20' AS Date), 0, 20, CAST(10.00 AS Decimal(10, 2)), CAST(75.00 AS Decimal(12, 2)), N'Nesting Masters Club', N'David Lee', N'Build the best Bulbul nest.', N'Good', 32, 1)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId]) VALUES (4, N'Red-whiskered Bulbul Dance-Off', N'Red-whiskered Bulbuls showcase their dancing talents in a contest.', CAST(N'2023-11-10T00:00:00.000' AS DateTime), 4, N'Active', CAST(N'2023-11-25' AS Date), CAST(N'2023-12-10' AS Date), 0, 20, CAST(25.00 AS Decimal(10, 2)), CAST(120.00 AS Decimal(12, 2)), N'Dance Fever Birds', N'Grace Martinez', N'Bulbul dance competition.', N'Good', 40, 1)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId]) VALUES (5, N'Bulbul Feeding Frenzy', N'Red-whiskered Bulbuls compete in a food-eating contest.', CAST(N'2023-11-15T00:00:00.000' AS DateTime), 5, N'Active', CAST(N'2023-12-01' AS Date), CAST(N'2023-12-15' AS Date), 0, 20, CAST(18.00 AS Decimal(10, 2)), CAST(80.00 AS Decimal(12, 2)), N'Feeding Champs', N'Charlie Adams', N'Eat the most delicious insects.', N'Good', 48, 1)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId]) VALUES (6, N'Red-whiskered Bulbul Quiz Bowl', N'Test your Red-whiskered Bulbul knowledge in a trivia showdown.', CAST(N'2023-10-25T00:00:00.000' AS DateTime), 6, N'Active', CAST(N'2023-11-15' AS Date), CAST(N'2023-11-30' AS Date), 0, 20, CAST(10.00 AS Decimal(10, 2)), CAST(60.00 AS Decimal(12, 2)), N'Bulbul Trivia Enthusiasts', N'Sarah Smith', N'Show off your Bulbul facts.', N'Good', 75, 1)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId]) VALUES (7, N'Bulbul Olympics', N'Red-whiskered Bulbuls compete in various athletic events.', CAST(N'2023-10-20T00:00:00.000' AS DateTime), 7, N'Active', CAST(N'2023-11-05' AS Date), CAST(N'2023-11-15' AS Date), 0, 20, CAST(5.00 AS Decimal(10, 2)), CAST(150.00 AS Decimal(12, 2)), N'Bulbul Athletes Club', N'Emily Wilson', N'Athletic contests for Bulbuls.', N'Good', 45, 1)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId]) VALUES (8, N'Red-whiskered Bulbul Colorful Plumage Parade', N'Bulbuls compete for the most vibrant and attractive plumage.', CAST(N'2023-10-30T00:00:00.000' AS DateTime), 8, N'Active', CAST(N'2023-11-15' AS Date), CAST(N'2023-11-25' AS Date), 0, 20, CAST(12.00 AS Decimal(10, 2)), CAST(90.00 AS Decimal(12, 2)), N'Plumage Parade Society', N'John Doe', N'Show off your colorful feathers.', N'Good', 24, 1)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId]) VALUES (9, N'Bulbul''s Got Talent', N'Red-whiskered Bulbuls showcase their unique talents.', CAST(N'2023-11-10T00:00:00.000' AS DateTime), 9, N'Active', CAST(N'2023-11-25' AS Date), CAST(N'2023-12-10' AS Date), 0, 20, CAST(20.00 AS Decimal(10, 2)), CAST(120.00 AS Decimal(12, 2)), N'Talented Bulbuls Club', N'Mochi', N'Bulbul talent show.', N'Good', 55, 1)
INSERT [dbo].[Contest] ([contestId], [contestName], [description], [registrationDeadline], [locationId], [status], [startDate], [endDate], [beforeScore], [afterScore], [fee], [prize], [host], [incharge], [note], [review], [numberOfParticipants], [clubId]) VALUES (10, N'Red-whiskered Bulbul Best Song Contest', N'Bulbuls compete to sing the best songs.', CAST(N'2023-10-25T00:00:00.000' AS DateTime), 10, N'Active', CAST(N'2023-11-10' AS Date), CAST(N'2023-11-20' AS Date), 0, 20, CAST(8.00 AS Decimal(10, 2)), CAST(40.00 AS Decimal(12, 2)), N'Bulbul Songbird Society', N'Zephyr', N'Show your singing skills.', N'Good', 10, 1)
SET IDENTITY_INSERT [dbo].[Contest] OFF
GO
SET IDENTITY_INSERT [dbo].[ContestMedia] ON 

INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (1, 1, N'Captivating moments from the Red Bulbul Contest', N'/contest_images/contest_image_1.jpg')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (2, 1, N'The winning bird in action', N'/contest_images/contest_image_2.jpg')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (3, 2, N'Contest participants in their natural habitat', N'/contest_images/contest_image_3.jpg')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (4, 2, N'Admiring the beauty of Red-Whiskered Bulbuls', N'/contest_images/contest_image_4.jpg')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (5, 3, N'Close-up of the winning bird', N'/contest_images/contest_image_5.jpg')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (6, 3, N'Judges evaluating the contestants', N'/contest_images/contest_image_6.jpg')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (7, 4, N'Diverse bird species showcased in the contest', N'/contest_images/contest_image_7.jpg')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (8, 4, N'Bird enthusiasts enjoying the contest', N'/contest_images/contest_image_8.jpg')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (9, 5, N'Birds in flight during the contest', N'/contest_images/contest_image_9.jpg')
INSERT [dbo].[ContestMedia] ([pictureId], [contestId], [description], [image]) VALUES (10, 5, N'Celebrating the successful conclusion of the contest', N'/contest_images/contest_image_10.jpg')
SET IDENTITY_INSERT [dbo].[ContestMedia] OFF
GO
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (1, 1, 1600, N'1', N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (1, 2, 1500, N'2', N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (1, 3, 1750, N'3', N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (2, 4, 1550, N'1', N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (2, 5, 1650, N'2', N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (3, 6, 1700, N'1', N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (3, 7, 1600, N'2', N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (4, 8, 1550, N'1', N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (4, 9, 1800, N'2', N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (5, 10, 1650, N'1', N'Checked-In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (1, 1, 1200, N'1', N'Checked In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (1, 2, 1350, N'2', N'Checked In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (1, 3, 1100, N'3', N'Not Checked In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (2, 4, 1250, N'1', N'Checked In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (2, 5, 1300, N'2', N'Not Checked In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (3, 6, 1400, N'1', N'Checked In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (3, 7, 1180, N'2', N'Checked In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (4, 8, 1320, N'1', N'Checked In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (4, 9, 1150, N'2', N'Not Checked In')
INSERT [dbo].[ContestParticipants] ([contestId], [birdId], [ELO], [participantNo], [checkInStatus]) VALUES (5, 10, 1280, N'1', N'Checked In')
GO
SET IDENTITY_INSERT [dbo].[ContestScore] ON 

INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (1, 1, 101, 201, CAST(85.00 AS Decimal(5, 2)), CAST(N'2023-03-01' AS Date), N'Impressive performance by the Robin.')
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (2, 1, 102, 202, CAST(92.00 AS Decimal(5, 2)), CAST(N'2023-03-01' AS Date), N'Excellent display by the Blue Jay.')
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (3, 2, 103, 203, CAST(78.00 AS Decimal(5, 2)), CAST(N'2023-03-05' AS Date), N'The bird showed some unique behaviors.')
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (4, 2, 104, 204, CAST(89.00 AS Decimal(5, 2)), CAST(N'2023-03-05' AS Date), N'Great participation and vocalizations.')
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (5, 3, 105, 205, CAST(94.00 AS Decimal(5, 2)), CAST(N'2023-03-10' AS Date), N'Outstanding plumage and singing.')
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (6, 3, 106, 206, CAST(82.00 AS Decimal(5, 2)), CAST(N'2023-03-10' AS Date), N'Good overall performance in the contest.')
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (7, 4, 107, 207, CAST(90.00 AS Decimal(5, 2)), CAST(N'2023-03-15' AS Date), N'Impressive agility and flight skills.')
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (8, 4, 108, 208, CAST(75.00 AS Decimal(5, 2)), CAST(N'2023-03-15' AS Date), N'The bird was a bit shy during the contest.')
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (9, 5, 109, 209, CAST(88.00 AS Decimal(5, 2)), CAST(N'2023-03-20' AS Date), N'Good participation with beautiful plumage.')
INSERT [dbo].[ContestScore] ([scoreId], [contestId], [birdId], [memberId], [score], [scoreDate], [comment]) VALUES (10, 5, 110, 210, CAST(91.00 AS Decimal(5, 2)), CAST(N'2023-03-20' AS Date), N'Exceptional behavior and interaction with others.')
SET IDENTITY_INSERT [dbo].[ContestScore] OFF
GO
SET IDENTITY_INSERT [dbo].[Feedback] ON 

INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (1, 1, N'Fantastic Bulbul Singing Contest', N'The singing contest was amazing. The Red-whiskered Bulbuls'' performances were outstanding.', CAST(N'2023-11-05T00:00:00.000' AS DateTime), N'Contest', N'Positive')
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (2, 2, N'Great Meeting with Bird Enthusiasts', N'Had a wonderful time at the meeting. Met some passionate bird lovers.', CAST(N'2023-10-15T00:00:00.000' AS DateTime), N'Meeting', N'Positive')
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (3, 3, N'Bulbul Dance-Off Feedback', N'The dance competition was so much fun. The Bulbuls really know how to dance!', CAST(N'2023-11-20T00:00:00.000' AS DateTime), N'Contest', N'Positive')
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (4, 4, N'Informative Birdwatching Meeting', N'The meeting provided valuable insights into birdwatching. Learned a lot!', CAST(N'2023-10-30T00:00:00.000' AS DateTime), N'Meeting', N'Positive')
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (5, 5, N'Nesting Championship', N'The nest-building competition was a bit challenging, but so rewarding!', CAST(N'2023-11-10T00:00:00.000' AS DateTime), N'Contest', N'Positive')
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (6, 6, N'Meeting Feedback', N'The meeting was informative, but it could be improved with more interactive sessions.', CAST(N'2023-10-25T00:00:00.000' AS DateTime), N'Meeting', N'Mixed')
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (7, 7, N'Bulbul Art Contest', N'Enjoyed the art contest. So many creative Bulbul-themed artworks!', CAST(N'2023-11-15T00:00:00.000' AS DateTime), N'Contest', N'Positive')
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (8, 8, N'Singing Contest Disappointment', N'The singing contest didn''t meet my expectations. Some performances were off-key.', CAST(N'2023-11-05T00:00:00.000' AS DateTime), N'Contest', N'Negative')
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (9, 9, N'Birdwatching Meeting Suggestion', N'The meeting was good, but it would be better if it had more practical birdwatching tips.', CAST(N'2023-10-15T00:00:00.000' AS DateTime), N'Meeting', N'Mixed')
INSERT [dbo].[Feedback] ([feedbackId], [userId], [title], [detail], [date], [category], [status]) VALUES (10, 10, N'Plumage Parade Contest', N'The colorful plumage contest was a visual treat. The Bulbuls looked stunning!', CAST(N'2023-11-30T00:00:00.000' AS DateTime), N'Contest', N'Positive')
SET IDENTITY_INSERT [dbo].[Feedback] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldTrip] ON 

INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review]) VALUES (22, N'Nature Exploration Trip', N'Discover the beauty of nature and observe various bird species in their natural habitat.', N'Exploring the forest and wetland areas.', CAST(N'2023-01-15' AS Date), CAST(N'2023-02-01' AS Date), CAST(N'2023-02-03' AS Date), 1, N'Open', CAST(30.00 AS Decimal(10, 2)), 20, N'John Doe', N'Jane Smith', N'Bring your own binoculars and cameras.', N'Excellent trip with diverse bird sightings.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review]) VALUES (23, N'Bird Photography Expedition', N'A trip dedicated to capturing the stunning moments of bird life through photography.', N'Photography workshop and guided birding.', CAST(N'2023-02-28' AS Date), CAST(N'2023-03-15' AS Date), CAST(N'2023-03-17' AS Date), 2, N'Open', CAST(40.00 AS Decimal(10, 2)), 15, N'Emily White', N'Robert Johnson', N'Bring your camera gear and get ready to snap amazing shots.', N'Great experience for photography enthusiasts.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review]) VALUES (24, N'Conservation and Habitat Study', N'An educational field trip focused on bird conservation efforts and studying their natural habitats.', N'Guided study sessions and habitat exploration.', CAST(N'2023-03-20' AS Date), CAST(N'2023-04-10' AS Date), CAST(N'2023-04-12' AS Date), 3, N'Canceled', CAST(25.00 AS Decimal(10, 2)), 10, N'Michael Green', N'Sarah Brown', N'Learn about conservation practices and contribute to bird protection.', N'Unfortunately, canceled due to unforeseen circumstances.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review]) VALUES (25, N'Bird Watching Weekend Retreat', N'A relaxing weekend retreat for bird enthusiasts to enjoy bird watching and connect with nature.', N'Casual bird watching, nature walks, and bird identification sessions.', CAST(N'2023-04-25' AS Date), CAST(N'2023-05-10' AS Date), CAST(N'2023-05-12' AS Date), 4, N'Open', CAST(35.00 AS Decimal(10, 2)), 25, N'Daniel Taylor', N'Olivia Thompson', N'Pack your binoculars and join us for a weekend of birding fun.', N'Highly recommended for a weekend getaway.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review]) VALUES (26, N'Migration Spectacle Tour', N'Witness the incredible phenomenon of bird migration and observe a variety of migratory species.', N'Guided tour along migration routes and observation points.', CAST(N'2023-05-30' AS Date), CAST(N'2023-06-15' AS Date), CAST(N'2023-06-17' AS Date), 5, N'Open', CAST(50.00 AS Decimal(10, 2)), 15, N'William Martin', N'Sophia Davis', N'Experience the awe-inspiring sight of migrating birds.', N'A unique and unforgettable birding experience.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review]) VALUES (27, N'Bird Ringing Workshop', N'Learn the art of bird ringing for scientific research and bird population monitoring.', N'Hands-on workshop on bird ringing techniques.', CAST(N'2023-06-25' AS Date), CAST(N'2023-07-10' AS Date), CAST(N'2023-07-12' AS Date), 6, N'Open', CAST(30.00 AS Decimal(10, 2)), 12, N'Daniel Baker', N'Emma Turner', N'Participate in bird conservation through scientific research.', N'Informative workshop with practical insights.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review]) VALUES (28, N'Night Owl Expedition', N'A nocturnal birding adventure to observe owls and other night-active bird species.', N'Night owl identification and observation.', CAST(N'2023-07-20' AS Date), CAST(N'2023-08-05' AS Date), CAST(N'2023-08-07' AS Date), 7, N'Open', CAST(45.00 AS Decimal(10, 2)), 8, N'Alex Clark', N'Mia Harris', N'Bring a flashlight and explore the world of nocturnal birds.', N'An enchanting experience under the night sky.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review]) VALUES (29, N'Birding and Yoga Retreat', N'Combine the benefits of birding and yoga for a rejuvenating retreat in nature.', N'Guided birding sessions and yoga sessions in scenic locations.', CAST(N'2023-08-25' AS Date), CAST(N'2023-09-10' AS Date), CAST(N'2023-09-12' AS Date), 8, N'Open', CAST(55.00 AS Decimal(10, 2)), 18, N'Sophie Roberts', N'Jack Anderson', N'Connect with birds and nature through yoga practices.', N'A perfect blend of relaxation and birding.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review]) VALUES (30, N'Rare Bird Spotting Expedition', N'Embark on a quest to spot rare and elusive bird species in specific habitats.', N'Expert-led expedition to rare bird habitats.', CAST(N'2023-09-20' AS Date), CAST(N'2023-10-05' AS Date), CAST(N'2023-10-07' AS Date), 9, N'Open', CAST(60.00 AS Decimal(10, 2)), 10, N'Benjamin Turner', N'Ava Miller', N'A unique opportunity to observe rare birds in their natural environments.', N'Experienced guides for an extraordinary birding adventure.')
INSERT [dbo].[FieldTrip] ([tripId], [tripName], [description], [Details], [registrationDeadline], [startDate], [endDate], [locationId], [status], [fee], [numberOfParticipants], [host], [inCharge], [note], [review]) VALUES (31, N'Birding and Sketching Workshop', N'Combine birding with the art of sketching in this creative and educational workshop.', N'Guided birding sessions and sketching tutorials.', CAST(N'2023-10-15' AS Date), CAST(N'2023-11-01' AS Date), CAST(N'2023-11-03' AS Date), 10, N'Open', CAST(35.00 AS Decimal(10, 2)), 15, N'Liam Johnson', N'Ella Davis', N'Capture the beauty and intricacies of bird life through sketching.', N'Enhance your artistic skills while enjoying the serenity of nature.')
SET IDENTITY_INSERT [dbo].[FieldTrip] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldtripDaybyDay] ON 

INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (22, 2, 1, N'Arrival and Welcome', N'img001.jpg')
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (23, 3, 2, N'Morning Birding Session', N'img002.jpg')
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (24, 4, 3, N'Nature Trail Exploration', N'img003.jpg')
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (25, 5, 4, N'Bird Watching by the Lakeside', N'img004.jpg')
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (26, 6, 5, N'Evening Campfire and Stories', N'img005.jpg')
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (27, 7, 1, N'Photography Workshop Kick-off', N'img006.jpg')
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (28, 8, 2, N'Guided Birding for Photography', N'img007.jpg')
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (29, 9, 3, N'Sunset Photography Session', N'img008.jpg')
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (30, 10, 4, N'Editing and Review Session', N'img009.jpg')
INSERT [dbo].[FieldtripDaybyDay] ([tripId], [daybyDayID], [day], [description], [pictureId]) VALUES (31, 11, 5, N'Closing Ceremony and Awards', N'img010.jpg')
SET IDENTITY_INSERT [dbo].[FieldtripDaybyDay] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldtripGettingThere] ON 

INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (22, 1, N'Start your journey by boarding the designated bus at the club meeting point.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (23, 2, N'If traveling by car, take the scenic route through the countryside to reach the field trip destination.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (24, 3, N'Consider carpooling with fellow bird enthusiasts for a more eco-friendly travel experience.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (25, 4, N'Arrive at the club meeting point early to ensure a smooth departure for the field trip.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (26, 5, N'For those using public transportation, plan your route in advance and arrive at the nearest bus/train station.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (27, 6, N'Prepare a checklist of essential items to bring on the trip, including binoculars, field guides, and a packed lunch.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (28, 7, N'Coordinate with fellow participants to organize a caravan for a fun and social journey to the field trip location.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (29, 8, N'Utilize the club''s online platform to connect with other participants for shared transportation arrangements.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (30, 9, N'Explore alternative travel options, such as biking or walking, if the field trip destination is nearby.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (31, 10, N'Take advantage of the club''s organized shuttle service for a convenient and comfortable journey to the field trip location.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (22, 11, N'Start your journey by boarding the designated bus at the club meeting point.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (23, 12, N'If traveling by car, take the scenic route through the countryside to reach the field trip destination.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (24, 13, N'Consider carpooling with fellow bird enthusiasts for a more eco-friendly travel experience.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (25, 14, N'Arrive at the club meeting point early to ensure a smooth departure for the field trip.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (26, 15, N'For those using public transportation, plan your route in advance and arrive at the nearest bus/train station.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (27, 16, N'Prepare a checklist of essential items to bring on the trip, including binoculars, field guides, and a packed lunch.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (28, 17, N'Coordinate with fellow participants to organize a caravan for a fun and social journey to the field trip location.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (29, 18, N'Utilize the club''s online platform to connect with other participants for shared transportation arrangements.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (30, 19, N'Explore alternative travel options, such as biking or walking, if the field trip destination is nearby.')
INSERT [dbo].[FieldtripGettingThere] ([tripId], [gettingThereId], [gettingThereText]) VALUES (31, 20, N'Take advantage of the club''s organized shuttle service for a convenient and comfortable journey to the field trip location.')
SET IDENTITY_INSERT [dbo].[FieldtripGettingThere] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldtripInclusions] ON 

INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (22, 1, N'Binoculars', N'High-quality binoculars for clear bird observation.', N'Equipment')
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (23, 2, N'Field Guide', N'A comprehensive field guide for bird identification.', N'Resource')
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (24, 3, N'Snacks and Water', N'Pack snacks and sufficient water for the day.', N'Provision')
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (25, 4, N'Sunscreen', N'Protect yourself from sun exposure during the trip.', N'Safety')
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (26, 5, N'Camera', N'Capture memorable moments and bird sightings.', N'Equipment')
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (27, 6, N'Notebook and Pen', N'Record observations and jot down important details.', N'Resource')
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (28, 7, N'First Aid Kit', N'Basic first aid supplies for emergencies.', N'Safety')
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (29, 8, N'Portable Chair', N'Comfortable seating for extended birdwatching sessions.', N'Equipment')
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (30, 9, N'Appropriate Footwear', N'Wear comfortable and weather-appropriate shoes.', N'Attire')
INSERT [dbo].[FieldtripInclusions] ([tripId], [inclusionId], [title], [inclusionText], [type]) VALUES (31, 10, N'Map of the Area', N'Stay oriented with a detailed map of the field trip location.', N'Resource')
SET IDENTITY_INSERT [dbo].[FieldtripInclusions] OFF
GO
SET IDENTITY_INSERT [dbo].[FieldtripMedia] ON 

INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (1, 22, N'Scenic views from the Red Bulbul Field Trip', N'/fieldtrip_images/fieldtrip_image_1.jpg')
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (2, 22, N'Participants enjoying the nature walk', N'/fieldtrip_images/fieldtrip_image_2.jpg')
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (3, 23, N'Exploring diverse bird habitats during the field trip', N'/fieldtrip_images/fieldtrip_image_3.jpg')
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (4, 23, N'Birdwatching in the early morning', N'/fieldtrip_images/fieldtrip_image_4.jpg')
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (5, 24, N'Learning about bird behavior from experts', N'/fieldtrip_images/fieldtrip_image_5.jpg')
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (6, 24, N'Capturing the beauty of rare bird species', N'/fieldtrip_images/fieldtrip_image_6.jpg')
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (7, 25, N'Field trip attendees observing bird nesting sites', N'/fieldtrip_images/fieldtrip_image_7.jpg')
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (8, 25, N'Bird enthusiasts documenting their findings', N'/fieldtrip_images/fieldtrip_image_8.jpg')
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (9, 26, N'Field trip group exploring a new birding destination', N'/fieldtrip_images/fieldtrip_image_9.jpg')
INSERT [dbo].[FieldtripMedia] ([pictureId], [tripId], [description], [image]) VALUES (10, 27, N'Memorable moments from the field trip', N'/fieldtrip_images/fieldtrip_image_10.jpg')
SET IDENTITY_INSERT [dbo].[FieldtripMedia] OFF
GO
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo]) VALUES (22, N'1', N'1')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo]) VALUES (22, N'2', N'2')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo]) VALUES (22, N'3', N'3')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo]) VALUES (23, N'4', N'1')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo]) VALUES (23, N'5', N'2')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo]) VALUES (24, N'6', N'1')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo]) VALUES (24, N'7', N'2')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo]) VALUES (24, N'8', N'3')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo]) VALUES (25, N'9', N'1')
INSERT [dbo].[FieldTripParticipants] ([tripId], [memberId], [participantNo]) VALUES (26, N'10', N'1')
GO
SET IDENTITY_INSERT [dbo].[FieldtripRates] ON 

INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType], [price]) VALUES (22, 1, N'Regular', CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType], [price]) VALUES (22, 2, N'Member', CAST(100.00 AS Decimal(10, 2)))
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType], [price]) VALUES (23, 3, N'Regular', CAST(150.00 AS Decimal(10, 2)))
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType], [price]) VALUES (23, 4, N'Member', CAST(120.00 AS Decimal(10, 2)))
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType], [price]) VALUES (24, 5, N'Regular', CAST(200.00 AS Decimal(10, 2)))
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType], [price]) VALUES (24, 6, N'Member', CAST(180.00 AS Decimal(10, 2)))
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType], [price]) VALUES (25, 7, N'Regular', CAST(130.00 AS Decimal(10, 2)))
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType], [price]) VALUES (26, 8, N'Regular', CAST(80.00 AS Decimal(10, 2)))
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType], [price]) VALUES (27, 9, N'Regular', CAST(250.00 AS Decimal(10, 2)))
INSERT [dbo].[FieldtripRates] ([tripId], [rateId], [rateType], [price]) VALUES (28, 10, N'Regular', CAST(120.00 AS Decimal(10, 2)))
SET IDENTITY_INSERT [dbo].[FieldtripRates] OFF
GO
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Beautiful Red Bulbul in Flight', 101, N'red_bulbul_flight.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Colorful Plumage of the Red Bulbul', 102, N'plumage_colorful.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Close-up of a Red Bulbul Nest', 103, N'nest_closeup.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Red Bulbul Perched on a Branch', 104, N'perched_on_branch.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Flock of Red Bulbuls in the Morning Sun', 105, N'morning_sun_flock.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Red Bulbul Feeding its Chicks', 106, N'feeding_chicks.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Red Bulbul Preening its Feathers', 107, N'preening_feathers.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Artistic Shot of a Red Bulbul', 108, N'artistic_shot.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Red Bulbul Pair Building a Nest', 109, N'building_nest.jpg')
INSERT [dbo].[Gallery] ([pictureId], [description], [userId], [image]) VALUES (NULL, N'Red Bulbul in its Natural Habitat', 110, N'natural_habitat.jpg')
GO
SET IDENTITY_INSERT [dbo].[Location] ON 

INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (3, N'Central Park,,4,Hanoi', N'Discover the vibrant Red Bulbul amidst the lush greenery of Central Park. Watch as these agile birds flit through the trees, their striking red plumage adding a burst of color to the urban oasis.')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (4, N'Golden Gate Park,,4,Hanoi', N'In the serene surroundings of Golden Gate Park, the Red Bulbul can be spotted darting among the botanical wonders. Their melodious calls harmonize with the peaceful ambiance of this iconic park.')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (5, N'Griffith Park,,,USA', N'Witness the Red Bulbul''s playful antics against the backdrop of Griffith Park''s rugged landscapes. These agile birds are at home in the diverse habitats, adding a touch of nature to the urban setting.')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (6, N'Lincoln Park,,,USA', N'Explore the coastal beauty of Lincoln Park while observing the Red Bulbul in its natural habitat. These birds thrive in the park''s coastal vegetation, creating a delightful spectacle for bird enthusiasts.')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (7, N'Piedmont Park,,,', N'In the heart of Atlanta, Piedmont Park provides a haven for the Red Bulbul. Marvel at their energetic flights and listen to their distinctive calls as they mingle with other bird species in this urban retreat.')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (8, N'Fairmount Park,,,', N'Discover the Red Bulbul''s charm in Fairmount Park, where their vibrant plumage stands out against the park''s scenic landscape. Capture moments of these lively birds as they explore the park''s diverse environments.')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (9, N'Discovery Park,,,', N'Amidst the natural beauty of Discovery Park, the Red Bulbul showcases its acrobatic maneuvers. Follow their movements as they navigate the park''s woodlands and meadows, adding a touch of elegance to the scenery.')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (10, N'Stanford University Campus,,,', N'Experience the harmony between education and nature at Stanford University Campus, where Red Bulbuls add a touch of biodiversity. Their presence creates a unique blend of academia and avian life.')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (11, N'Everglades National Park,,,', N'Witness the Red Bulbul''s adaptability in the Everglades, where they navigate the diverse ecosystems of wetlands and forests. Their presence adds a splash of color to the Everglades'' rich avian diversity.')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (12, N'South Mountain Park,,,', N'Explore the desert landscapes of South Mountain Park and encounter the Red Bulbul, a resilient bird thriving in arid environments. Marvel at their resilience and enjoy the unique juxtaposition of desert flora and avian life.')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (13, N'Red Bulbul Bird Club Headquarters,,,', N'Main office for club administration and events')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (14, N'Forest Reserve Park,,,', N'A designated area within the park for birdwatching activities')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (15, N'Wildlife Conservation Center,,,', N'Club''s collaboration site for conservation efforts')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (16, N'Photography Workshop Venue,,,', N'Location for photography workshops focused on bird documentation')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (17, N'Nature Education Center,,,', N'Club''s outreach and education center for nature enthusiasts')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (18, N'Bulbul Sanctuary,,,', N'Dedicated sanctuary for Red Bulbuls and birdwatching')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (19, N'Community Garden,,3,Ho Chi Minh City', N'Club''s initiative to create a bird-friendly garden')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (20, N'Hiking Trail Point,,4,Da nang', N'Start/end point for birdwatching hikes in the local area')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (21, N'Avian Research Station,,,', N'Location for club members involved in bird research projects')
INSERT [dbo].[Location] ([locationId], [locationName], [description]) VALUES (22, N'Bulbul Bird Club Store,,,', N'Club store for birdwatching equipment and merchandise')
SET IDENTITY_INSERT [dbo].[Location] OFF
GO
SET IDENTITY_INSERT [dbo].[Meeting] ON 

INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [note]) VALUES (1, N'Bulbul Enthusiasts Gathering', N'A gathering of Red-whiskered Bulbul enthusiasts to discuss birdwatching and conservation.', CAST(N'2023-10-10T00:00:00.000' AS DateTime), CAST(N'2023-11-01T00:00:00.000' AS DateTime), CAST(N'2023-11-02T00:00:00.000' AS DateTime), 50, N'Bulbul Lovers Club', N'Alice Johnson', N'Don''t forget to bring your binoculars!')
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [note]) VALUES (2, N'Avian Health Seminar', N'Learn about the health and well-being of Red-whiskered Bulbuls from experts in avian medicine.', CAST(N'2023-10-15T00:00:00.000' AS DateTime), CAST(N'2023-11-05T00:00:00.000' AS DateTime), CAST(N'2023-11-05T00:00:00.000' AS DateTime), 30, N'Bulbul Care Society', N'Dr. Maria Gonzalez', N'Get your bird health questions answered.')
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [note]) VALUES (3, N'Feathered Friends Meetup', N'Casual meet and greet with fellow bird enthusiasts. Share your bird stories!', CAST(N'2023-11-05T00:00:00.000' AS DateTime), CAST(N'2023-12-01T00:00:00.000' AS DateTime), CAST(N'2023-12-01T00:00:00.000' AS DateTime), 100, N'Bird Lovers Club', N'Emily Wilson', N'A relaxed evening with bird lovers.')
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [note]) VALUES (4, N'Bulbul Conservation Symposium', N'Join experts to discuss Red-whiskered Bulbul conservation efforts and strategies.', CAST(N'2023-10-20T00:00:00.000' AS DateTime), CAST(N'2023-11-15T00:00:00.000' AS DateTime), CAST(N'2023-11-15T00:00:00.000' AS DateTime), 75, N'Bulbul Conservation Society', N'Dr. James Parker', N'Let''s protect our feathered friends.')
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [note]) VALUES (5, N'Songbird Choir Practice', N'Practice and perform beautiful bird songs with fellow enthusiasts.', CAST(N'2023-10-25T00:00:00.000' AS DateTime), CAST(N'2023-11-10T00:00:00.000' AS DateTime), CAST(N'2023-11-15T00:00:00.000' AS DateTime), 40, N'Choir of Songbirds', N'Sarah Smith', N'Get ready to harmonize with nature!')
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [note]) VALUES (6, N'Feeding Bulbuls Workshop', N'Learn the art of feeding and attracting Red-whiskered Bulbuls to your garden.', CAST(N'2023-11-01T00:00:00.000' AS DateTime), CAST(N'2023-12-10T00:00:00.000' AS DateTime), CAST(N'2023-12-10T00:00:00.000' AS DateTime), 60, N'Bird Feeding Experts', N'John Doe', N'Discover the right bird feed.')
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [note]) VALUES (7, N'Nesting Strategies Discussion', N'Explore nesting behaviors and strategies of Red-whiskered Bulbuls.', CAST(N'2023-11-10T00:00:00.000' AS DateTime), CAST(N'2023-12-05T00:00:00.000' AS DateTime), CAST(N'2023-12-05T00:00:00.000' AS DateTime), 35, N'Nesting Experts', N'Dr. Linda Martin', N'Become a bird nesting expert.')
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [note]) VALUES (8, N'Bird Photography Workshop', N'Capture the beauty of Red-whiskered Bulbuls with a hands-on photography workshop.', CAST(N'2023-10-30T00:00:00.000' AS DateTime), CAST(N'2023-11-25T00:00:00.000' AS DateTime), CAST(N'2023-11-25T00:00:00.000' AS DateTime), 25, N'Photography Enthusiasts', N'Alex Davis', N'Bring your cameras and lenses!')
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [note]) VALUES (9, N'Bulbul Behavior Analysis', N'Learn about the behaviors, calls, and movements of Red-whiskered Bulbuls.', CAST(N'2023-11-15T00:00:00.000' AS DateTime), CAST(N'2023-12-15T00:00:00.000' AS DateTime), CAST(N'2023-12-15T00:00:00.000' AS DateTime), 45, N'Behavior Analysts', N'Prof. Jane Smith', N'Decode bird language and actions.')
INSERT [dbo].[Meeting] ([meetingId], [meetingName], [description], [registrationDeadline], [startDate], [endDate], [numberOfParticipants], [host], [incharge], [note]) VALUES (10, N'Bulbul Art Exhibition', N'Showcase and appreciate artistic representations of Red-whiskered Bulbuls.', CAST(N'2023-11-20T00:00:00.000' AS DateTime), CAST(N'2023-12-20T00:00:00.000' AS DateTime), CAST(N'2023-12-20T00:00:00.000' AS DateTime), 70, N'Artists'' Guild', N'Mark Johnson', N'Celebrate avian artistry at its best.')
SET IDENTITY_INSERT [dbo].[Meeting] OFF
GO
SET IDENTITY_INSERT [dbo].[MeetingMedia] ON 

INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (1, 1, N'Memorable moments from the Red Bulbul Club Meeting', N'/meeting_images/meeting_image_1.jpg')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (2, 1, N'Guest speaker sharing insights about bird conservation', N'/meeting_images/meeting_image_2.jpg')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (3, 2, N'Members engaged in a lively discussion', N'/meeting_images/meeting_image_3.jpg')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (4, 2, N'Exploring new ideas for the bird club', N'/meeting_images/meeting_image_4.jpg')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (5, 3, N'Highlights from the annual meeting', N'/meeting_images/meeting_image_5.jpg')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (6, 3, N'Club members sharing their experiences', N'/meeting_images/meeting_image_6.jpg')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (7, 4, N'Bird enthusiasts showcasing their latest findings', N'/meeting_images/meeting_image_7.jpg')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (8, 4, N'Learning about rare bird species', N'/meeting_images/meeting_image_8.jpg')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (9, 5, N'Interactive session on bird photography techniques', N'/meeting_images/meeting_image_9.jpg')
INSERT [dbo].[MeetingMedia] ([pictureId], [meetingId], [description], [image]) VALUES (10, 5, N'Networking and socializing at the club meeting', N'/meeting_images/meeting_image_10.jpg')
SET IDENTITY_INSERT [dbo].[MeetingMedia] OFF
GO
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo]) VALUES (1, N'1', N'1')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo]) VALUES (1, N'2', N'2')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo]) VALUES (1, N'3', N'3')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo]) VALUES (2, N'4', N'1')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo]) VALUES (2, N'5', N'2')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo]) VALUES (3, N'6', N'1')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo]) VALUES (3, N'7', N'2')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo]) VALUES (4, N'8', N'1')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo]) VALUES (4, N'9', N'2')
INSERT [dbo].[MeetingParticipant] ([meetingId], [memberId], [participantNo]) VALUES (5, N'10', N'1')
GO
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'1', N'John Doe', N'john_doe', N'john.doe@email.com', N'Male', N'Observer', N'123 Main St, City', N'555-1234', N'Passionate birdwatcher', N'Active', 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'10', N'Sophia Davis', N'sophia_d', N'sophia.d@email.com', N'Female', N'Conservationist', N'777 Oak Ave, City', N'555-3456', N'Wildlife advocate', N'Active', 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'2', N'Jane Smith', N'jane_smith', N'jane.smith@email.com', N'Female', N'Photographer', N'456 Oak Ave, Town', N'555-5678', N'Nature photographer', N'Active', 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'3', N'Robert Johnson', N'robert_j', N'robert.j@email.com', N'Male', N'Researcher', N'789 Pine Blvd, Village', N'555-9876', N'Avian researcher', N'Active', 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'4', N'Emily White', N'emily_white', N'emily.white@email.com', N'Female', N'Educator', N'101 Cedar St, City', N'555-5432', N'Nature educator', N'Active', 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'5', N'Michael Green', N'michael_g', N'michael.g@email.com', N'Male', N'Conservationist', N'222 Elm St, Town', N'555-1122', N'Wildlife conservationist', N'Active', 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'6', N'Sarah Black', N'sarah_b', N'sarah.b@email.com', N'Female', N'Hiker', N'333 Oak Ave, Village', N'555-3344', N'Nature enthusiast', N'Inactive', 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'7', N'Daniel Brown', N'daniel_b', N'daniel.b@email.com', N'Male', N'Observer', N'444 Pine Blvd, City', N'555-8765', N'Birdwatching enthusiast', N'Active', 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'8', N'Olivia Taylor', N'olivia_t', N'olivia.t@email.com', N'Female', N'Photographer', N'555 Cedar St, Town', N'555-7890', N'Bird photographer', N'Active', 1)
INSERT [dbo].[Member] ([memberId], [fullName], [userName], [email], [gender], [role], [address], [phone], [description], [status], [clubId]) VALUES (N'9', N'William Miller', N'william_m', N'william.m@email.com', N'Male', N'Researcher', N'666 Elm St, Village', N'555-6543', N'Ornithologist', N'Active', 1)
GO
SET IDENTITY_INSERT [dbo].[News] ON 

INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (1, N'New Bird Species Discovered', N'Discovery', N'Exciting news about a new bird species found in our region.', CAST(N'2023-01-15T00:00:00.000' AS DateTime), N'Published', N'image1.jpg', N'document1.pdf', 101)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (2, N'Upcoming Bird Watching Event', N'Event', N'Join us for a bird watching event on the upcoming weekend.', CAST(N'2023-02-10T00:00:00.000' AS DateTime), N'Published', N'image2.jpg', N'document2.pdf', 102)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (3, N'Conservation Success Story', N'Conservation', N'Our efforts in bird conservation have shown positive results.', CAST(N'2023-03-25T00:00:00.000' AS DateTime), N'Draft', N'image3.jpg', N'document3.pdf', 103)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (4, N'Bird Photography Contest Winners', N'Contest', N'Announcing the winners of our recent bird photography contest.', CAST(N'2023-04-05T00:00:00.000' AS DateTime), N'Published', N'image4.jpg', N'document4.pdf', 104)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (5, N'Important Bird Migration Update', N'Migration', N'Stay informed about the latest bird migration patterns in our region.', CAST(N'2023-05-20T00:00:00.000' AS DateTime), N'Published', N'image5.jpg', N'document5.pdf', 105)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (6, N'Volunteer Opportunities', N'Volunteer', N'Get involved in bird-related conservation projects. Volunteer with us!', CAST(N'2023-06-15T00:00:00.000' AS DateTime), N'Published', N'image6.jpg', N'document6.pdf', 106)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (7, N'Bird Club Achievements', N'Achievement', N'Celebrating the achievements and milestones of our bird club members.', CAST(N'2023-07-02T00:00:00.000' AS DateTime), N'Published', N'image7.jpg', N'document7.pdf', 107)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (8, N'Educational Workshop on Bird Identification', N'Workshop', N'Learn the basics of bird identification in our upcoming workshop.', CAST(N'2023-08-15T00:00:00.000' AS DateTime), N'Draft', N'image8.jpg', N'document8.pdf', 108)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (9, N'Bird Club Newsletter - Summer Edition', N'Newsletter', N'Stay updated with the latest news and events in our summer newsletter.', CAST(N'2023-09-10T00:00:00.000' AS DateTime), N'Published', N'image9.jpg', N'document9.pdf', 109)
INSERT [dbo].[News] ([newsId], [title], [category], [newsContent], [uploadDate], [status], [picture], [filepdf], [userId]) VALUES (10, N'Bird Watching Tips for Beginners', N'Tips', N'New to bird watching? Check out our tips for beginners to get started.', CAST(N'2023-10-01T00:00:00.000' AS DateTime), N'Published', N'image10.jpg', N'document10.pdf', 110)
SET IDENTITY_INSERT [dbo].[News] OFF
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 

INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (1, 101, N'Membership Renewal', CAST(50.00 AS Decimal(10, 2)), CAST(N'2023-01-01' AS Date), CAST(N'2023-01-02' AS Date), N'Completed', N'DOC001')
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (2, 102, N'Event Registration', CAST(20.00 AS Decimal(10, 2)), CAST(N'2023-02-15' AS Date), CAST(N'2023-02-16' AS Date), N'Completed', N'DOC002')
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (3, 103, N'Donation', CAST(30.00 AS Decimal(10, 2)), CAST(N'2023-03-10' AS Date), CAST(N'2023-03-11' AS Date), N'Pending', N'DOC003')
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (4, 104, N'Membership Renewal', CAST(50.00 AS Decimal(10, 2)), CAST(N'2023-04-05' AS Date), CAST(N'2023-04-06' AS Date), N'Completed', N'DOC004')
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (5, 105, N'Event Registration', CAST(25.00 AS Decimal(10, 2)), CAST(N'2023-05-20' AS Date), CAST(N'2023-05-21' AS Date), N'Completed', N'DOC005')
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (6, 106, N'Donation', CAST(40.00 AS Decimal(10, 2)), CAST(N'2023-06-15' AS Date), CAST(N'2023-06-16' AS Date), N'Completed', N'DOC006')
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (7, 107, N'Membership Renewal', CAST(50.00 AS Decimal(10, 2)), CAST(N'2023-07-01' AS Date), CAST(N'2023-07-02' AS Date), N'Pending', N'DOC007')
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (8, 108, N'Event Registration', CAST(30.00 AS Decimal(10, 2)), CAST(N'2023-08-10' AS Date), CAST(N'2023-08-11' AS Date), N'Completed', N'DOC008')
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (9, 109, N'Donation', CAST(35.00 AS Decimal(10, 2)), CAST(N'2023-09-05' AS Date), CAST(N'2023-09-06' AS Date), N'Pending', N'DOC009')
INSERT [dbo].[Transactions] ([transactionId], [userId], [transactionType], [value], [paymentDate], [transactionDate], [status], [docNo]) VALUES (10, 110, N'Membership Renewal', CAST(50.00 AS Decimal(10, 2)), CAST(N'2023-10-20' AS Date), CAST(N'2023-10-21' AS Date), N'Completed', N'DOC010')
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
INSERT [dbo].[Users] ([userId], [clubId], [memberId], [userName], [password], [role]) VALUES (1, 1, N'1', N'JohnDoe', N'password123', N'Manager')
INSERT [dbo].[Users] ([userId], [clubId], [memberId], [userName], [password], [role]) VALUES (2, 1, N'2', N'Adminj', N'securepass', N'Admin')
INSERT [dbo].[Users] ([userId], [clubId], [memberId], [userName], [password], [role]) VALUES (3, 2, N'3', N'RobertJo', N'pass123', N'Manager')
INSERT [dbo].[Users] ([userId], [clubId], [memberId], [userName], [password], [role]) VALUES (4, 3, N'4', N'EmilyWhite', N'secretword', N'Staff')
INSERT [dbo].[Users] ([userId], [clubId], [memberId], [userName], [password], [role]) VALUES (5, 4, N'5', N'MichaelGreen', N'myp@ssw0rd', N'Staff')
INSERT [dbo].[Users] ([userId], [clubId], [memberId], [userName], [password], [role]) VALUES (6, 5, N'6', N'Sarah', N'strongpassword', N'Member')
INSERT [dbo].[Users] ([userId], [clubId], [memberId], [userName], [password], [role]) VALUES (7, 6, N'7', N'Dandelion', N'mypass123', N'Member')
INSERT [dbo].[Users] ([userId], [clubId], [memberId], [userName], [password], [role]) VALUES (8, 7, N'8', N'OliStaff', N'letmein', N'Staff')
INSERT [dbo].[Users] ([userId], [clubId], [memberId], [userName], [password], [role]) VALUES (9, 8, N'9', N'WilliamMil', N'password456', N'Member')
INSERT [dbo].[Users] ([userId], [clubId], [memberId], [userName], [password], [role]) VALUES (10, 9, N'10', N'SopiSmith', N'secure123', N'Member')
GO