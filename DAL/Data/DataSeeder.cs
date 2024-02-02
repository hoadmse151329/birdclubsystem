using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace DAL.Data
//{
//    public static class DataSeeder
//    {
//        public static void SeedLocations(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Location>().HasData(
//                new Location { LocationId = 1, LocationName = "H22/183,Hoang Van Thai,Thanh Xuan,Hanoi", Description = "This is a big city, CS 1" },
//                new Location { LocationId = 2, LocationName = "42/6,Ha Huy Tap,P3,Da Lat", Description = "This is a big city" },
//                new Location { LocationId = 3, LocationName = "7,Quang Trung,Hai Chau,Da Nang", Description = "This is a big city" },
//                new Location { LocationId = 4, LocationName = "224,Le Van Viet,9,Ho Chi Minh",Description = "This is a big city, CS 2" },
//                new Location { LocationId = 5, LocationName = "23,Nguyen Dinh Chieu,9,Ho Chi Minh", Description = "This is a big city" }
//                );
//        }
//        public static void SeedMeetings(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Meeting>().HasData(
//                new Meeting { MeetingId = 1, MeetingName = "ChaoMaoClub First Anual Meeting", Description = "Meet up with new members, exchanging experiences and ideas...Fusce dui est, pellentesque a dolor eu, The main focus of the meetings is birding, thus the meeting location is rotated among good birding locations.", RegistrationDeadline = new DateTime(2024,1,10), StartDate = new DateTime(2024,1,15), EndDate = new DateTime(2024,1,16), NumberOfParticipants = 30, Host = "John Connor", Incharge = "James Howard", Note = "Everyone interested in birds of the Ha Noi is welcome at our general meetings, whether members of the Chao Mao Club or not.Specifics about upcoming meetings are provided via the Newsletter sent to all members,and are also provided on this web site.", LocationId = 1 },
//                new Meeting { MeetingId = 2, MeetingName = "ChaoMaoClub Second Anual Meeting", Description = "Meet up with new members, exchanging experiences and ideas...Fusce dui est, pellentesque a dolor eu, The main focus of the meetings is birding, thus the meeting location is rotated among good birding locations.", RegistrationDeadline = new DateTime(2024, 2, 11), StartDate = new DateTime(2024, 2, 16), EndDate = new DateTime(2024, 2, 17), NumberOfParticipants = 20, Host = "Adam Anderson", Incharge = "Adele Holmes", Note = "Everyone interested in birds of the Da Lat City is welcome at our general meetings, whether members of the Chao Mao Club or not.Specifics about upcoming meetings are provided via the Newsletter sent to all members,and are also provided on this web site.", LocationId = 2 },
//                new Meeting { MeetingId = 3, MeetingName = "ThunderBird Club First Meeting", Description = "Meet up with new members, exchanging experiences and ideas...Fusce dui est, pellentesque a dolor eu, The main focus of the meetings is birding, thus the meeting location is rotated among good birding locations.", RegistrationDeadline = new DateTime(2024, 3, 3), StartDate = new DateTime(2024, 3, 9), EndDate = new DateTime(2024, 3, 10), NumberOfParticipants = 10, Host = "Nguyen Van A", Incharge = "Vuong Cam Tu", Note = "Everyone interested in birds of the Da Nang is welcome at our general meetings, whether members of the Chao Mao Club or not.Specifics about upcoming meetings are provided via the Newsletter sent to all members,and are also provided on this web site.", LocationId = 3 },
//                new Meeting { MeetingId = 4, MeetingName = "WindStrike Club First Meeting", Description = "Meet up with new members, exchanging experiences and ideas...Fusce dui est, pellentesque a dolor eu, The main focus of the meetings is birding, thus the meeting location is rotated among good birding locations.", RegistrationDeadline = new DateTime(2024, 3, 5), StartDate = new DateTime(2024, 3, 10), EndDate = new DateTime(2024, 3, 11), NumberOfParticipants = 20, Host = "Nguyen Tri Thien", Incharge = "Ngo Ho Quan", Note = "Everyone interested in birds of the HCM city is welcome at our general meetings, whether members of the Chao Mao Club or not.Specifics about upcoming meetings are provided via the Newsletter sent to all members,and are also provided on this web site.", LocationId = 4 }
//                );
//        }
//        public static void SeedClubInfos(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<ClubInformation>().HasData(
//                new ClubInformation { ClubId = 1, ClubLocationId = 3, Description = "The ThunderBird Roars!!!, ThunderBird Bird No 1 Fan Club in Da Nang city"},
//                new ClubInformation { ClubId = 2, ClubLocationId = 1, Description = "ChaoMao Club Main headquarter"},
//                new ClubInformation { ClubId = 3, ClubLocationId = 4, Description = "WindStrike Bird Fan Club from Ho Chi Minh city"}
//                );
//        }
//        public static void SeedMembers(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Member>().HasData(
//                new Member { MemberId = "1", UserName = "Nguyen Van A", ClubId = 1, FullName = "Nguyen Van A", Email = "anguyenvan@gmail.com.vn", Gender = "male", Role = "Manager", Address = "154/20 Nguyen Kim Street, Ward 6, Dist.10, Ho Chi Minh City", Phone = "0838548850", Description = "yo adsjsdajndsjadna" , Status = "Active"},
//                new Member { MemberId = "2", UserName = "DanEchocraft", ClubId = null, FullName = "Daniel Echocraft", Email = "danecho@gmail.com", Gender = "male", Role = "Member", Address = "334 Huynh Tan Phat Street, District 7, Ho Chi Minh City", Phone = "0838726767", Description = "yo adsjsdajndsjadna", Status = "Active" },
//                new Member { MemberId = "3", UserName = "Admin", ClubId = 2, FullName = "Vuong Hai Quan", Email = "adminquan@chaomaoclub.com.vn", Gender = "male", Role = "Admin", Address = "23 Nguyen Bieu Street, Ba Dinh District, Ha Noi", Phone = "0938329397", Description = "yo adsjsdajndsjadna", Status = "Active" },
//                new Member { MemberId = "4", UserName = "JohnCon", ClubId = 2, FullName = "John Connor", Email = "johncon@gmail.com", Gender = "male", Role = "Manager", Address = "111-E1 Phuong Mai Street, Dong Da District, Ha Noi", Phone = "0938523649", Description = "yo adsjsdajndsjadna", Status = "Active" },
//                new Member { MemberId = "5", UserName = "Vuong Cam Tu", ClubId = 1, FullName = "Vuong Cam Tu", Email = "tuvc@gmail.com", Gender = "female", Role = "Staff", Address = "14 Nguyen Bieu Street, Ba Dinh District, Ha Noi", Phone = "0838548850", Description = "yo adsjsdajndsjadna", Status = "Active" },
//                new Member { MemberId = "6", UserName = "Nguyen Tri Thien", ClubId = 3, FullName = "Nguyen Tri Thien", Email = "thiennguyen132@gmail.com", Gender = "male", Role = "Manager", Address = "28/12, Phan Dinh Giot Street, Ward 2, Dist.Tan Binh, Ho Chi Minh City", Phone = "0938478766", Description = "yo adsjsdajndsjadna", Status = "Active" },
//                new Member { MemberId = "7", UserName = "Ngo Ho Quan", ClubId = 3, FullName = "Ngo Ho Quan", Email = "quanNHo145@gmail.com", Gender = "male", Role = "Staff", Address = "478 Nguyen Thi Minh Khai Street, Ward 2, District 3, Ho Chi Minh City", Phone = "0938353577", Description = "yo adsjsdajndsjadna", Status = "Active" },
//                new Member { MemberId = "8", UserName = "Michael Jordan", ClubId = null, FullName = "Michael Jordan", Email = "justym@gmail.com", Gender = "male", Role = "Member", Address = null, Phone = "01241242141", Description = null, Status = "Active" }
//                );
//        }
//        public static void SeedUsers(this ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<User>().HasData(
//                new User { UserId = 1, ClubId = 1, UserName = "ANV2024", Password = "123", MemberId = "1"},
//                new User { UserId = 2, ClubId = null, UserName = "DanEcho", Password = "123", MemberId = "2"},
//                new User { UserId = 3 , ClubId = 2, UserName = "Admin", Password = "theadmin", MemberId = "3"},
//                new User { UserId = 4, ClubId = 2, UserName = "JConnor", Password = "123", MemberId = "4"},
//                new User { UserId = 5, ClubId = 1, UserName = "TuVC1010", Password = "123", MemberId = "5"},
//                new User { UserId = 6, ClubId = 3, UserName = "ThienNTBirdio", Password = "123", MemberId = "6"},
//                new User { UserId = 7, ClubId = 3, UserName = "QuanNH2024", Password = "123", MemberId = "7"},
//                new User { UserId = 8, ClubId = null, UserName = "JustYourMan", Password = "123", MemberId = "8" }
//                );
//        }
//    }
//}
