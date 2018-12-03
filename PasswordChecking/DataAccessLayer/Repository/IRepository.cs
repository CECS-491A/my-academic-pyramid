using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IRepository<T> where T:class
    {
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        T GetByID(int id);
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();

    }
}
