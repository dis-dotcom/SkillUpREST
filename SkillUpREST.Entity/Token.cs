namespace SkillUpREST.Entity;


public class Token
{
    public string Value { get; internal set; }

    internal Token(Guid guid)
    {
        Value = guid.ToString(format: "N");
    }

    public static Token New() => new(Guid.NewGuid());

    public static Token From(string rawValue) => new(Guid.ParseExact(rawValue, format: "N"));
}
