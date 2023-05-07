using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrainzLib.Repository;
using TrainzLiteDb.Data;

namespace TrainzLiteDb
{
    public abstract class AbstractCrudDb<T> : ICrudRepository<T> where T : class
    {
        protected readonly TrainzContext _context;

        protected abstract DbSet<T> DbSet { get; }

        public AbstractCrudDb(TrainzContext context)
        {
            _context = context;
        }

        public void ClearAll()
        {
            foreach (var entity in DbSet)
                _context.Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var entity = GetById(id);
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(int id)
        {
            return DbSet.Find(id);
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            var tmp = _context.ChangeTracker.Entries().FirstOrDefault(t => t.Entity == entity);
            if (tmp != null)
                tmp.State = EntityState.Detached;

            _context.Attach(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
