namespace Kingsland.MofParser.Models.Values;

[PublicAPI]
public sealed class EnumValue : EnumTypeValue
{

    [PublicAPI]
    public EnumValue(string name)
        : this(null, name)
    {
    }

    [PublicAPI]
    public EnumValue(string? type, string name)
    {
        this.Type = type;
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    [PublicAPI]
    public string? Type
    {
        get;
    }

    [PublicAPI]
    public string Name
    {
        get;
    }

}
