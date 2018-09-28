using Hotel.Core.Database;
using Hotel.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Repository
{
    public class BaseRepository<T> : IDisposable where T : class, IBaseEntity
    {
        internal EticaretDbContext context = null;

        public DbSet<T> Entity { get { return context.Set<T>(); } }

        public BaseRepository()
        {
            context = new EticaretDbContext();
        }

        public virtual bool Add(T entity)
        {
            entity.CreateTime = DateTime.Now;
            Entity.Add(entity);

            return context.SaveChanges() > 0;
        }

        public virtual T Find(int Id)
        {
            return Entity.FirstOrDefault(x => x.Id == Id);
        }

        public virtual bool Delete(T entity)
        {
            if (entity != null && entity.Id != default(int))
            {
                var record = Find(entity.Id);
                if (record == null)
                {
                    throw new NullReferenceException(nameof(entity.Id));
                }
                record.IsDeleted = true;
                return context.SaveChanges() > 0;
            }
            throw new ArgumentOutOfRangeException(nameof(entity.Id));
        }

        public virtual bool Update(T entity)
        {
            if (entity != null && entity == null)
            {
                throw new ArgumentOutOfRangeException(nameof(entity.Id));
            }

            var record = Find(entity.Id);
            if (record == null)
            {
                throw new NullReferenceException(nameof(entity.Id));
            }

            record = entity;

            return context.SaveChanges() > 0;
        }

        public virtual bool Update_New(T entity)
        {
            context.Set<T>().AddOrUpdate(entity);
            return context.SaveChanges() > 0;
        }

        public virtual IQueryable<IBaseEntity> ListAll()
        {
            return Entity;
        }


        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
        public IQueryable<E> Query<E>() where E : class
        {
            return context.Set<E>();
        }
    }
   
}
