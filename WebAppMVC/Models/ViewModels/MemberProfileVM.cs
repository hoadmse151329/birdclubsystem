﻿namespace WebAppMVC.Models.ViewModels
{
    public class MemberProfileVM
    {
        public MemberProfileVM()
        {
            managerPassword = new BAL.ViewModels.Member.UpdateMemberPassword();
            managerDetail = new BAL.ViewModels.MemberViewModel();
        }

        public BAL.ViewModels.Member.UpdateMemberPassword managerPassword { get; set; }

        public BAL.ViewModels.MemberViewModel managerDetail { get; set; }
    }
}
