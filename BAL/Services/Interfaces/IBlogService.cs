using BAL.ViewModels;
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
        Task<IEnumerable<BlogViewModel>> GetAllBlogs();
        Task<IEnumerable<BlogViewModel>> GetAllBlogsByUserId(int usrId);
    }
}
