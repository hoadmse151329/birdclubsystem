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
        Task<IEnumerable<Blog>> GetAllBlogsByUserId(int usrId);
        Task<int> CountBlog();
    }
}
