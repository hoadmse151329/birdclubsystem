using AutoMapper;
using Azure.Storage.Blobs.Models;
using BAL.Services.Interfaces;
using BAL.ViewModels;
using BAL.ViewModels.Blog;
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
        public void Create(CreateNewBlog entity)
        {
            var usr = _unitOfWork.UserRepository.GetByMemberId(entity.MemberId).Result;
            if (usr != null)
            {
                var blog = _mapper.Map<Blog>(entity);
                blog.UserId = usr.UserId;
                _unitOfWork.BlogRepository.Create(blog);
                _unitOfWork.Save();
            }
        }

        public async Task<IEnumerable<BlogViewModel>> GetAllBlogs()
        {
            var blogList = _mapper.Map<IEnumerable<BlogViewModel>>(await _unitOfWork.BlogRepository.GetAllBlogs());
            foreach (var blog in blogList)
            {
                var blogCommentList = _mapper.Map<List<CommentViewModel>>(await _unitOfWork.CommentRepository.GetAllByBlogId(blog.BlogId.Value));
                if (blogCommentList != null)
                {
                    blog.Comments = blogCommentList;
                }
            }
            return blogList;
        }

        public async Task<IEnumerable<BlogViewModel>?> GetAllBlogsByUserId(int usrId)
        {
            var blogList = _mapper.Map<IEnumerable<BlogViewModel>?>(await _unitOfWork.BlogRepository.GetAllBlogsByUserId(usrId));
            foreach (var blog in blogList)
            {
                var blogCommentList = _mapper.Map<List<CommentViewModel>>(await _unitOfWork.CommentRepository.GetAllByBlogId(blog.BlogId.Value));
                if (blogCommentList != null)
                {
                    blog.Comments = blogCommentList;
                }
            }
            return blogList;
        }

        public async Task<BlogViewModel?> GetBlogByIdNoTracking(int blogId)
        {
            var blog = _mapper.Map<BlogViewModel?>(await _unitOfWork.BlogRepository.GetBlogByIdNoTracking(blogId));
            var blogCommentList = _mapper.Map<List<CommentViewModel>>(await _unitOfWork.CommentRepository.GetAllByBlogId(blog.BlogId.Value));
            if (blogCommentList != null)
            {
                blog.Comments = blogCommentList;
            }
            return blog;
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
            var blogList = _mapper.Map<IEnumerable<BlogViewModel>?>(await _unitOfWork.BlogRepository.GetSortedBlogs(
                description: description,
                category: category,
                uploadDate: uploadDate,
                vote: vote,
                statuses: statuses,
                orderBy: orderBy,
                userId: userId,
                isMemberOrGuest: isMemberOrGuest));
            foreach (var blog in blogList)
            {
                var blogCommentList = _mapper.Map<List<CommentViewModel>>(await _unitOfWork.CommentRepository.GetAllByBlogId(blog.BlogId.Value));
                if (blogCommentList != null)
                {
                    blog.Comments = blogCommentList;
                }
            }
            return blogList;
        }

        public void Update(BlogViewModel entity)
        {
            var blog = _mapper.Map<Blog>(entity);
            _unitOfWork.BlogRepository.Update(blog);
            _unitOfWork.Save();
        }
        public void UpdateStatus(UpdateBlogStatus entity)
        {
            var blog = _unitOfWork.BlogRepository.GetBlogByIdNoTracking(entity.BlogId.Value).Result;
            if(blog != null)
            {
                if(blog.Status != entity.Status)
                {
                    blog.Status = entity.Status;
                    _unitOfWork.BlogRepository.Update(blog);
                    _unitOfWork.Save();
                }
            }
        }
    }
}
