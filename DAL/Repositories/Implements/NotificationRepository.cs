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
        public async Task<IEnumerable<Notification>> GetAllNotificationsByMemberId(string id)
        {
            return await _context.Notifications.AsNoTracking().Where(n => n.UserDetails.MemberId == id).ToListAsync();
        }

        public async Task<IEnumerable<Notification>> UpdateAllNotificationStatus(List<Notification> notif)
        {
            foreach (var notification in notif)
            {
                var usrnotif = await _context.Notifications
                    .SingleOrDefaultAsync(n => n.UserId == notification.UserId && n.NotificationId == notification.NotificationId);
                if (usrnotif != null)
                {
                    if (usrnotif.Status != "Read")
                    {
                        usrnotif.Status = "Read";
                        _context.Update(usrnotif);
                    }
                }
            }
            return notif;
        }

        public async Task<int> GetCountUnreadNotificationsByMemberId(string id)
        {
            return await _context.Notifications.AsNoTracking().CountAsync(n => n.UserDetails.MemberId == id && n.Status == "Unread");
        }

        public async Task<bool> GetBoolNotificationId(string id)
        {
            var notif = await _context.Notifications.SingleOrDefaultAsync(n => n.NotificationId.Equals(id));
            if (notif != null) return true;
            else return false;
        }

        public async Task<Notification?> GetNotificationById(string id)
        {
            return await _context.Notifications.AsNoTracking().SingleOrDefaultAsync(n => n.NotificationId == id);
        }

        public async Task<IEnumerable<string?>> GetUnreadNotificationTitle(string id)
        {
            return await _context.Notifications.AsNoTracking()
                .Where(n => n.UserDetails.MemberId == id && n.Status == "Unread")
                .OrderByDescending(n => n.Date)
                .Select(n => n.Title).ToListAsync();
        }

        public async Task<IEnumerable<string?>> GetReadNotificationTitle(string id)
        {
            return await _context.Notifications.AsNoTracking()
                .Where(n => n.UserDetails.MemberId == id && n.Status == "Read")
                .OrderByDescending(n => n.Date)
                .Select(n => n.Title).ToListAsync();
        }

        public async Task<string> GenerateNewNotificationId()
        {
            var lastNotif = await _context.Notifications.OrderByDescending(n => Convert.ToInt32(n.NotificationId)).FirstOrDefaultAsync();
            int newId = 1;
            if (lastNotif != null)
            {
                newId = int.Parse(lastNotif.NotificationId) + 1;
            }
            return newId.ToString();
        }
    }
}
