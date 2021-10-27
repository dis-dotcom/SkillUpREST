namespace SkillUpREST.Entity.Repository;

using SkillUpREST.Entity.Repository.Interfaces;
using System;
using System.Linq;


public static class Repository
{
    public static IRepository<TEntity> Resolve<TEntity>(params (string Key, object Value)[] @params) where TEntity: IEntity
    {
        if (typeof(TEntity) == typeof(User))
        {
            var location = @params.First(it => it.Key == "Location").Value as string;

            return new UserRepositoryOnDrive(location) as IRepository<TEntity>;
        }

        throw new Exception($"Can't resolve required Repository by type {typeof(TEntity)}");
    }

    public static IUserRepository ResolveUserRepository(params (string Key, object Value)[] @params)
    {
        var location = @params.First(it => it.Key == "Location").Value as string;
        
        return new UserRepositoryOnDrive(location);
    }

    public static ICompanyRepository ResolveCompanyRepository(params (string Key, object Value)[] @params)
    {
        var location = @params.First(it => it.Key == "Location").Value as string;

        return new CompanyRepositoryOnDrive(location);
    }
}
