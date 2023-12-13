using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        IBirdRepository BirdRepository { get; }
        IBirdMediaRepository BirdMediaRepository { get; }
        IBlogRepository BlogRepository { get; }
        ICommentRepository CommentRepository { get; }
        void save();
    }
}
