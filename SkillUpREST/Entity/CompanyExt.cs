namespace SkillUpREST.Entity.Ext;

using Newtonsoft.Json;
using SkillUpREST.Entity;

[Obsolete]
public static class CompanyExt
{
    [Obsolete]
    public static string ToJson(this CompanyEntity company) => JsonConvert.SerializeObject(company, Formatting.Indented);
}

[Obsolete]
public static class StringExt
{
    [Obsolete]
    public static T FromJson<T>(this string json) => JsonConvert.DeserializeObject<T>(json);
}
