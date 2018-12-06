
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;


namespace DataAccessLayer.Repository
{
    public class Repository<T> : IRepository<T> where T : class,  IEntity
    {
        // Use a list to simulate a database. We can replace it with a real SQL database later 
        protected DbContext context;
        protected DbSet<T> Dbset;

        // Allow property ID in domain entities  which  can be accessed in this repository class
        public int Id { get; set; }

        // Constructor which accept list of any data type and initialize the context. 
        public Repository(DbContext context)
        {
            this.context = context;
            Dbset = context.Set<T>();
        }

        // Insert an element of generic entity 
        public void Insert(T entity)
        {
            //Insert Exception
            if (entity == null)
            {
                throw new ArgumentNullException("Required input. Input is empty.");
            }
            _context.Add(entity);
        }

        // Delete an element of generic entity 
        public void Delete(T entity)
        {
            //Delete Exception
            if (entity == null)
            {
                throw new ArgumentNullException("Required input. Input is empty.");
            }
            // Search by ID, then remove the element
            context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
            context.SaveChanges();
        }

        // Update an element of generic entity 
        public void Update(T entity)
        {
            //Update Exception
            if (entity == null)
            {
                throw new ArgumentNullException("Required I.D. I.D. is not found.");
            }
            // Find the index of the element by search for the Id 
            int index = _context.FindIndex(e => e.Id == entity.Id);

            //Index Exception
            if (index == null)
            {
                throw new ArgumentNullException("Required Index, Index is found.");
            }

            // Remove the element at the index 
            _context.RemoveAt(index);

            // Add new element to the index
            _context.Add(entity);
        }

        //Return all elements of generic entity
        public IEnumerable<T> GetAll()
        {
            return _context;
        }

        // Return an element by id
        public T GetByID(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Required I.D. I.D. is not found.");
            }
            return _context.SingleOrDefault(e => e.Id == id);
        }

        //Return all elements of generic entity
        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Where(predicate);
        }

    }
}
