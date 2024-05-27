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

        public void Create(BlogViewModel entity)
        {
            var blog = _mapper.Map<Blog>(entity);
            _unitOfWork.BlogRepository.Create(blog);
            _unitOfWork.Save();
        }

        public async Task<IEnumerable<BlogViewModel>> GetAllBlogs()
        {
            return _mapper.Map<IEnumerable<BlogViewModel>>(await _unitOfWork.BlogRepository.GetAllBlogs());
        }

        public async Task<IEnumerable<BlogViewModel>?> GetAllBlogsByUserId(int usrId)
        {
            return _mapper.Map<IEnumerable<BlogViewModel>?>(await _unitOfWork.BlogRepository.GetAllBlogsByUserId(usrId));
        }

        public Task<BlogViewModel?> GetBlogByIdNoTracking(int blogId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogViewModel>?> GetSortedBlogs(
            string? description, 
            string? category, 
            DateTime? uploadDate, 
            int? vote, 
            List<string>? statuses, 
            string? orderBy, 
            int? userId = null, 
            bool isMemberOrGuest = false)
        {
            return _mapper.Map<IEnumerable<BlogViewModel>?>(await _unitOfWork.BlogRepository.GetSortedBlogs(
                description: description,
                category: category,
                uploadDate: uploadDate,
                vote: vote,
                statuses: statuses,
                orderBy: orderBy,
                userId: userId,
                isMemberOrGuest: isMemberOrGuest));
        }

        public void Update(BlogViewModel entity)
        {
            var blog = _mapper.Map<Blog>(entity);
            _unitOfWork.BlogRepository.Update(blog);
            _unitOfWork.Save();
        }
    }
}
