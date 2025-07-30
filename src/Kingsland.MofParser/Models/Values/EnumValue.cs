namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class EnumValue : EnumTypeValue
{

    [PublicAPI]
    internal EnumValue(string value)
        : this(null, value)
    {
    }

    [PublicAPI]
    internal EnumValue(string? name, string literal)
    {
        this.Name = name;
        this.Literal = literal ?? throw new ArgumentNullException(nameof(literal));
    }

    [PublicAPI]
    public string? Name
    {
        get;
    }

    [PublicAPI]
    public string Literal
    {
        get;
    }

    public static implicit operator EnumValue(string literal) => new(literal);

}
