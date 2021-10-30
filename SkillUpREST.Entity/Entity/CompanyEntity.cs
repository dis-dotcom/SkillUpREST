namespace SkillUpREST.Entity;

using Newtonsoft.Json;

public class CompanyEntity : EntityBase
{
    public string Name { get; set; }

    [JsonProperty("owner-id")]
    public Guid? OwnerId { get; set; }
}
