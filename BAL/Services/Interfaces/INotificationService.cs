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
        Task<bool> Create(int id, NotificationViewModel notifModel);
        Task<bool> UpdateAllNotificationStatus(List<NotificationViewModel> listNotif);
        Task<int> GetCountUnreadNotificationsByMemberId(string id);
        Task<bool> GetBoolNotificationId(int id);
    }
}
