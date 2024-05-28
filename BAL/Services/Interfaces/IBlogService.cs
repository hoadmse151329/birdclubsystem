using BAL.ViewModels;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IBlogService
    {
        Task<int> CountBlog();
        Task<BlogViewModel?> GetBlogByIdNoTracking(int blogId);
        void Create(BlogViewModel entity);
        void Update(BlogViewModel entity);
        Task<IEnumerable<BlogViewModel>> GetAllBlogs();
        Task<IEnumerable<BlogViewModel>?> GetAllBlogsByUserId(int usrId);
        Task<IEnumerable<BlogViewModel>?> GetSortedBlogs(
            string? description,
            string? category,
            DateTime? uploadDate,
            int? vote,
            List<string>? statuses,
            string? orderBy,
            int? userId = null,
            bool isMemberOrGuest = false
            );
    }
}
