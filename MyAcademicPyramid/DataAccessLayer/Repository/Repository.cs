
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;


namespace DataAccessLayer.Repository
{
    /// <summary>
    /// Generic repository class which provide CRUD method
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T> where T : class,  IEntity
    {
      
        protected DbContext context;
        protected DbSet<T> Dbset;

        // Allow property ID in domain entities  which  can be accessed in this repository class
        public int Id { get; set; }

        /// <summary>
        /// Repository Constructor that accept Dbcontext object 
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.context = context;
            Dbset = context.Set<T>();
        }

        /// <summary>
        /// Insert method with generic type
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(T entity)
        {
            //Insert Exception
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            context.Entry(entity).State = System.Data.Entity.EntityState.Added;
        }

        /// <summary>
        /// Delete method with generic type
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            //Delete Exception
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            // Search by ID, then remove the element
            context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            
        }

        /// <summary>
        /// Update method with generic type
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            //Update Exception
            if (entity == null)
            {
                throw new ArgumentNullException("entity.");
            }
    
            context.Entry(entity).State = System.Data.Entity.EntityState.Modified;

        }

        /// <summary>
        /// Get all a elements  in a Dbset method with generic type
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        /// <summary>
        /// Return an element of generic entity - Look up by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // Return entity object by id
        public T GetByID(int id)
        {
            return context.Set<T>().Find(id);
        }

        /// <summary>
        /// Return all elements of generic entity by giving a search condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Where(predicate);
        }

    }
}
