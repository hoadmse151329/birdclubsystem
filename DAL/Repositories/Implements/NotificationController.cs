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
    }
}
