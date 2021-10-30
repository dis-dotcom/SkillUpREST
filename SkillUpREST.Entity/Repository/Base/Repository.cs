namespace SkillUpREST.Entity.Repository;

using SkillUpREST.Entity.Repository.Interfaces;
using System.Linq;

public static class Repository
{
    public static T Resolve<T>(params (string Key, object Value)[] @params) where T: class
    {
        var location = @params.First(it => it.Key == "Location").Value as string;

        if (typeof(T) == typeof(IUserRepository))
        {
            return ResolveUserRepository(location) as T;
        }

        if (typeof(T) == typeof(ICompanyRepository))
        {
            return ResolveCompanyRepository(location) as T;
        }

        throw new Exception($"Can't resolve {typeof(T)}");
    }

    internal static IUserRepository ResolveUserRepository(string location)
    {
        return new UserRepositoryOnDrive(location);
    }

    internal static ICompanyRepository ResolveCompanyRepository(string location)
    {
        return new CompanyRepositoryOnDrive(location);
    }
}
