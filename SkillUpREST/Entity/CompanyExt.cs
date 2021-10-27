namespace SkillUpREST.Entity.Ext;

using Newtonsoft.Json;
using SkillUpREST.Entity;

public static class CompanyExt
{
    public static string ToJson(this Company company) => JsonConvert.SerializeObject(company, Formatting.Indented);
}

public static class StringExt
{
    public static T FromJson<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
}
