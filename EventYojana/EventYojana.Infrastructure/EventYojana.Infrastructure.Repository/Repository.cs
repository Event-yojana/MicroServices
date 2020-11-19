using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EventYojana.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext DbContext { get; set; }

        protected DbSet<T> DbSet { get; set; }

        public Repository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("DbContext");
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<T>();
        }
        public virtual void Add(T entity)
        {
            EntityEntry dbEntityEntry = (EntityEntry)this.DbContext.Entry<T>(entity);
            if (dbEntityEntry.State != EntityState.Detached)
                dbEntityEntry.State = EntityState.Added;
            else
                this.DbSet.Add(entity);
        }

        public virtual async Task AddAsync(T entity)
        {
            EntityEntry dbEntityEntry = (EntityEntry)this.DbContext.Entry<T>(entity);
            if (dbEntityEntry.State != EntityState.Detached)
                dbEntityEntry.State = EntityState.Added;
            else
                await this.DbSet.AddAsync(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
                this.Add(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await this.DbSet.AddRangeAsync(entities);
        }

        public virtual void AddRelatedEntity(T item, IEnumerable<object> updatedSet1, IEnumerable<object> updatedSet2 = null)
        {
            this.DbContext.Entry(item).State = EntityState.Added;
            foreach (var entity in updatedSet1.ToList())
            {
                this.DbContext.Entry(entity).State = EntityState.Added;
            }
            if (updatedSet2 != null)
            {
                foreach (var entity in updatedSet2.ToList())
                {
                    this.DbContext.Entry(entity).State = EntityState.Added;
                }
            }
        }

        public virtual IQueryable<T> All()
        {
            return this.DbSet.AsQueryable<T>();
        }

        public virtual async Task<List<T>> AllAsync()
        {
            return await this.DbSet.ToListAsync<T>();
        }

        public virtual void Delete(T entity)
        {
            EntityEntry entityEntry = (EntityEntry)this.DbContext.Entry<T>(entity);
            if (entityEntry.State != EntityState.Deleted)
            {
                entityEntry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            T byId = this.GetById(id);
            if ((object)byId == null)
                return;
            this.Delete(byId);
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                this.Delete(entity);
            }
        }

        public virtual void DeleteRange(IEnumerable<T> entities, Dictionary<string, object> properties)
        {
            foreach (T entity in entities)
            {
                this.BaseChanges(properties, (object)entity);
                this.Delete(entity);
            }
        }

        public virtual void Detach(T entity)
        {
            EntityEntry dbEntityEntry = (EntityEntry)this.DbContext.Entry<T>(entity);
            if (dbEntityEntry.State != EntityState.Detached)
                dbEntityEntry.State = EntityState.Detached;
        }

        public virtual async Task<bool> ExecuteNonQuerySp(string spName, SqlParameter[] InSqlParameters = null)
        {
            bool isSuccess = false;
            if (!string.IsNullOrEmpty(spName))
            {
                using (SqlConnection con = new SqlConnection(this.DbContext.Database.GetDbConnection().ConnectionString))
                {
                    try
                    {
                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        using (SqlCommand cmd = new SqlCommand(spName, con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            if (InSqlParameters != null && InSqlParameters.Length > 0)
                            {
                                cmd.Parameters.AddRange(InSqlParameters);
                            }
                            await cmd.ExecuteNonQueryAsync();
                            isSuccess = true;
                        }
                    }
                    catch
                    {
                        isSuccess = false;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }
            return isSuccess;
        }

        public virtual SpOut ExecuteSp(string spName, SqlParameter[] InSqlParameters = null, SqlParameter[] OutSqlParameters = null)
        {
            SpOut spOut = new SpOut();
            if (!string.IsNullOrEmpty(spName))
            {
                using (SqlConnection con = new SqlConnection(this.DbContext.Database.GetDbConnection().ConnectionString))
                {

                    using (SqlCommand cmd = new SqlCommand(spName, con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (InSqlParameters != null && InSqlParameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(InSqlParameters);
                        }
                        if(OutSqlParameters != null && OutSqlParameters.Length> 0)
                        {
                            cmd.Parameters.AddRange(OutSqlParameters);
                            foreach(var op in OutSqlParameters)
                            {
                                cmd.Parameters[op.ParameterName].Direction = ParameterDirection.Output;
                            }
                        }

                        DataTable dt = new DataTable();
                        using(SqlDataAdapter adp = new SqlDataAdapter())
                        {
                            adp.SelectCommand = cmd;
                            adp.Fill(dt);
                        }

                        spOut.Data = dt;
                        spOut.OutParam = OutSqlParameters;
                    }

                }
            }
            return spOut;
        }

        public virtual SpOutDs ExecuteSpForDS(string spName, SqlParameter[] InSqlParameters = null, SqlParameter[] OutSqlParameters = null)
        {
            SpOutDs spOutDs = new SpOutDs();
            if(!string.IsNullOrEmpty(spName))
            {
                using(SqlConnection con = new SqlConnection(this.DbContext.Database.GetDbConnection().ConnectionString))
                {
                    using(SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (InSqlParameters != null && InSqlParameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(InSqlParameters);
                        }
                        if (OutSqlParameters != null && OutSqlParameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(OutSqlParameters);
                            foreach (var op in OutSqlParameters)
                            {
                                cmd.Parameters[op.ParameterName].Direction = ParameterDirection.Output;
                            }
                        }

                        DataSet dt = new DataSet();
                        using (SqlDataAdapter adp = new SqlDataAdapter())
                        {
                            adp.SelectCommand = cmd;
                            adp.Fill(dt);
                        }

                        spOutDs.DS = dt;
                        spOutDs.OutParam = OutSqlParameters;
                    }
                }
            }
            return spOutDs;
        }

        public virtual Type ExecuteSpForSongleResult<Type>(string spName, SqlParameter[] InSqlParameters = null)
        {
            Type retValue = default(Type);
            if (!string.IsNullOrEmpty(spName))
            {
                using (SqlConnection con = new SqlConnection(this.DbContext.Database.GetDbConnection().ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (InSqlParameters != null && InSqlParameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(InSqlParameters);
                        }


                        using (DataSet dt = new DataSet())
                        {
                            using (SqlDataAdapter adp = new SqlDataAdapter())
                            {
                                adp.SelectCommand = cmd;
                                adp.Fill(dt);

                                retValue = JsonConvert.DeserializeObject<IEnumerable<Type>>(JsonConvert.SerializeObject(dt)).FirstOrDefault();
                            }
                        }
                    }
                }
            }
            return retValue;
        }

        public virtual IEnumerable<Type> ExecuteSpToType<Type>(string SpName, SqlParameter[] InSqlParameters = null)
        {
            IEnumerable<Type> retValue = null;
            if (!string.IsNullOrEmpty(SpName))
            {
                using (SqlConnection con = new SqlConnection(this.DbContext.Database.GetDbConnection().ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (InSqlParameters != null && InSqlParameters.Length > 0)
                        {
                            cmd.Parameters.AddRange(InSqlParameters);
                        }


                        using (DataSet dt = new DataSet())
                        {
                            using (SqlDataAdapter adp = new SqlDataAdapter())
                            {
                                adp.SelectCommand = cmd;
                                adp.Fill(dt);

                                retValue = JsonConvert.DeserializeObject<IEnumerable<Type>>(JsonConvert.SerializeObject(dt));
                            }
                        }
                    }
                }
            }
            return retValue;

        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> filter)
        {
            return await this.DbSet.AnyAsync(filter);
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> expression)
        {
            return this.DbSet.Where<T>(expression);
        }

        public async virtual Task<List<T>> FindByAsync(Expression<Func<T, bool>> expression)
        {
            return await this.DbSet.Where<T>(expression).ToListAsync();
        }

        public virtual T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return this.DbSet.FirstOrDefault<T>(expression);
        }

        public virtual async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await this.DbSet.FirstOrDefaultAsync<T>(expression);
        }

        public virtual IQueryable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = this.DbSet;
            if(navigationProperties != null)
            {
                dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
            }
            return dbQuery.AsNoTracking();
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, string orderBy = null, char orderDir = 'A', string includeProperties = "", int? numberOfRecordsInList = null, int? skip = null)
        {
            IQueryable<T> query = this.GetQuery(filter, orderBy, orderDir, includeProperties, numberOfRecordsInList, skip);

            return await query.ToListAsync();
        }

        public virtual T GetById(int id)
        {
            return this.DbSet.Find((object)id);
        }

        public virtual T GetById(Guid id)
        {
            return this.DbSet.Find((object)id);
        }

        public async virtual Task<T> GetByIdAsync(int id)
        {
            return await this.DbSet.FindAsync((object)id);
        }

        public virtual ICollection<T> GetCacheList(Expression<Func<T, bool>> where = null)
        {
            var applicationData = GetAll().ToList();
            if (@where == null) return applicationData;
            var dbQuery = applicationData.AsQueryable();
            return dbQuery.Where(@where).AsNoTracking().ToList();
        }

        public virtual async Task<ICollection<T>> GetCacheListAsync(Expression<Func<T, bool>> where = null)
        {
            if (where == null) return this.DbSet.AsNoTracking().ToList();
            return await Task.FromResult(this.DbSet.AsNoTracking().ToList());
        }

        public virtual IQueryable<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            var dbQuery = this.DbSet.AsQueryable();
            dbQuery = navigationProperties.Aggregate(dbQuery, (current, navigationProperty) => current.Include(navigationProperty));
            var list = dbQuery.Where(@where).AsNoTracking().AsQueryable();
            return list;
        }

        public virtual IQueryable<T> GetQuery(Expression<Func<T, bool>> filter = null, string orderBy = null, char orderDir = 'A', string includeProperties = "", int? numberOfRecordsInList = null, int? skip = null)
        {
            if(orderDir != 'A' && orderDir != 'D')
            {
                orderDir = 'A';
            }

            DbSet<T> _dbSet = this.DbContext.Set<T>();
            IQueryable<T> query = _dbSet;

            if(filter != null)
            {
                query = query.Where(filter);
            }

            if(includeProperties != null)
            {
                foreach(var includeProperty in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if(!string.IsNullOrEmpty(orderBy))
            {
                query = query.OrderByCustom<T>(orderBy, orderDir);
            }

            if(skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if(numberOfRecordsInList.HasValue)
            {
                query = query.Take(numberOfRecordsInList.Value);
            }

            return query.AsNoTracking();
        }

        public virtual List<T> Include(Expression<Func<T, object>> expression)
        {
            return this.DbSet.Include<T, object>(expression).ToList();
        }

        public virtual List<T> Include(Expression<Func<T, object>> expression, Expression<Func<T, bool>> whereExpression)
        {
            return this.DbSet.Include<T, object>(expression).Where(whereExpression).ToList();
        }

        public virtual T LastOrDefault(Expression<Func<T, bool>> expression)
        {
            return this.DbSet.LastOrDefault(expression);
        }

        public void SaveDocument(DataTable xmlData, string xmlPath, string destinationTableName, bool isFireTrigger)
        {
            var document = new XmlDocument();
            document.Load(xmlPath);
            if(document.DocumentElement == null)
            {
                return;
            }

            var selectNode = document.DocumentElement.SelectSingleNode("Mapping");
            using (var sqlBulk = new SqlBulkCopy(this.DbContext.Database.GetDbConnection().ConnectionString, isFireTrigger ? SqlBulkCopyOptions.FireTriggers : SqlBulkCopyOptions.KeepIdentity))
            {
                sqlBulk.DestinationTableName = destinationTableName;
                if(selectNode == null)
                {
                    return;
                }
                foreach(XmlElement element in selectNode)
                {
                    var entityNode = element.SelectSingleNode("Entity");
                    if(entityNode == null)
                    {
                        continue;
                    }
                    var sqlNode = element.SelectSingleNode("SQL");
                    if(sqlNode != null)
                    {
                        sqlBulk.ColumnMappings.Add(entityNode.InnerText, sqlNode.InnerText);

                    }
                }

                sqlBulk.BulkCopyTimeout = 0;
                sqlBulk.WriteToServer(xmlData);
            }
        }

        public virtual T SingleOrDefault(Expression<Func<T, bool>> expression)
        {
            return this.DbSet.SingleOrDefault<T>(expression);
        }

        public BoolValue SqlBulkCopy(DataTable writeData, string destinationTable, int timeOut)
        {
            BoolValue Objboolvalue = new BoolValue();
            Objboolvalue.value = false;
            try
            {
                using(var connection = new SqlConnection(this.DbContext.Database.GetDbConnection().ConnectionString))
                {
                    if(connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                    }
                    using(var bulkcopy = new SqlBulkCopy(connection))
                    {
                        bulkcopy.BulkCopyTimeout = timeOut;
                        bulkcopy.DestinationTableName = destinationTable;
                        bulkcopy.WriteToServer(writeData);
                    }
                }

                Objboolvalue.value = true;
                return Objboolvalue;
            }
            catch
            {
                return Objboolvalue;
            }
        }

        public async Task Truncate()
        {
            var query = $"TRUNCATE TABLE {typeof(T).Name}";
            await DbContext.Database.ExecuteSqlRawAsync(query);
        }

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = (EntityEntry)this.DbContext.Entry<T>(entity);
            if (dbEntityEntry.State == EntityState.Detached)
                this.DbSet.Attach(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
                this.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities, Dictionary<string, object> properties)
        {
            foreach(T entity in entities)
            {
                this.BaseChanges(properties, (object)entity);
                this.Update(entity);
            }
        }

        public virtual void UpdateRelatedEntity(T item, IEnumerable<object> updatedSet1, IEnumerable<object> updatedSet2 = null)
        {
            this.DbContext.Entry(item).State = EntityState.Modified;
            foreach(var entity in updatedSet1.ToList())
            {
                this.DbContext.Entry(entity).State = EntityState.Modified;
            }
            if(updatedSet2 != null)
            {
                foreach(var entity in updatedSet2.ToList())
                {
                    this.DbContext.Entry(entity).State = EntityState.Modified;
                }
            }
        }

        protected virtual void BaseChanges(Dictionary<string, object> properties, object entity)
        {
            foreach (KeyValuePair<string, object> property1 in properties)
            {
                PropertyInfo propertyInfo = entity.GetType().GetProperty(property1.Key);
                propertyInfo.SetValue(entity, Convert.ChangeType(property1.Value, propertyInfo.PropertyType), (object[])null);
            }
        }
    }

    internal static class Ordering
    {
        public static IQueryable<T> OrderByCustom<T>(this IQueryable<T> source, string ordering, char orderDirection = 'A', params object[] values)
        {
            var type = typeof(T);

            var propertyAccessAndType = ConstructPropertyExpressionAndResultType(type, ordering);
            var orderByExpression = propertyAccessAndType.Item1;
            var typeArray = propertyAccessAndType.Item2;

            return source.Provider.CreateQuery<T>(Expression.Call(typeof(Queryable), orderDirection == 'A' ? "OrderBy" : "OrderByDescending", typeArray, source.Expression, orderByExpression));
        }

        private static Tuple<UnaryExpression, Type[]> ConstructPropertyExpressionAndResultType(Type type, string ordering)
        {
            var resultType = type;

            var parameter = Expression.Parameter(type, "p");
            Expression propertyAccessExpression = parameter;

            var splitOrderBy = ordering.Split('.');
            foreach(var orderByProperty in splitOrderBy)
            {
                resultType = resultType.GetProperty(orderByProperty).PropertyType;
                propertyAccessExpression = Expression.PropertyOrField(propertyAccessExpression, orderByProperty);

            }

            var orderByExpression = Expression.Quote(Expression.Lambda(propertyAccessExpression, parameter));
            var typeArray = new Type[] { type, resultType };
            return new Tuple<UnaryExpression, Type[]>(orderByExpression, typeArray);
        }
    }
}
