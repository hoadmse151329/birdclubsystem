using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface INotificationService
    {
        Task<IEnumerable<NotificationViewModel>> GetAllNotificationsByUserId(int id);
        void Create(NotificationViewModel notifModel);
        Task<bool> UpdateAllNotificationStatus(List<NotificationViewModel> listNotif);
        Task<int> GetCountUnreadNotificationsByMemberId(string id);
        Task<bool> GetBoolNotificationId(string id);
        Task<NotificationViewModel?> GetNotificationById(string id);
        Task<IEnumerable<string?>?> GetUnreadNotificationTitle(string id);
    }
}
