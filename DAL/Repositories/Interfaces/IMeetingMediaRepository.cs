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
        Task<MeetingMedia> GetMeetingMediaById(int pictureId);
        Task<MeetingMedia> GetMeetingMediaByIdTracking(int pictureId);
        Task<IEnumerable<MeetingMedia>> GetAllMeetingMediasByMeetingId(int meetingId);
    }
}
