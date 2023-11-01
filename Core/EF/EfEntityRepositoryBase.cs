using Core.DataAcces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.EF
{
    public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
        where TEntity : class,IEntity, new()
        where TContext:DbContext,new()
    {
        public void Add(TEntity entity)
        {
            using (TContext ctx = new TContext())
            {
                var addedEntity = ctx.Entry(entity);
                addedEntity.State = EntityState.Added;
                ctx.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext ctx = new TContext())
            {
                var deletedEntity = ctx.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                ctx.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext ctx = new TContext())
            {
                return ctx.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public TEntity GetById(int id)
        {
            using(TContext ctx = new TContext())
            {
                return ctx.Set<TEntity>().Find(id);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext ctx = new TContext())
            {
                return filter == null ? ctx.Set<TEntity>().ToList() : ctx.Set<TEntity>().Where(filter).ToList();
            }
        }

        public List<TEntity> GetAllByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            using (TContext ctx = new TContext())
            {
                var updatedEntity = ctx.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                ctx.SaveChanges();
            }
        }
    }
}
