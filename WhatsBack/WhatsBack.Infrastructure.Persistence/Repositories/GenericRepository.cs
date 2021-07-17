using WhatsBack.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using WhatsBack.SharedKernal.Enum;
using WhatsBack.Application.Interfaces.Repositories;

namespace WhatsBack.Infrastructure.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        /// <summary>
        /// Defines the entities
        /// </summary>
        private DbSet<T> entities;
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="ElectricityCorrespondenceContext"/></param> 
        public GenericRepository(ApplicationDbContext _context)
        {
            context = _context;
            entities = context.Set<T>();
        }

        #region Insert
        public T Insert(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            return entities.Add(entity).Entity;
        }
        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            return (await entities.AddAsync(entity)).Entity;
        }
        public void BulkInsert(List<T> entities)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            // entities.AddRange(entities);
            context.AddRangeAsync(entities);
        }
        #endregion

        #region Update
        public void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            //#region Check Related Data Before Deleting
            //var isDeleted = entity.GetType().GetProperties().Where(a => a.Name == "IsDeleted").FirstOrDefault();

            //if (isDeleted != null && (bool)isDeleted.GetValue(entity))
            //{
            //    // attach entity to load related data
            //    entities.Attach(entity);

            //    List<string> exculededProps = new List<string> { nameof(CorrespondenceFile), nameof(CorrespondenceComment), nameof(CorrespondenceHiddenData), nameof(CorrespondenceStepHistory), nameof(CorrespondenceDepartment), nameof(RoleWorkflowStepBussinesRule) };

            //    var props = entity.GetType().GetProperties().Where(a => a.PropertyType.IsInterface && a.PropertyType.Name == "ICollection`1").Where(x => !exculededProps.Contains(x.Name));
            //    foreach (PropertyInfo pInfo in props)
            //    {
            //        context.Entry(entity).Collection(pInfo.Name).Load();

            //        var list = (IReadOnlyCollection<object>)pInfo.GetValue(entity);

            //        foreach (var item in list)
            //        {
            //            var isDeletedProp = item.GetType().GetProperties().Where(a => a.Name == "IsDeleted").FirstOrDefault();
            //            if (isDeletedProp != null && !(bool)isDeletedProp.GetValue(item))
            //                throw new ValidationsException($"Can't delete, There's another non deleted data related to that item ( { pInfo.Name } )");
            //        }
            //    }
            // deatch entity for update
            //    context.Entry(entity).State = EntityState.Detached;
            //}
            //#endregion

            entities.Update(entity);
        }
        #endregion

        #region Delete
        public void BulkHardDelete(Expression<Func<T, bool>> filter = null)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            entities.RemoveRange(entities.Where(filter));
        }
        public void Delete(T entity)
        {
            if (entities == null)
                throw new ArgumentNullException("entities");
            context.Entry(entity).State = EntityState.Deleted;
           
        }
        #endregion

        #region Retreive

        #region GetById
        public async Task<T> GetByIdIfNotDeleted(int Id)
        {
            T record = await entities.FindAsync(Id);
            //context.Entry(record).State = EntityState.Detached;
            if (record != null)
            {
                var property = record.GetType().GetProperties().FirstOrDefault(a => a.Name == "IsDeleted" /*According to system naming convension here*/);
                if (property != null && (bool)property.GetValue(record))
                    return null;
                else
                    context.Entry(record).State = EntityState.Detached;
            }
            return record;
        }
        #endregion

        #region GetList
        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = entities;
            if (filter != null)
                query = query.Where(filter);
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Get Paged
        public async Task<List<T>> GetPageAsync<TKey>(int PageNumeber, int PageSize, Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> sortingExpression, SortDirection sortDir = SortDirection.Ascending, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();
            int skipCount = (PageNumeber - 1) * PageSize;

            if (filter != null)
                query = query.Where(filter);

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            switch (sortDir)
            {
                case SortDirection.Ascending:
                    if (skipCount == 0)
                        query = query.OrderBy<T, TKey>(sortingExpression).Take(PageSize);
                    else
                        query = query.OrderBy<T, TKey>(sortingExpression).Skip(skipCount).Take(PageSize);
                    break;
                case SortDirection.Descending:
                    if (skipCount == 0)
                        query = query.OrderByDescending<T, TKey>(sortingExpression).Take(PageSize);
                    else
                        query = query.OrderByDescending<T, TKey>(sortingExpression).Skip(skipCount).Take(PageSize);
                    break;
                default:
                    break;
            }
            return await query.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Get Individuals
        public async Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null)
        {
            return await entities.CountAsync(filter);
        }
        public async Task<bool> GetAnyAsync(Expression<Func<T, bool>> filter = null)
        {
            return await entities.AnyAsync(filter);
        }
        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();
            if (filter != null)
                query = query.Where(filter).AsNoTracking();
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            T record = await query.FirstOrDefaultAsync();
            if (record != default)
                context.Entry(record).State = EntityState.Detached;
            return record;
        }
        public async Task<T> GetLastOrDefaultAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = context.Set<T>();
            if (filter != null)
                query = query.Where(filter).AsNoTracking();
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            T record = await query.OrderByDescending(item => item).FirstOrDefaultAsync();
            if (record != default)
                context.Entry(record).State = EntityState.Detached;
            return record;
        }

        
        #endregion

        #endregion
    }
}
