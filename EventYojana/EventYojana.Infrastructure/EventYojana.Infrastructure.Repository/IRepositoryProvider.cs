using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventYojana.Infrastructure.Repository
{
    public interface IRepositoryProvider
    {
        DbContext DbContext { get; set; }

        IRepository<T> Repository<T>() where T : class;

        IRepository<T> Repository<T>(Type type) where T : class;

        T GetRepository<T>(Func<DbContext, object> factory = null) where T : class;

        T GetRepository<T>(Func<DbContext, object> factory, DbContext currentDbContext) where T : class;

        void SetRepositoryFactories(IDictionary<Type, Func<DbContext, object>> factories);

        void SetRepositoryFactories(IDictionary<Type, DbContext> factories);
    }
}
