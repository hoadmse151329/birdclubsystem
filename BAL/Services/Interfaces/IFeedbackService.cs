using BAL.ViewModels;
using BAL.ViewModels.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<List<GetFeedbackResponse>> GetAllFeedbacks();
        Task<FeedbackViewModel?> GetFeedbackById(int id);
        void Create(FeedbackViewModel feedback);
        Task<int> CountFeedback();
    }
}
