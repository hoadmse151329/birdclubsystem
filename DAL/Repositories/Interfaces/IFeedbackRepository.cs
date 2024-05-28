using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IFeedbackRepository : IRepositoryBase<Feedback>
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacks();
        Task<Feedback?> GetFeedbackById(int id);
        Task<int> CountFeedback();
    }
}
