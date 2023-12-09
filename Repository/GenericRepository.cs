using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly BirdClubContext _context = null;
        protected readonly DbSet<T> _dbSet = null;

        public GenericRepository()
        {
            this._context = new BirdClubContext();
            this._dbSet = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return this._dbSet.ToList();
        }

        public T GetById(int id)
        {
            return this._dbSet.Find(id);
        }

        public void Insert(T obj)
        {
            this._dbSet.Add(obj);
        }

        public void Update(T obj)
        {
            this._dbSet.Attach(obj);
            this._context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            T existing = this._dbSet.Find(id);
            if (existing != null)
            {
                this._dbSet.Remove(existing);
            }
        }

        public void Save()
        {
            this._context.SaveChanges();
        }
    }
}
