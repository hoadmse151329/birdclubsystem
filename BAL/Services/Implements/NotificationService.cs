using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<NotificationViewModel>> GetAllNotificationsByUserId(int id)
        {
            return _mapper.Map<IEnumerable<NotificationViewModel>>(await
                _unitOfWork.NotificationRepository.GetAllNotificationsByUserId(id));
        }

        public async Task<bool> Create(int id, NotificationViewModel notifModel)
        {
            var usr = await _unitOfWork.UserRepository.GetByIdNoTracking(id);
            if (usr == null) return false;
            var notif = _mapper.Map<Notification>(notifModel);
            notif.UserId = id;
            _unitOfWork.NotificationRepository.Create(notif);
            _unitOfWork.Save();
            return true;
        }

        public async Task<bool> UpdateAllNotificationStatus(List<NotificationViewModel> listNotif)
        {
            var notif = await _unitOfWork.NotificationRepository.UpdateAllNotificationStatus
                (_mapper.Map<List<Notification>>(listNotif));
            if (notif != null)
            {
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public async Task<int> GetCountUnreadNotificationsByMemberId(string id)
        {
            return await _unitOfWork.NotificationRepository.GetCountUnreadNotificationsByMemberId(id);
        }

        public async Task<bool> GetBoolNotificationId(string id)
        {
            var notif = await _unitOfWork.NotificationRepository.GetBoolNotificationId(id);
            if (!notif) return false;
            return true;
        }
    }
}
