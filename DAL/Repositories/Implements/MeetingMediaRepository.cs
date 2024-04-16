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
    public class MeetingMediaRepository : RepositoryBase<MeetingMedia>, IMeetingMediaRepository
    {
        private readonly BirdClubContext _context;
        public MeetingMediaRepository(BirdClubContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MeetingMedia> GetMeetingMediaById(int meetingId, int pictureId)
        {
            return _context.MeetingMedia.AsNoTracking().SingleOrDefault(m => m.MeetingId.Equals(meetingId) && m.PictureId.Equals(pictureId));
        }

        public async Task<IEnumerable<MeetingMedia>> GetMeetingMediasByMeetingId(int meetingId)
        {
            return _context.MeetingMedia.AsNoTracking().Where(m => m.MeetingId.Equals(meetingId));
        }
    }
}
