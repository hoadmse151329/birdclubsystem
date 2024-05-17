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

        public void Create(NotificationViewModel notifModel)
        {
            var notif = _mapper.Map<Notification>(notifModel);
            _unitOfWork.NotificationRepository.Create(notif);
            _unitOfWork.Save();
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

        public async Task<NotificationViewModel?> GetNotificationById(string id)
        {
            var notif = await _unitOfWork.NotificationRepository.GetNotificationById(id);
            if (notif != null)
            {
                var notification = _mapper.Map<NotificationViewModel>(notif);
                return notification;
            }
            return null;
        }

        public async Task<IEnumerable<string?>?> GetUnreadNotificationTitle(string id)
        {
            var notif = await _unitOfWork.NotificationRepository.GetUnreadNotificationTitle(id);
            if (notif != null) return notif;
            return null;
        }
    }
}
