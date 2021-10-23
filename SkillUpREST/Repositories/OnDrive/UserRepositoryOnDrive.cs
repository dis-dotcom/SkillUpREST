using Newtonsoft.Json;
using SkillUpREST.Entity;
using SkillUpREST.Repositories.Exceptions;
using SkillUpREST.Repositories.Interfaces;
using System.Collections.Generic;
using System.IO;

namespace SkillUpREST.Repositories.OnDrive
{
    public class UserRepositoryOnDrive : IUserRepository
    {
        private readonly string _repositoryPath;

        public UserRepositoryOnDrive(string repositoryPath)
        {
            _repositoryPath = repositoryPath;
        }

        public IEnumerable<User> Get()
        {
            foreach (var filePath in GetFiles())
            {
                yield return JsonConvert.DeserializeObject<User>(File.ReadAllText(filePath));
            }
        }

        public User GetById(Guid id)
        {
            return Exists(id)
                ? JsonConvert.DeserializeObject<User>(File.ReadAllText(GetFilePath(id)))
                : null;
        }

        public void Insert(User user)
        {
            if (Exists(user))
            {
                throw new UserAlreadyExistsException();
            }

            File.WriteAllText(GetFilePath(user.Id), JsonConvert.SerializeObject(user));
        }
        
        public void Update(User user)
        {
            if (Exists(user))
            {
                Delete(user.Id);
                Insert(user);
            }
        }

        public void Delete(User entity)
        {
            Delete(entity.Id);
        }

        public void Delete(Guid id)
        {
            if (Exists(id))
            {
                File.Delete(GetFilePath(id));
            }
        }

        public User Find(params Predicate<User>[] requirements) => FindAll(requirements).FirstOrDefault();

        public IEnumerable<User> FindAll(params Predicate<User>[] requirements) => Get().Where(u => requirements.All(r => r(u)));

        private IEnumerable<string> GetFiles()
        {
            try
            {
                return Directory.GetFiles(_repositoryPath);
            }
            catch
            {
                return Array.Empty<string>();
            }
        }
        private bool Exists(Guid id) => File.Exists(GetFilePath(id));
        private bool Exists(User user) => File.Exists(GetFilePath(user.Id));
        private string GetFilePath(Guid id) => Path.Combine(_repositoryPath, id.ToString());
    }
}
