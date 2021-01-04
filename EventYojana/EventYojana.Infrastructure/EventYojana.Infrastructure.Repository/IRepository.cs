using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.Infrastructure.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();

        Task<List<T>> AllAsync();

        IQueryable<T> FindBy(Expression<Func<T, bool>> expression);

        Task<List<T>> FindByAsync(Expression<Func<T, bool>> expression);

        T GetById(int id);

        T GetById(Guid id);

        Task<T> GetByIdAsync(int id);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string orderBy = null, char orderDir = 'A', string includeProperties = "", int? numberOfRecordsInList = null, int? skip = null);

        IQueryable<T> GetQuery(Expression<Func<T, bool>> filter = null, string orderBy = null, char orderDir = 'A', string includeProperties = "", int? numberOfRecordsInList = null, int? skip = null);

        void Add(T entity);

        Task AddAsync(T entity);

        void AddRange(IEnumerable<T> entities);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);

        void UpdateRange(IEnumerable<T> entities);

        void UpdateRange(IEnumerable<T> entities, Dictionary<string, object> properties);

        void Delete(T entity);

        void Delete(int id);

        void DeleteRange(IEnumerable<T> entities);

        void DeleteRange(IEnumerable<T> entities, Dictionary<string, object> properties);

        List<T> Include(Expression<Func<T, object>> expression);

        List<T> Include(Expression<Func<T, object>> expression, Expression<Func<T, bool>> whereExpression);

        T SingleOrDefault(Expression<Func<T, bool>> expression);

        T FirstOrDefault(Expression<Func<T, bool>> expression);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);

        T LastOrDefault(Expression<Func<T, bool>> expression);

        void AddRelatedEntity(T item, IEnumerable<object> updatedSet1, IEnumerable<object> updatedSet2 = null);

        void UpdateRelatedEntity(T item, IEnumerable<object> updatedSet1, IEnumerable<object> updatedSet2 = null);

        void Detach(T entity);

        SpOut ExecuteSp(string spName, SqlParameter[] InSqlParameters = null, SqlParameter[] OutSqlParameters = null);

        IEnumerable<Type> ExecuteSpToType<Type>(string SpName, SqlParameter[] InSqlParameters = null);

        SpOutDs ExecuteSpForDS(string spName, SqlParameter[] InSqlParameters = null, SqlParameter[] OutSqlParameters = null);

        Task<bool> ExecuteNonQuerySp(string spName, SqlParameter[] InSqlParameters = null);

        Type ExecuteSpForSongleResult<Type>(string spName, SqlParameter[] InSqlParameters = null);

        BoolValue SqlBulkCopy(DataTable writeData, string destinationTable, int timeOut);

        void SaveDocument(DataTable xmlData, string xmlPath, string destinationTableName, bool isFireTrigger);

        IQueryable<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties);

        IQueryable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);

        Task Truncate();

        ICollection<T> GetCacheList(Expression<Func<T, bool>> where = null);

        Task<ICollection<T>> GetCacheListAsync(Expression<Func<T, bool>> where = null);
    }
}
