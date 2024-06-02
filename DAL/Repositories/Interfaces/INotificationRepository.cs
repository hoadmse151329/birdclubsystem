using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface INotificationRepository : IRepositoryBase<Notification>
    {
        Task<IEnumerable<Notification>> GetAllNotificationsByMemberId(string id);
        Task<IEnumerable<Notification>> UpdateAllNotificationStatus(List<Notification> notif);
        Task<int> GetCountUnreadNotificationsByMemberId(string id);
        Task<bool> GetBoolNotificationId(string id);
        Task<Notification?> GetNotificationById(string id);
        Task<IEnumerable<string?>> GetUnreadNotificationTitle(string id);
        Task<IEnumerable<string?>> GetReadNotificationTitle(string id);
        Task<string> GenerateNewNotificationId();
    }
}
