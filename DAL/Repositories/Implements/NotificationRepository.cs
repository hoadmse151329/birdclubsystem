using DAL.Infrastructure;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implements
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        private readonly BirdClubContext _context;
        public NotificationRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Notification>> GetAllNotificationsByUserId(int id)
        {
            return _context.Notifications.AsNoTracking().Where(n => n.UserId == id).ToList();
        }

        public async Task<IEnumerable<Notification>> UpdateAllNotificationStatus(List<Notification> notif)
        {
            foreach (var notification in notif)
            {
                var usrnotif = _context.Notifications
                    .SingleOrDefault(n => n.UserId == notification.UserId && n.NotificationId == notification.NotificationId);
                if (usrnotif != null)
                {
                    if (usrnotif.Status != notification.Status)
                    {
                        usrnotif.Status = notification.Status;
                        _context.Update(usrnotif);
                    }
                }
            }
            return notif;
        }

        public async Task<int> GetCountUnreadNotificationsByMemberId(string id)
        {
            return _context.Notifications.AsNoTracking().Count(n => n.UserDetail.MemberId == id && n.Status == "Unread");
        }

        public async Task<bool> GetBoolNotificationId(string id)
        {
            var notif = _context.Notifications.SingleOrDefault(n => n.NotificationId.Equals(id));
            if (notif != null) return true;
            else return false;
        }

        public async Task<Notification?> GetNotificationById(string id)
        {
            return _context.Notifications.AsNoTracking().SingleOrDefault(n => n.NotificationId == id);
        }

        public async Task<IEnumerable<string?>> GetUnreadNotificationTitle(string id)
        {
            return _context.Notifications.AsNoTracking()
                .Where(n => n.UserDetail.MemberId == id && n.Status == "Unread")
                .OrderByDescending(n => n.Date)
                .Select(n => n.Title).ToList();
        }
    }
}
