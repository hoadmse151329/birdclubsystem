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

        public async Task<int> GetCountUnreadNotificationsByUserId(int id)
        {
            return _context.Notifications.AsNoTracking().Count(n => n.UserId == id && n.Status == "Unread");
        }
    }
}
