﻿using System.Data.Entity;
using System.Threading.Tasks;

namespace DAL.DataLayer
{
    public interface IContext
    {
        DbSet<Person> Persons { get; set; }
        Task<int> SaveChangesAsync();
        void Attach<T>(T entity) where T : class;
        void SetModified<T>(T entity) where T : class;
    }

    public class Context : DbContext, IContext
    {
        public virtual DbSet<Person> Persons { get; set; }
        public void Attach<T>(T entity) where T : class 
        {
            if (Entry(entity).State == EntityState.Detached)
            {
                Set<T>().Attach(entity);
            }
        }

        public void SetModified<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}