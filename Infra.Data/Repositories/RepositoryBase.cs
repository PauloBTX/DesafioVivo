using Domain.Interfaces.Repositories;
using Infra.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable
namespace Infra.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected VivoContext Db = new VivoContext();


        public virtual void Add(TEntity obj)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    Db.Set<TEntity>().Add(obj);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }

        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().AsNoTracking().ToList();
        }

        public virtual TEntity GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public virtual void Update(TEntity obj)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    Db.Entry(obj).State = EntityState.Modified;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        public virtual void Remove(TEntity obj)
        {
            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    Db.Set<TEntity>().Remove(obj);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await Db.SaveChangesAsync()) > 0;
        }

        public bool SaveChanges()
        {
            return (Db.SaveChanges()) > 0;
        }

        public void Dispose()
        {
            if (Db != null)
            {
                Db.Dispose();
                GC.SuppressFinalize(this);
            }
        }
    }
}
