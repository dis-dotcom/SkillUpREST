namespace SkillUpREST.EntityExt;

using Newtonsoft.Json;
using SkillUpREST.Entity;

public static class UserExt
{
    public static string ToJson(User user) => JsonConvert.SerializeObject(user, Formatting.Indented);
}
