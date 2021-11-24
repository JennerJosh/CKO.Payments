﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CKO.Payments.DAL.Repositories.Interfaces;
using CKO.Payments.Data;
using Microsoft.EntityFrameworkCore;

namespace CKO.Payments.DAL.Repositories
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly CkoContext _context;
        private readonly DbSet<T> _entities;

        public BaseRepository(CkoContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public virtual void Add(T entity) =>
            _entities.Add(entity);

        public virtual void AddRange(IEnumerable<T> entities) =>
            _entities.AddRange(entities);

        public virtual void Update(T entity) =>
            _entities.Update(entity);

        public virtual void Delete(T entity) =>
            _entities.Remove(entity);

        public virtual void DeleteRange(IEnumerable<T> entities) =>
            _entities.RemoveRange(entities);

        public virtual T GetById(Guid id) =>
            _entities.Find(id);

        public virtual IQueryable<T> GetQuery() =>
            _entities.AsQueryable();


        public virtual void SaveChanges() =>
            _context.SaveChanges();

    }
}
