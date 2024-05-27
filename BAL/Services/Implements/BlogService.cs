using AutoMapper;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implements
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BlogService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CountBlog()
        {
            return await _unitOfWork.BlogRepository.CountBlog();
        }

        public async Task<IEnumerable<BlogViewModel>> GetAllBlogs()
        {
            return _mapper.Map<IEnumerable<BlogViewModel>>(await _unitOfWork.BlogRepository.GetAllBlogs());
        }

        public async Task<IEnumerable<BlogViewModel>> GetAllBlogsByUserId(int usrId)
        {
            return _mapper.Map<IEnumerable<BlogViewModel>>(await _unitOfWork.BlogRepository.GetAllBlogsByUserId(usrId));
        }
    }
}
