using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebAppMVC.Models.ViewModels
{
    public class ManagerFieldTripIndexVM
    {
        public ManagerFieldTripIndexVM()
        {
            Roads = new List<string>();
            Districts = new List<string>();
            Cities = new List<string>();
            SelectedRoads = new List<string>();
            SelectedDistricts = new List<string>();
            SelectedCities = new List<string>();
            FieldtripList = new();
            CreateFieldtrip = new();
        }

        public List<string> Roads { get; set; }
        public List<string> Districts { get; set; }
        public List<string> Cities { get; set; }

        public List<string> SelectedRoads { get; set; }
        public List<string> SelectedDistricts { get; set; }
        public List<string> SelectedCities { get; set; }
        public List<BAL.ViewModels.FieldTripViewModel> FieldtripList { get; set; }
        public BAL.ViewModels.Manager.CreateNewFieldtripVM CreateFieldtrip { get; set; }

        public void SetCreateFieldtrip(BAL.ViewModels.Manager.CreateNewFieldtripVM createNewFieldtrip, string? hostname, List<SelectListItem> staffnames)
        {
            CreateFieldtrip = createNewFieldtrip != null ? createNewFieldtrip : new();
            CreateFieldtrip.Host = hostname;
            CreateFieldtrip.StaffNames = staffnames;
        }
    }
}
