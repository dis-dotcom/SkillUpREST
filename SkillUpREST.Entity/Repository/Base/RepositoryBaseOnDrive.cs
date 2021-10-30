namespace SkillUpREST.Entity.Repository;

using Newtonsoft.Json;
using SkillUpREST.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


public class RepositoryBaseOnDrive<TEntity> : RepositoryBase<TEntity>, IRepositoryOnDrive<TEntity> where TEntity : IEntity
{
    private readonly Locker locker;
    private readonly object locationSync = new();

    private string location;

    public RepositoryBaseOnDrive(string path)
    {
        locker = new Locker(new());

        SetLocation(path);
    }

    public virtual string Location
    {
        get => GetLocation();
        protected set => SetLocation(value);
    }

    #region Public
    public override void Insert(TEntity entity)
    {
        locker.UnderLock(InsertInternal, entity);
    }
    public override void InsertMany(IEnumerable<TEntity> entities)
    {
        locker.UnderLock(InsertManyInternal, entities);
    }
    public override void Delete(TEntity entity)
    {
        locker.UnderLock(DeleteInternal, entity);
    }
    public override void DeleteById(Guid id)
    {
        locker.UnderLock(DeleteByIdInternal, id);
    }
    public override void DeleteMany(IEnumerable<TEntity> entities)
    {
        locker.UnderLock(DeleteManyInternal, entities);
    }
    public override TEntity Find(params Predicate<TEntity>[] requirements)
    {
        return locker.UnderLock(FindInternal, requirements);
    }
    public override IEnumerable<TEntity> FindMany(params Predicate<TEntity>[] requirements)
    {
        return locker.UnderLock(FindManyInternal, requirements);
    }
    #endregion

    #region Internal
    private void InsertInternal(TEntity entity)
    {
        Save(entity);
    }
    private void InsertManyInternal(IEnumerable<TEntity> entities)
    {
        entities.ToList()
                .ForEach(Save);
    }
    private void DeleteInternal(TEntity entity)
    {
        DeleteFile(entity);
    }
    private void DeleteByIdInternal(Guid id)
    {
        DeleteFile(id);
    }
    private void DeleteManyInternal(IEnumerable<TEntity> entities)
    {
        entities.ToList()
                .ForEach(DeleteFile);
    }
    private TEntity FindInternal(params Predicate<TEntity>[] requirements)
    {
        return Directory.GetFiles(Location).Where(File.Exists)
                                           .Select(File.ReadAllText)
                                           .Select(EntityFromJson)
                                           .Where(entity => requirements.All(requirement => requirement(entity)))
                                           .FirstOrDefault();
    }
    private IEnumerable<TEntity> FindManyInternal(params Predicate<TEntity>[] requirements)
    {
        return Directory.GetFiles(Location).Where(File.Exists)
                                           .Select(File.ReadAllText)
                                           .Select(EntityFromJson)
                                           .Where(entity => requirements.All(requirement => requirement(entity)));
    }
    #endregion

    private void Save(TEntity entity) => File.WriteAllText(path: PathForEntity(entity),
                                                           contents: JsonForEntity(entity));
    private void DeleteFile(TEntity entity) => File.Delete(path: PathForEntity(entity));
    private void DeleteFile(Guid id) => File.Delete(path: PathForId(id));
    private string PathForId(Guid id) => Path.Combine(Location, id.ToString());
    private string PathForEntity(TEntity entity) => Path.Combine(Location, entity.Id.ToString());
    private TEntity EntityFromJson(string json) => JsonConvert.DeserializeObject<TEntity>(json);
    private string JsonForEntity(TEntity entity) => JsonConvert.SerializeObject(entity, Formatting.Indented);

    private void SetLocation(string location)
    {
        lock (locationSync)
        {
            this.location = location;
        }
    }
    private string GetLocation()
    {
        lock (locationSync)
        {
            return location;
        }
    }

    private class Locker
    {
        private readonly object sync;

        public Locker(object sync)
        {
            this.sync = sync;
        }

        public TOut UnderLock<TOut>(Func<TOut> function)
        {
            TOut value = default;

            lock (sync)
            {
                value = function();
            }

            return value;
        }
        public TOut UnderLock<TIn, TOut>(Func<TIn, TOut> function, TIn arg)
        {
            TOut value = default;

            lock (sync)
            {
                value = function(arg);
            }

            return value;
        }
        public void UnderLock(Action action)
        {
            lock (sync)
            {
                action();
            }
        }
        public void UnderLock<T>(Action<T> action, T arg)
        {
            lock (sync)
            {
                action(arg);
            }
        }
    }
}
