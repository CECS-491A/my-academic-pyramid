using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class,  IEntity
    {
        private List<T> _context;
        public int Id { get; set; }

        public Repository(List<T> list)
        {
            _context = list;
        }

        public void Insert(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.RemoveAll(e => e.Id == entity.Id);
        }


        public void Update(T entity)
        {
            int index = _context.FindIndex(e => e.Id == entity.Id);
            _context.RemoveAt(index);
            _context.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _context;
        }

        public T GetByID(int id)
        {
            return _context.SingleOrDefault(e => e.Id == id);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
             return _context.AsQueryable().Where(predicate);
        }

    }
}
