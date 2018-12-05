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
        // Use a list to simulate a database. We can replace it with a real SQL database later 
        private List<T> _context;

        // Allow property ID in domain entities  which  can be accessed in this repository class
        public int Id { get; set; }

        // Constructor which accept list of any data type and initialize the context. 
        public Repository(List<T> list)
        {
            _context = list;
        }

        // Insert an element of generic entity 
        public void Insert(T entity)
        {
            _context.Add(entity);
        }

        // Delete an element of generic entity 
        public void Delete(T entity)
        {
            // Search by ID, then remove the element
            _context.RemoveAll(e => e.Id == entity.Id);
        }

        // Update an element of generic entity 
        public void Update(T entity)
        {
            // Find the index of the element by search for the Id 
            int index = _context.FindIndex(e => e.Id == entity.Id);

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
            return _context.SingleOrDefault(e => e.Id == id);
        }

        //Return all elements of generic entity
        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
             return _context.AsQueryable().Where(predicate);
        }

    }
}
