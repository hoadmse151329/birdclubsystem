using DAL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IBlogRepository : IRepositoryBase<Blog>
    {
        Task<IEnumerable<Blog>> GetAllBlogs();
        Task<Blog?> GetBlogByIdNoTracking(int blogId);
        Task<IEnumerable<Blog>?> GetAllBlogsByUserId(int usrId);
        Task<int> CountBlog();
        Task<IEnumerable<Blog>?> GetSortedBlogs(
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
