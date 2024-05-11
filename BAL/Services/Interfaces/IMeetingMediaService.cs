using BAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IMeetingMediaService
    {
        Task<bool> Create(int meetingId, MeetingMediaViewModel media);
        Task<bool> Delete(int meetingId, int pictureId);
        Task<bool> Update(int meetingId, MeetingMediaViewModel media);
        Task<MeetingMediaViewModel> GetById(int pictureId);
        Task<IEnumerable<MeetingMediaViewModel>> GetAllByMeetingId(int meetingId);
    }
}
