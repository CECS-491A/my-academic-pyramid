using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Interface for generic entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T:class
    {
        /// <summary>
        /// Insert an element of generic entity 
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);

        /// <summary>
        /// Delete an element of generic entity 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Update an element of generic entity 
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Return an element by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetByID(int id);

        /// <summary>
        /// Return an element of generic entity by providing a search condition 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);



    }
}
