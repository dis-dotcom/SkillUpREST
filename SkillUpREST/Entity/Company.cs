using Newtonsoft.Json;

namespace SkillUpREST.Entity;

public class Company : Entity
{
    public string Name { get; set; }

    public static Company FromJson(string json)
    {
        return JsonConvert.DeserializeObject<Company>(json);
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}
