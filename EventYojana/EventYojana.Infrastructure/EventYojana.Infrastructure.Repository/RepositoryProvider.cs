using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace EventYojana.Infrastructure.Repository
{
    public class RepositoryProvider : IRepositoryProvider
    {
        private IDictionary<Type, Func<DbContext, object>> _repositoryFactories;
        private IDictionary<Type, DbContext> contextFactories;
        protected Dictionary<Type, object> Repositories { get; private set; }

        public DbContext DbContext { get; set; }

        public RepositoryProvider()
        {
            this._repositoryFactories = (IDictionary<Type, Func<DbContext, object>>)null;
            this.Repositories = new Dictionary<Type, object>();
            this.contextFactories = (IDictionary<Type, DbContext>)new Dictionary<Type, DbContext>();
        }

        public virtual T GetRepository<T>(Func<DbContext, object> factory = null) where T : class
        {
            object obj;
            this.Repositories.TryGetValue(typeof(T), out obj);
            if (obj != null)
                return (T)obj;
            return this.MakeRepository<T>(factory, this.DbContext);
        }

        public virtual T GetRepository<T>(Func<DbContext, object> factory, DbContext currentDbContext) where T : class
        {
            object obj;
            this.Repositories.TryGetValue(typeof(T), out obj);
            if (obj != null)
                return (T)obj;
            return this.MakeRepository<T>(factory, currentDbContext);

        }

        public IRepository<T> Repository<T>() where T : class
        {
            return this.GetRepository<IRepository<T>>(this.GetRepositoryFactoryForEntityType<T>());
        }

        public IRepository<T> Repository<T>(Type type) where T : class
        {
            DbContext currentDbContext;
            this.contextFactories.TryGetValue(type, out currentDbContext);
            return this.GetRepository<IRepository<T>>(this.GetRepositoryFactoryForEntityType<T>(), currentDbContext);

        }

        public void SetRepositoryFactories(IDictionary<Type, Func<DbContext, object>> factories)
        {
            this._repositoryFactories = factories;
        }

        public void SetRepositoryFactories(IDictionary<Type, DbContext> factories)
        {
            this.contextFactories = factories;
        }

        public Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            if(this._repositoryFactories != null)
            {
                return this.GetRepositoryFactory<T>() ?? this.DefaultEntityRepositoryFactory<T>();
            }
            return this.DefaultEntityRepositoryFactory<T>();
        }

        public Func<DbContext, object> GetRepositoryFactory<T>()
        {
            Func<DbContext, object> func;
            this._repositoryFactories.TryGetValue(typeof(T), out func);
            return func;
        }

        public virtual Func<DbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            return (Func<DbContext, object>)(dbContext => (object)new Repository<T>(dbContext));
        }

        protected virtual T MakeRepository<T>(Func<DbContext, object> factory, DbContext dbContext)
        {
            Func<DbContext, object> func = factory ?? this.GetRepositoryFactory<T>();
            if (func == null)
                throw new NotImplementedException("No factory for repository type, " + typeof(T).FullName);
            T obj = (T)func(dbContext);
            this.Repositories[typeof(T)] = (object)obj;
            return obj;
        }
    }
}
