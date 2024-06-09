namespace WebAppMVC.Models.ViewModels
{
    public class GuestIndexVM
    {

        public WelcomeMessage WelcomeMess { get; set; }
        public List<string> Banners { get; set; }
        public List<Feature> Features { get; set; }
        public AboutUs About { get; set; }
        public List<TeamMember> TeamMembers { get; set; }
        public List<Service> Services { get; set; }
        public Footer FooterBlock { get; set; }
        public List<BAL.ViewModels.MeetingViewModel> Meetings { get; set; }
        public List<BAL.ViewModels.FieldTripViewModel> FieldTrips { get; set; }
        public List<BAL.ViewModels.ContestViewModel> Contests { get; set; }

        public class AboutUs
        {
            public string Heading { get; set; }
            public string Description { get; set; }
            public string ShortName { get; set; }
            public string Founder { get; set; }
            public string Mission { get; set; }
            public string Image { get; set; }
        }
        public class Feature
        {
            public Feature()
            {
                Image = "image";
                Title = "title";
                Description = "description";
            }
            public string Image { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }
        public class WelcomeMessage
        {
            public string Part1 { get; set; }
            public string Part2 { get; set; }
            public string Part3 { get; set; }
        }
        public class TeamMember
        {
            public TeamMember()
            {
                Name = "name";
                Role = "role";
                Experience = "experience";
                Image = "image";
                Github = "github";
            }
            public string Title { get; set; }
            public string Name { get; set; }
            public string Role { get; set; }
            public string Experience { get; set; }
            public string Image { get; set; }
            public string Github { get; set; }
        }
        public class Service
        {
            public string Icon { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }
        public class Footer
        {
            public string Text { get; set; }
            public string ButtonLink { get; set; }
            public string ButtonText {  get; set; }
            public string Copyright {  get; set; }
        }

    }
}
