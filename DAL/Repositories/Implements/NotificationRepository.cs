﻿using DAL.Infrastructure;
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
                var usrnotif = _context.Notifications
                    .SingleOrDefault(n => n.UserId == notification.UserId && n.NotificationId == notification.NotificationId);
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
            return await _context.Notifications.AsNoTracking()
                .Where(n => n.UserDetails.MemberId == id && n.Status == "Unread")
                .OrderByDescending(n => n.Date)
                .Select(n => n.Title).ToList();
        }

        public async Task<IEnumerable<string?>> GetReadNotificationTitle(string id)
        {
            return await _context.Notifications.AsNoTracking()
                .Where(n => n.UserDetails.MemberId == id && n.Status == "Read")
                .OrderByDescending(n => n.Date)
                .Select(n => n.Title).ToList();
        }
    }
}
