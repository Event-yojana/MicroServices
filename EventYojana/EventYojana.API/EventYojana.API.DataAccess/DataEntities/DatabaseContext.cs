using EventYojana.API.DataAccess.Interfaces;
using EventYojana.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.DataAccess.DataEntities
{
    public class DatabaseContext : IDatabaseContext
    {
        private DbContext dbContext { get; set; }

        protected IRepositoryProvider _repositoryProvider { get; set; }

        private IDbContextTransaction dbContextTransaction;

        public DatabaseContext()
        {
            this.CreateDbContext();
        }

        public DatabaseContext(IRepositoryProvider repositoryProvider)
        {
            this.CreateDbContext();
            repositoryProvider.DbContext = this.dbContext;
            _repositoryProvider = repositoryProvider;
        }

        protected void CreateDbContext()
        {
            dbContext = new EventYojanaContext();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            return this._repositoryProvider.Repository<T>();
        }

        public IDbContextTransaction BeginTransaction()
        {
            dbContextTransaction = dbContext.Database.BeginTransaction();
            return dbContextTransaction;
        }

        public void Rollback()
        {
            dbContextTransaction?.Rollback();
        }

        public void CommitTransaction()
        {
            dbContextTransaction?.Commit();
        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }

        public async Task CommitAsync()
        {
            try
            {
                await this.dbContext.SaveChangesAsync();
            }
            catch
            {
                throw null;
            }
        }
    }
}
