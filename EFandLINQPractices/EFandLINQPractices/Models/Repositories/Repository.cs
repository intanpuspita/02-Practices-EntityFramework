using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace EFandLINQPractices.Models.Repositories
{
    public class Repository<T> where T : class
    {
        private bool disposed = false;
        private SchoolContext context = null;
        protected DbSet<T> dbset { get; set; }

        public Repository()
        {
            this.context = new SchoolContext();
            dbset = this.context.Set<T>();
        }

        public Repository(SchoolContext context)
        {
            this.context = context;
        }

        public List<T> GetAll()
        {
            return dbset.ToList();
        }

        public void Add(T data)
        {
            dbset.Add(data);
        }

        public T GetById(string id)
        {
            return dbset.Find(id);
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            //context.Entry(entity).State = EntityState.Modified;
            //dbset.Attach(entity);
            //context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(string id)
        {
            T data = dbset.Find(id);
            dbset.Remove(data);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                context.Dispose();
                disposed = true;
            }
        }
    }
}