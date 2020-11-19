using EventYojana.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.DataAccess.Interfaces
{
    public interface IDatabaseContext
    {
        IRepository<T> Repository<T>() where T : class;

        IDbContextTransaction BeginTransaction();

        void Rollback();

        void CommitTransaction();

        void Commit();

        Task CommitAsync();
    }
}
