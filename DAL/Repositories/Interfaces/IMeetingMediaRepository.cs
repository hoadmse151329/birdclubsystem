using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IMeetingMediaRepository : IRepositoryBase<MeetingMedia>
    {
        Task<MeetingMedia> GetMeetingMediaById(int meetingId, int pictureId);
        Task<IEnumerable<MeetingMedia>> GetMeetingMediasByMeetingId(int meetingId);
    }
}
