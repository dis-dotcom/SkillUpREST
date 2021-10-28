namespace SkillUpREST.Entity;

using Newtonsoft.Json;
using System.Collections.Generic;


public class Company : EntityBase
{
    public Company(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public string Name { get; }

    [JsonProperty("owner-id")] public Guid? OwnerId { get; }

    public IEnumerable<User> Employees { get; } = Array.Empty<User>();
}
