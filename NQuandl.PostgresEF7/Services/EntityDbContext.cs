﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using NQuandl.PostgresEF7.Api.Entities;
using NQuandl.PostgresEF7.Services.Models.ModelCreation;

namespace NQuandl.PostgresEF7.Services
{
    public class EntityDbContext : DbContext, IWriteEntities
    {
        
        // todo move to interface
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=192.168.43.191;Username=postgres;Password=postgres;Database=nquandl;MINPOOLSIZE=10;MAXPOOLSIZE=40;Connection Lifetime=0;");
        }

        #region Model Creation

        public ICreateDbModel ModelCreator { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelCreator = ModelCreator ?? new DefaultDbModelCreator();
            ModelCreator.Create(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Queries

        public IQueryable<TEntity> Query<TEntity>() where TEntity : Entity
        {
            // AsNoTracking returns entities that are not attached to the DbContext
            return new EntitySet<TEntity>(Set<TEntity>().AsNoTracking(), this);
        }

        #endregion

        #region Commands

        public TEntity Get<TEntity>(object firstKeyValue, params object[] otherKeyValues) where TEntity : Entity
        {
            if (firstKeyValue == null)
                throw new ArgumentNullException("firstKeyValue");
            var keyValues = new List<object> {firstKeyValue};
            if (otherKeyValues != null)
                keyValues.AddRange(otherKeyValues);
            return Set<TEntity>().FirstOrDefault(x => x.Equals(keyValues.ToArray()));
        }

        public Task<TEntity> GetAsync<TEntity>(object firstKeyValue, params object[] otherKeyValues)
            where TEntity : Entity
        {
            if (firstKeyValue == null)
                throw new ArgumentNullException("firstKeyValue");
            var keyValues = new List<object> {firstKeyValue};
            if (otherKeyValues != null)
                keyValues.AddRange(otherKeyValues);
            return Set<TEntity>().FirstOrDefaultAsync(x => x.Equals(keyValues.ToArray()));
        }

        public IQueryable<TEntity> Get<TEntity>() where TEntity : Entity
        {
            return new EntitySet<TEntity>(Set<TEntity>(), this);
        }

        public void Create<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (Entry(entity).State == EntityState.Detached)
                Set<TEntity>().Add(entity);
           
        }

        public void Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            var entry = Entry(entity);
            entry.State = EntityState.Modified;
        }

        public Task SaveChangesAsync()
        {
           return base.SaveChangesAsync();
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (Entry(entity).State != EntityState.Deleted)
                Set<TEntity>().Remove(entity);
        }

        #endregion
    }
}