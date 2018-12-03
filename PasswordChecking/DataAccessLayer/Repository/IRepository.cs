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
        // Insert an element of generic entity 
        void Insert(T entity);

        // Delete an element of generic entity 
        void Delete(T entity);

        // Update an element of generic entity 
        void Update(T entity);

        // Return an element by id
        T GetByID(int id);

        /// Return an element of generic entity by providing a search condition 
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);

        //Return all elements of generic entity
        IEnumerable<T> GetAll();

    }
}
