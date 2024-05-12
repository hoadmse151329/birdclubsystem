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

        public async Task<MeetingMedia> GetMeetingMediaById(int pictureId)
        {
            return _context.MeetingMedia.AsNoTracking().SingleOrDefault(m => m.PictureId.Equals(pictureId));
        }

        public async Task<MeetingMedia> GetMeetingMediaByIdTracking(int pictureId)
        {
            return _context.MeetingMedia.SingleOrDefault(m => m.PictureId.Equals(pictureId));
        }

        public async Task<IEnumerable<MeetingMedia>> GetAllMeetingMediasByMeetingId(int meetingId)
        {
            return _context.MeetingMedia.AsNoTracking().Where(m => m.MeetingId.Equals(meetingId)).ToList();
        }
    }
}
