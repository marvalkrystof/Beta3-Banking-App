using BankingSystemMVC.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankingSystemMVC.Repositories
{
    /// <summary>
    /// This class is representing the Generic Repository Pattern
    /// </summary>
    /// <typeparam name="T">Object</typeparam>
    public class Repository<T> where T : class
    {
        
        internal BankingSystemDbContext context;
        internal DbSet<T> dbSet;

        public Repository(BankingSystemDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        /// <summary>
        /// Gets an object from the db
        /// </summary>
        /// <param name="filter">Linq Query</param>
        /// <param name="orderBy">Order by asc/dsc</param>
        /// <param name="includeProperties">Includes to not be used by lazy loading</param>
        /// <returns>The objects retrieved</returns>
        public virtual IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                try {
                    return query.ToList();
                }catch(ArgumentException e)
                {
                    Logger.Logger.LogCriticalFailure("The connection to the database could not be established, because of the connection string.");
                }
                catch(SqlException)
                {
                    Logger.Logger.LogCriticalFailure("The connection to the database could not be established.");
                }
                return null;
                
            }
        }

        /// <summary>
        /// Gets all objects from the Db table
        /// </summary>
        /// <returns>The objects</returns>
        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        /// <summary>
        /// Returns the object by id
        /// </summary>
        /// <param name="id">Id of object</param>
        /// <returns>Object</returns>
        public virtual T GetByID(int id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Inserts specific object to the DbSet
        /// </summary>
        /// <param name="entity">Object</param>
        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Deletes object by id from the DbSet
        /// </summary>
        /// <param name="id">Object id</param>
        public virtual void Delete(int id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Deletes specific object from the DbSet
        /// </summary>
        /// <param name="entityToDelete">Object</param>
        public virtual void Delete(T entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Updates the specific object in the DbSet
        /// </summary>
        /// <param name="entityToUpdate">Object</param>
        public virtual void Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }


    }
}
