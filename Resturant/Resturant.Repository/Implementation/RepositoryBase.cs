using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TMDoyle.Repository.Interface;
using static TMDoyle.Common.ENUMS;

namespace TMDoyle.Repository.Implementation
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Private members

        internal ResturantEntities context;
        private readonly DbSet<TEntity> _dbSet;
        private const bool ShareContext = false;

        #endregion

        #region Constructors

        public RepositoryBase()
        {
            context = new ResturantEntities();

        }
        public RepositoryBase(ResturantEntities context)
        {
            this.context = context;
            _dbSet = context.Set<TEntity>();
        }

        #endregion

        #region Protected properties

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return context.Set<TEntity>();
            }
        }

        #endregion

        #region Public methods

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return orderBy(query).AsEnumerable();
            }
            return query.AsEnumerable();
        }

        public virtual IEnumerable<TEntity> GetOrderBy(
            Expression<Func<TEntity, bool>> filter = null,
            Expression<Func<TEntity, bool>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return query.OrderBy(orderBy);
            }
            return query.AsEnumerable();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return _dbSet.SqlQuery(query, parameters).ToList();
        }

        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == System.Data.Entity.EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if (!Exists(entityToUpdate))
            {
                _dbSet.Attach(entityToUpdate);
            }

            context.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;

        }
        public virtual void Update(TEntity oldEntity, TEntity entityToUpdate)
        {
            if (!Exists(entityToUpdate))
            {
                _dbSet.Attach(entityToUpdate);
            }
            context.Entry(oldEntity).CurrentValues.SetValues(entityToUpdate);
        }
        public virtual void DetachAndUpdate(DbContext ctxt, TEntity originalEntity, TEntity entityToUpdate)
        {
            var objectContext = ((IObjectContextAdapter)ctxt).ObjectContext;
            var objSet = objectContext.CreateObjectSet<TEntity>();
            var entityKey = objectContext.CreateEntityKey(objSet.EntitySet.Name, originalEntity);

            Object foundEntity;
            var exists = objectContext.TryGetObjectByKey(entityKey, out foundEntity);

            if (!exists)
            {
                ctxt.Set<TEntity>().Attach(entityToUpdate);
                ctxt.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
            }
            else
            {
                objectContext.Detach(foundEntity);
                ctxt.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
            }

        }

        public virtual Boolean Exists(TEntity entity)
        {
            var objContext = ((IObjectContextAdapter)context).ObjectContext;
            var objSet = objContext.CreateObjectSet<TEntity>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
            // TryGetObjectByKey attaches a found entity
            // Detach it here to prevent side-effects
            if (exists)
            {
                objContext.Detach(foundEntity);
            }

            return (exists);
        }

        public IEnumerable<TEntity> All()
        {
            return DbSet.AsEnumerable<TEntity>().ToList();
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable();
        }

        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var resetSet = filter != null ? DbSet.Where(filter).AsQueryable() : DbSet.AsQueryable();
            resetSet = skipCount == 0 ? resetSet.Take(size) : resetSet.Skip(skipCount).Take(size);
            total = resetSet.Count();
            return resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0;
        }

        public TEntity Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }

        public int Count
        {
            get
            {
                return DbSet.Count();
            }
        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }

        public TEntity Create(TEntity tEntity)
        {
            var newEntry = DbSet.Add(tEntity);
            if (ShareContext)
                context.SaveChanges();
            return newEntry;
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var objects = Filter(predicate);
            foreach (var obj in objects)
                DbSet.Remove(obj);
            if (ShareContext)
                return context.SaveChanges();
            return 0;
        }

        //to run stored procedre
        public int ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return context.Database.ExecuteSqlCommand(query, parameters);

        }

        //to read from stored procedure
        public IEnumerable<TEntity> ExecReadWithStoreProcedure(string query, params object[] parameters)
        {
            return context.Database.SqlQuery<TEntity>(query, parameters).ToList();
        }



        //public DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters)
        //{
        //    return context.Database.SqlQuery<T>(sql, parameters);
        //}

        #endregion
    }
}
