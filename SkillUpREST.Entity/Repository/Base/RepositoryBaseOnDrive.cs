namespace SkillUpREST.Entity.Repository;


using Newtonsoft.Json;
using SkillUpREST.Repositories.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


public class RepositoryBaseOnDrive<TEntity> : RepositoryBase<TEntity>, IRepositoryOnDrive<TEntity> where TEntity : IEntity
{
    private object sync = new object();

    public RepositoryBaseOnDrive(string repositoryPath)
    {
        Location = repositoryPath;
    }

    public virtual string Location { get; protected set; }

    public override void Insert(TEntity entity)
    {
        File.WriteAllText(PathForEntity(entity), JsonForEntity(entity));
    }
    public override void InsertMany(IEnumerable<TEntity> items)
    {
        foreach (var item in items)
        {
            File.WriteAllText(PathForEntity(item), JsonForEntity(item));
        }
    }
    public override void Delete(TEntity entity)
    {
        File.Delete(PathForEntity(entity));
    }
    public override void DeleteById(Guid id)
    {
        File.Delete(PathForId(id));
    }
    public override void DeleteMany(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            File.Delete(PathForEntity(entity));
        }
    }
    public override TEntity Find(params Predicate<TEntity>[] requirements)
    {
        foreach (var filePath in Directory.GetFiles(Location))
        {
            TEntity entity = default;

            lock (sync)
            {
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var tmpEntity = EntityFromJson(json);

                    if (requirements.All(requirement => requirement(entity)))
                    {
                        entity = tmpEntity;
                    }
                }
            }

            if (entity.Equals(default))
            {
                continue;
            }

            return entity;
        }

        return default;
    }
    public override IEnumerable<TEntity> FindMany(params Predicate<TEntity>[] requirements)
    {
        foreach (var filePath in Directory.GetFiles(Location))
        {
            TEntity entity = default;

            lock (sync)
            {
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var tmpEntity = EntityFromJson(json);

                    if (requirements.All(requirement => requirement(entity)))
                    {
                        entity = tmpEntity;
                    }
                }
            }

            if (entity.Equals(default))
            {
                continue;
            }

            yield return entity;
        }
    }

    string PathForId(Guid id) => Path.Combine(Location, id.ToString());
    string PathForEntity(TEntity entity) => Path.Combine(Location, entity.Id.ToString());
    TEntity EntityFromJson(string json) => JsonConvert.DeserializeObject<TEntity>(json);
    string JsonForEntity(TEntity entity) => JsonConvert.SerializeObject(entity, Formatting.Indented);
}
